<template>
	<view class="container">
		<view class="tui-page-title">关联手机号</view>
		<view class="tui-form">
			<view class="tui-view-input">
				<tui-list-cell :hover="false" :lineLeft="false" backgroundColor="transparent">
					<view class="tui-cell-input">
						<tui-icon name="mobile" color="#6d7a87" :size="20"></tui-icon>
						<input :value="mobile" placeholder="请输入手机号" placeholder-class="tui-phcolor" type="number" maxlength="11" @input="inputMobile" />
						<view class="tui-icon-close" v-show="mobile" @tap="clearInput(1)">
							<tui-icon name="close-fill" :size="16" color="#bfbfbf"></tui-icon>
						</view>
					</view>
				</tui-list-cell>
				<tui-list-cell :hover="false" :lineLeft="false" backgroundColor="transparent">
					<view class="tui-cell-input">
						<tui-icon name="shield" color="#6d7a87" :size="20"></tui-icon>
						<input placeholder="请输入验证码" placeholder-class="tui-phcolor" type="text" maxlength="6" @input="inputCode" />
						<view class="tui-btn-send" @tap="sendCode" :class="{ 'tui-gray': isSend }" :hover-class="isSend ? '' : 'tui-opcity'" :hover-stay-time="150">{{ btnSendText }}</view>
					</view>
				</tui-list-cell>
			</view>
			<view class="tui-btn-box">
				<tui-button :disabledGray="true" :disabled="disabled" :shadow="true" shape="circle" @tap="registerWechatUser">关联</tui-button>
			</view>
			<view class="tui-cell-text">
				注册代表同意
				<view class="tui-color-primary" hover-class="tui-opcity" :hover-stay-time="150" @tap="protocol">MiCake用户服务协议、隐私政策</view>
			</view>
		</view>

		<!--居中消息-->
		<tui-tips position="center" ref="toast"></tui-tips>
	</view>
</template>


<script lang="ts">
import { Vue, Component, Prop, Watch, Emit, Ref } from "vue-property-decorator";
import { Action, Mutation, State } from "vuex-class";
import tuiListCell from "@/components/thorui/tui-list-cell/tui-list-cell.vue";
import tuiIcon from "@/components/thorui/tui-icon/tui-icon.vue";
import tuiButton from "@/components/thorui/tui-button/tui-button.vue";
import tuiTips from "@/components/thorui/tui-tips/tui-tips.vue";
import uniHelper, { thorUiHelper } from '../../common/uniHelper';
import { RegisterWeChatUserDto, RegisterResultDto, LoginDto, LoginResultDto, WeChatLoginDto } from '../../models/apiModel';
import { MiCakeApiModel } from '../../common/environment';
import { UserStoreKey } from '../../store/store-keys';

const namespace = UserStoreKey.nameSpace;

@Component({
	components: {
		tuiListCell,
		tuiIcon,
		tuiButton,
		tuiTips
	},
})
export default class extends Vue {
	private _disabled: boolean = false;
	public get disabled(): boolean {
		return this._disabled;
	}

	public mobile: string = "";
	public password: string = "";
	public code: string = '';
	public isSend: boolean = false;
	public btnSendText?: string = '获取验证码';

	private sessionKey: string = '';

	@Action(UserStoreKey.actions_loginSuccess, { namespace }) public loginSuccess!: Function;
	@Mutation(UserStoreKey.mutations_saveUserInfo, { namespace }) public saveUserInfo!: Function;
	
	public onLoad(options: any) {
		console.log(options.key);
		this.sessionKey = options.key;
	}

	public back() {
		uni.navigateBack();
	}

	public inputCode(e: any) {
		this.code = e.detail.value;
	}
	public inputMobile(e: any) {
		this.mobile = e.detail.value;
	}

	public inputPwd(e: any) {
		this.password = e.detail.value;
	}

	public clearInput(type: number) {
		if (type == 1) {
			this.mobile = '';
		} else {
			this.password = '';
		}
	}

	public sendCode() {
		this.isSend = true;

		thorUiHelper.showTips(this.$refs.toast, '直接输入 micake 就行啦~', 2000, 'green');

		setTimeout(() => {
			this.isSend = false;
		}, 5000);
	}

