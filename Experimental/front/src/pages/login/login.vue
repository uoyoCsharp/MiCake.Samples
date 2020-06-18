<template>
	<view class="container">
		<!-- #ifndef MP-WEIXIN -->
		<view class="tui-status-bar"></view>
		<view class="tui-header">
			<view>MiCake 实验性案例</view>
			<tui-icon name="shut" :size="26" @click="back"></tui-icon>
		</view>
		<!-- #endif -->
		<view class="tui-page-title">登录</view>
		<view class="tui-form">
			<view class="tui-view-input">
				<tui-list-cell :hover="false" :lineLeft="false" backgroundColor="transparent">
					<view class="tui-cell-input">
						<tui-icon name="mobile" color="#6d7a87" :size="20"></tui-icon>
						<input :adjust-position="false" :value="mobile" placeholder="请输入手机号" placeholder-class="tui-phcolor" type="number" maxlength="11" @input="inputMobile" />
						<view class="tui-icon-close" v-show="mobile" @tap="clearInput(1)">
							<tui-icon name="close-fill" :size="16" color="#bfbfbf"></tui-icon>
						</view>
					</view>
				</tui-list-cell>
				<tui-list-cell :hover="false" :lineLeft="false" backgroundColor="transparent">
					<view class="tui-cell-input">
						<tui-icon name="pwd" color="#6d7a87" :size="20"></tui-icon>
						<input :adjust-position="false" :value="password" placeholder="请输入密码" :password="true" placeholder-class="tui-phcolor" type="text" maxlength="36" @input="inputPwd" />
						<view class="tui-icon-close" v-show="password" @tap="clearInput(2)">
							<tui-icon name="close-fill" :size="16" color="#bfbfbf"></tui-icon>
						</view>
					</view>
				</tui-list-cell>
			</view>
			<view class="tui-cell-text">
				<view class="tui-color-primary" hover-class="tui-opcity" :hover-stay-time="150" @tap="href(1)">忘记密码？</view>
				<view hover-class="tui-opcity" :hover-stay-time="150">
					没有账号？
					<text class="tui-color-primary" @tap="href(2)">注册</text>
				</view>
			</view>
			<view class="tui-btn-box">
				<tui-button :disabledGray="true" :disabled="disabled" :shadow="true" shape="circle">登录</tui-button>
			</view>
		</view>
		<view class="tui-login-way" v-if="!popupShow">
			<view hover-class="tui-opcity" :hover-stay-time="150" @tap="showOtherLogin">其他方式登录</view>
		</view>
		<tui-bottom-popup :mask="false" backgroundColor="transparent" :show="popupShow">
			<view class="tui-auth-login">
				<!-- #ifdef APP-PLUS || MP-WEIXIN || H5 -->
				<view class="tui-icon-platform" hover-class="tui-opcity" :hover-stay-time="150">
					<image src="/static/images/share/icon_wechat.png" class="tui-login-logo" />
				</view>
				<!-- #endif -->
			</view>
		</tui-bottom-popup>
	</view>
</template>

<script lang="ts">
import { Vue, Component, Prop, Watch, Emit, Ref } from "vue-property-decorator";
import tuiListCell from "@/components/thorui/tui-list-cell/tui-list-cell.vue";
import tuiIcon from "@/components/thorui/tui-icon/tui-icon.vue";
import tuiButton from "@/components/thorui/tui-button/tui-button.vue";
import tuiBottomPopup from "@/components/thorui/tui-bottom-popup/tui-bottom-popup.vue";

@Component({
	components: {
		tuiListCell,
		tuiIcon,
		tuiButton,
		tuiBottomPopup
	},
})
export default class extends Vue {

	private _disabled: boolean = false;
	public get disabled(): boolean {
		return this._disabled;
	}

	public mobile: string = "";
	public password: string = "";
	public popupShow: boolean = false;

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
	public href(type: number) {
		let url = '../forgetPwd/forgetPwd';
		if (type == 2) {
			url = '../reg/reg';
		}
		uni.navigateTo({
			url: url
		})
	}

	public showOtherLogin() {
		//打开后 不再关闭
		this.popupShow = true;
	}

	public back() {
		uni.navigateBack({
			animationType:'pop-out',
			animationDuration:300
		});
	}
}
</script>

<style lang="scss">
.container {
	.tui-status-bar {
		width: 100%;
		height: var(--status-bar-height);
	}

	.tui-header {
		width: 100%;
		padding: 40rpx;
		display: flex;
		align-items: center;
		justify-content: space-between;
		box-sizing: border-box;
	}

	.tui-page-title {
		width: 100%;
		font-size: 48rpx;
		font-weight: bold;
		color: $uni-text-color;
		line-height: 42rpx;
		padding: 40rpx;
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
			}
		}

		.tui-cell-text {
			width: 100%;
			padding: $uni-spacing-col-lg $uni-spacing-row-lg;
			box-sizing: border-box;
			font-size: $uni-font-size-sm;
			color: $uni-text-color-grey;
			display: flex;
			align-items: center;
			justify-content: space-between;

			.tui-color-primary {
				color: $uni-color-primary;
			}
		}

		.tui-btn-box {
			width: 100%;
			padding: 0 $uni-spacing-row-lg;
			box-sizing: border-box;
			margin-top: 80rpx;
		}
	}

	.tui-login-way {
		width: 100%;
		font-size: 26rpx;
		color: $uni-color-primary;
		display: flex;
		justify-content: center;
		position: fixed;
		left: 0;
		bottom: 80rpx;

		view {
			padding: 12rpx 0;
		}
	}

	.tui-auth-login {
		width: 100%;
		display: flex;
		align-items: center;
		justify-content: center;
		padding-bottom: 80rpx;
		padding-top: 20rpx;

		.tui-icon-platform {
			width: 90rpx;
			height: 90rpx;
			display: flex;
			align-items: center;
			justify-content: center;
			position: relative;

			&::after {
				content: "";
				position: absolute;
				width: 200%;
				height: 200%;
				transform-origin: 0 0;
				transform: scale(0.5, 0.5) translateZ(0);
				box-sizing: border-box;
				left: 0;
				top: 0;
				border-radius: 180rpx;
				border: 1rpx solid $uni-text-color-placeholder;
			}
		}

		.tui-login-logo {
			width: 60rpx;
			height: 60rpx;
		}
	}
}
</style>
