<template>
	<view class="container">
		<!--tabbar-->
		<!--searchbox-->
		<view class="tui-searchbox">
			<view class="tui-search-input" @tap="search">
				<icon type="search" size="15" color="#999"></icon>
				<text class="tui-search-text">搜索</text>
			</view>
		</view>
		<!--searchbox-->

		<block v-for="(item,index) in msgList" :key="index">
			<tui-list-cell @click="detail" :unlined="true" @tap="goChatDetail(item)">
				<view class="tui-chat-item">
					<view class="tui-msg-box">
						<image :src="'/static/logo.png'" class="tui-msg-pic" mode="widthFix" />
						<view class="tui-msg-item">
							<view class="tui-msg-name">{{item.nickname}}</view>
							<view class="tui-msg-content">{{item.msg}}</view>
						</view>
					</view>
					<view class="tui-msg-right" :class="[item.level==3?'tui-right-dot':'']">
						<view class="tui-msg-time">{{item.time}}</view>
						<tui-badge :type="item.level==2?'gray':'danger'" :dot="item.level==3?true:false" v-if="item.msgNum>0">{{item.level!=3?item.msgNum:""}}</tui-badge>
					</view>
				</view>
			</tui-list-cell>
		</block>
		<view class="tui-safearea-bottom"></view>

		<!--居中消息-->
		<tui-tips position="center" ref="toast"></tui-tips>
	</view>
</template>

<script lang="ts">
import { Vue, Component, Prop, Watch, Emit, Ref } from "vue-property-decorator";
import { Action, Mutation, State } from "vuex-class";

import tuiIcon from "@/components/thorui/tui-icon/tui-icon.vue";
import tuiBadge from "@/components/thorui/tui-badge/tui-badge.vue";
import tuiListCell from "@/components/thorui/tui-list-cell/tui-list-cell.vue";
import tuiTips from "@/components/thorui/tui-tips/tui-tips.vue";
import uniHelper, { thorUiHelper } from '../../common/uniHelper';
import { UserStoreKey } from '../../store/store-keys';

const namespace = UserStoreKey.nameSpace;

@Component({
	components: {
		tuiIcon,
		tuiBadge,
		tuiListCell,
		tuiTips
	}
})
export default class HomePage extends Vue {
	@State(UserStoreKey.state_isLogin, { namespace }) public isLogin!: boolean;

	public current: number = 0;

	public msgList: MsgItem[] = [{
		nickname: "MiCake酱",
		pic: "",
		msg: '这将使用前端验证',
		msgNum: 2,
		time: "10:22",
		level: 1,
		frontAuth: true,
	}, {
		nickname: "MiCake酱",
		pic: "",
		msg: "这将使用AspNetCore后端验证",
		msgNum: 2,
		time: "13:27",
		level: 3,
		frontAuth: false,
	}];

	public search() { }

	public detail() { }

	public tabbarSwitch() { }

	public onPullDownRefresh() {
		uniHelper.showToast('稍等~');
		uni.stopPullDownRefresh();
	}

	public goChatDetail(item: MsgItem) {
		if (this.isLogin) {
			uni.navigateTo({
				url: `/pages/chat/chat?frontAuth=${item.frontAuth ? 1 : 0}`
			});
		} else {
			thorUiHelper.showTips(this.$refs.toast, '亲，貌似您还没有登录呢~');
		}
	}
}

class MsgItem {
	nickname: string = '';
	pic: string = '';
	msg: string = '';
	msgNum: number = 0;
	time: string = '';
	level: number = 0;
	frontAuth: Boolean = false;
}
</script>

<style>
.container {
	padding-bottom: 100rpx;
}

/*tabbar*/

.tui-tabbar {
	width: 100%;
	position: fixed;
	display: flex;
	align-items: center;
	justify-content: space-between;
	z-index: 99999;
	background-color: #fff;
	height: 100rpx;
	left: 0;
	bottom: 0;
	padding-bottom: env(safe-area-inset-bottom);
}

.tui-safearea-bottom {
	width: 100%;
	height: env(safe-area-inset-bottom);
}

.tui-tabbar::before {
	content: "";
	width: 100%;
	border-top: 1rpx solid #d2d2d2;
	position: absolute;
	top: 0;
	left: 0;
	-webkit-transform: scaleY(0.5);
	transform: scaleY(0.5);
}

.tui-tabbar-item {
	flex: 1;
	width: 25%;
	display: flex;
	align-items: center;
	flex-direction: column;
	justify-content: space-between;
	font-size: 24rpx;
	color: #666;
	height: 80rpx;
}

.tui-ptop-4 {
	padding-top: 4rpx;
}

.tui-scale {
	font-weight: bold;
	transform: scale(0.8);
	transform-origin: center 100%;
	line-height: 30rpx;
}

.tui-item-active {
	color: #00c0fb !important;
}

/*tabbar*/

/*searchbox*/

.tui-searchbox {
	width: 100%;
	height: 100rpx;
	padding: 0 30rpx;
	box-sizing: border-box;
	background: #fff;
	display: flex;
	align-items: center;
	justify-content: center;
	position: relative;
}

.tui-search-input {
	width: 100%;
	height: 72rpx;
	background: #fafafa;
	border-radius: 36rpx;
	font-size: 30rpx;
	color: #a8abb8;
	display: flex;
	align-items: center;
	justify-content: center;
}

.tui-search-text {
	padding-left: 16rpx;
}

/*searchbox*/

.tui-chat-item {
	width: 100%;
	display: flex;
	align-items: center;
	justify-content: space-between;
}

.tui-msg-box {
	display: flex;
	align-items: center;
}

.tui-msg-pic {
	width: 100rpx;
	height: 100rpx;
	border-radius: 50%;
	display: block;
	margin-right: 24rpx;
}

.tui-msg-item {
	max-width: 500rpx;
	min-height: 80rpx;
	overflow: hidden;
	display: flex;
	flex-direction: column;
	justify-content: space-between;
}

.tui-msg-name {
	overflow: hidden;
	white-space: nowrap;
	text-overflow: ellipsis;
	font-size: 34rpx;
	line-height: 1;
	color: #262b3a;
}

.tui-msg-content {
	overflow: hidden;
	white-space: nowrap;
	text-overflow: ellipsis;
	font-size: 28rpx;
	line-height: 1;
	color: #9397a4;
}

.tui-msg-right {
	max-width: 120rpx;
	height: 88rpx;
	margin-left: auto;
	text-align: right;
	display: flex;
	flex-direction: column;
	justify-content: space-between;
	align-items: flex-end;
}

.tui-msg-right.tui-right-dot {
	height: 76rpx;
	padding-bottom: 10rpx;
}

.tui-msg-time {
	width: 100%;
	font-size: 24rpx;
	line-height: 24rpx;
	color: #9397a4;
}
</style>