	public protocol() {
		thorUiHelper.showTips(this.$refs.toast, '这是一个不可告人的秘密哟~', 2000, 'green');
	}

	public async registerWechatUser() {
		if (uniHelper.validator.isNullOrEmpty(this.sessionKey)) {
			thorUiHelper.showTips(this.$refs.toast, '抱歉出现了一个问题，导致我没有办法帮助您创建账户~');
			return;
		}
		if (!uniHelper.validator.isMobile(this.mobile)) {
			thorUiHelper.showTips(this.$refs.toast, '貌似手机号码不正确呀~');
			return;
		}
		if (uniHelper.validator.isNullOrEmpty(this.code)) {
			thorUiHelper.showTips(this.$refs.toast, '貌似还没有输入验证码呀~');
			return;
		}

		let dto = new RegisterWeChatUserDto();
		dto.sessionKey = this.sessionKey;
		dto.name = 'MiCaker';
		dto.phone = this.mobile;

		try {
			uniHelper.showLoading();
			let result = await this.$httpClient.post<MiCakeApiModel<RegisterResultDto>>('/WeChatUser/RegisterUser', dto);
			await uniHelper.hideLoading(2000);

			if (result.isError) {
				thorUiHelper.showTips(this.$refs.toast, result.message ?? "存在错误哦~");
				return;
			}

			if (result.result!.success) {
				uniHelper.showLoading('注册成功，正在拼命为您登录中~');

				let result = await this.$httpClient.get<MiCakeApiModel<WeChatLoginDto>>('/WeChatUser/Login', { 'key': this.sessionKey });
				await uniHelper.hideLoading(2000);

				if (result.isError) {
					thorUiHelper.showTips(this.$refs.toast, result.message ?? "存在错误哦~");
					return;
				}

				var loginResult = result.result!;
				if (!loginResult.hasUser) {
					thorUiHelper.showTips(this.$refs.toast, '抱歉，我们遇到了一个错误，您能重新操作一次吗~');
					return;
				}

				this.loginSuccess(result.result!.accessToken);
				let userInfo = result.result!.userInfo!;
				this.saveUserInfo({ id: userInfo.id, name: userInfo.name, avatar: userInfo.avatar, mobile: this.mobile });

				uni.switchTab({
					url: '/pages/my/my'
				}); //回跳'我的'页面
			}
		} catch (error) {
			console.log(error);
		}
	}
}
</script>

<style lang="scss" scoped>
.container {
	.tui-page-title {
		width: 100%;
		font-size: 48rpx;
		font-weight: bold;
		color: $uni-text-color;
		line-height: 42rpx;
		padding: 110rpx 40rpx 40rpx 40rpx;
		box-sizing: border-box;
	}
	.tui-form {
		padding-top: 50rpx;
		.tui-view-input {
			width: 100%;
			box-sizing: border-box;
			padding: 0 40rpx;
			.tui-cell-input {
				width: 100%;
				display: flex;
				align-items: center;
				padding-top: 48rpx;
				padding-bottom: $uni-spacing-col-base;
				input {
					flex: 1;
					padding-left: $uni-spacing-row-base;
				}
				.tui-icon-close {
					margin-left: auto;
				}
				.tui-btn-send {
					width: 156rpx;
					text-align: right;
					flex-shrink: 0;
					font-size: $uni-font-size-base;
					color: $uni-color-primary;
				}
				.tui-gray {
					color: $uni-text-color-placeholder;
				}
			}
		}
		.tui-cell-text {
			width: 100%;
			padding: 40rpx $uni-spacing-row-lg;
			box-sizing: border-box;
			font-size: $uni-font-size-sm;
			color: $uni-text-color-grey;
			display: flex;
			align-items: center;
			.tui-color-primary {
				color: $uni-color-primary;
				padding-left: $uni-spacing-row-sm;
			}
		}
		.tui-btn-box {
			width: 100%;
			padding: 0 $uni-spacing-row-lg;
			box-sizing: border-box;
			margin-top: 80rpx;
		}
	}
}
</style>


