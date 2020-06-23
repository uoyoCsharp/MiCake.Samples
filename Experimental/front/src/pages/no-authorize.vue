<template>
	<view class="container">
		<tui-no-data :fixed="true" imgUrl="/static/logo.png" btnText="去登录" @click="goLogin">看起来您并没有权限来访问当前页面呀~</tui-no-data>
	</view>
</template>

<script lang="ts">
import tuiNoData from "@/components/thorui/tui-no-data/tui-no-data.vue";
import tuiButton from "@/components/thorui/tui-button/tui-button.vue";
import { State, Action } from "vuex-class";
import { Vue, Component, Prop, Watch, Emit, Ref } from "vue-property-decorator";
import { UserStoreKey } from '../store/store-keys';

const namespace = UserStoreKey.nameSpace;

@Component({
	components: {
		tuiNoData,
		tuiButton
	}
})
export default class extends Vue {

	@Action(UserStoreKey.actions_loginOut, { namespace })
	public loginOutAction!: Function;

	onLoad() {
		uni.hideLoading();
	}

	public goLogin() {
		this.loginOutAction();

		uni.navigateTo({
			url: '/pages/login/login'
		});
	}
}
</script>

<style>
.container {
	padding: 20rpx 0 120rpx 0;
	box-sizing: border-box;
}

.header {
	padding: 80rpx 90rpx 60rpx 90rpx;
	box-sizing: border-box;
}

.title {
	font-size: 34rpx;
	color: #333;
	font-weight: 500;
}

.sub-title {
	font-size: 24rpx;
	color: #7a7a7a;
	padding-top: 18rpx;
}

.tui-btn-box {
	padding: 10rpx 40rpx;
	box-sizing: border-box;
}
</style>
