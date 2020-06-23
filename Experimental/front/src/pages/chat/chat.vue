<template>
	<view class="container">
		<!--tabbar-->

		<!--tabbar-->
		<view class="tui-chat-content">
			<tui-loadmore v-if="loadding" :index="3" type="primary" text=" "></tui-loadmore>
			<view class="tui-label">对方已通过您的好友请求</view>
			<view class="tui-chat-center">星期四 11:02</view>
			<view v-show="show" v-for="msgItem in messages" :key="msgItem.text">
				<view v-if="msgItem.userId !== myId" class="tui-chat-left">
					<image src="/static/logo.png" class="tui-user-pic tui-right" />
					<view class="tui-chatbox tui-chatbox-left">{{msgItem.text}}</view>
				</view>
				<view v-else class="tui-chat-right">
					<view class="tui-chatbox tui-chatbox-right">{{msgItem.text}}</view>
					<image src="/static/logo.png" class="tui-user-pic tui-left" />
				</view>
			</view>

			<view class="tui-chat-right">
				<view class="tui-flex-center tui-flex-end tui-flex-reverse">
					<view class="tui-img-chatbox">
						<image src="/static/logo.png" class="tui-chat-img" mode="widthFix" />
					</view>
					<image src="/static/images/chat/fail.png" class="tui-chat-fail tui-mr" />
				</view>
				<image src="/static/logo.png" class="tui-user-pic tui-left" />
			</view>
		</view>
		<t-chat-bar></t-chat-bar>
	</view>
</template>

<script lang="ts">
import tChatBar from '@/components/views/t-chat-bar/t-chat-bar.vue';
import tuiBadge from "@/components/thorui/tui-badge/tui-badge.vue";
import tuiLoadmore from "@/components/thorui/tui-loadmore/tui-loadmore.vue";
import { Action, Mutation, State } from "vuex-class";

import { Vue, Component, Prop, Watch, Emit, Ref } from "vue-property-decorator";
import { ChatMsgItem } from '../../models/apiModel';
import { UserStoreKey } from '../../store/store-keys';
import { MiCakeApiModel } from '../../common/environment';
import uniHelper from '../../common/uniHelper';

const namespace = UserStoreKey.nameSpace;

@Component({
	components: {
		tChatBar,
		tuiBadge,
		tuiLoadmore
	}
})
export default class extends Vue {
	public loadding: Boolean = false;
	public myId: number = 1;
	public show: Boolean = true;
	public bottom: number = 0;
	public messages: ChatMsgItem[] = [];

	@State(UserStoreKey.state_userId, { namespace }) public userId!: string;

	public async onLoad(options: any) {
		if (options.frontAuth == true) {
			this.messages = this.getFrontMessages();
		} else {
			this.messages = await this.getEndMessages();
		}
	}

	public onPageScroll(e: any) {
		if (e.scrollTop == 0 && !this.loadding) {
			this.loadding = true;
			setTimeout(() => {
				this.show = true;
				this.loadding = false;
			}, 1000);
		}
	}

	private async getEndMessages(): Promise<ChatMsgItem[]> {
		try {
			var result = await this.$httpClient.get<MiCakeApiModel<ChatMsgItem[]>>('/Chat/GetMsg', { 'userId': this.userId });

			if (!result.isError) {
				return result!.result!;
			}
			else {
				uniHelper.showToast('啊哦，出错了');
			}
		} catch{
			uniHelper.showToast('啊哦，出错了');
		}
		return [];
	}

	private getFrontMessages(): ChatMsgItem[] {
		let msgItems: ChatMsgItem[] = JSON.parse(`[{
				"type": 1,
				"userId": 2,
				"text": "当您看到这条消息的时候，证明您通过的是前端验证",
				"isRead": false,
				"sendSuccess": false
			},
			{
				"type": 1,
				"userId": 2,
				"text": "现在显示的是前端本地的消息",
				"isRead": false,
				"sendSuccess": false
			},
			{
				"type": 1,
				"userId": 2,
				"text": "很多时候，我们会通过前端的路由守卫等方式来进行避免用户访问某些页面",
				"isRead": false,
				"sendSuccess": false
			},
			{
				"type": 1,
				"userId": 1,
				"text": "哦，好的。",
				"isRead": false,
				"sendSuccess": false
			},
			{
				"type": 1,
				"userId": 2,
				"text": "但是，您要知道，完全靠前端屏蔽页面显示来达到保护数据的方式是很危险的",
				"isRead": false,
				"sendSuccess": false
			},
			{
				"type": 1,
				"userId": 2,
				"text": "有的不法分子可以通过探测API和抓包等方式获取到API进行访问",
				"isRead": false,
				"sendSuccess": false
			},
			{
				"type": 1,
				"userId": 2,
				"text": "所以我们此时也要在后端进行验证",
				"isRead": false,
				"sendSuccess": false
			},
			{
				"type": 1,
				"userId": 2,
				"text": "那么点击另外一个聊天窗口，来尝试一下服务端验证吧",
				"isRead": false,
				"sendSuccess": false
			}]`);

		return msgItems;
	}
}
</script>

<style>
.container {
	padding-left: 20rpx;
	padding-right: 20rpx;
	padding-bottom: 146rpx;
	box-sizing: border-box;
}

/*chatbox*/
.tui-chat-content {
	width: 100%;
}

.tui-chatbox {
	max-width: 66%;
	border-radius: 10rpx;
	position: relative;
	padding: 20rpx 22rpx;
	font-size: 32rpx;
	color: #333;
	word-break: break-all;
	word-wrap: break-word;
}

.tui-chatbox::before {
	content: "";
	position: absolute;
	width: 0;
	height: 0;
	top: 20rpx;
	border: 16rpx solid;
}

.tui-chatbox-left {
	background: #fff;
	border: 1rpx solid #fff;
	display: inline-block;
}

.tui-chatbox-left::before {
	right: 100%;
	border-color: transparent #fff transparent transparent;
}

.tui-chatbox-right {
	background: #a0d5f3;
	border: 1rpx solid #a0d5f3;
}

.tui-chatbox-right::before {
	left: 100%;
	border-color: transparent transparent transparent #a0d5f3;
}

/*chatbox*/

.tui-chat-left,
.tui-chat-right {
	display: flex;
	align-items: flex-start;
	padding-top: 36rpx;
}

.tui-user-pic {
	width: 80rpx;
	height: 80rpx;
	flex-shrink: 0;
	border-radius: 50%;
	display: block;
}

.tui-left {
	margin-left: 26rpx;
}

.tui-right {
	margin-right: 26rpx;
}

.tui-chat-right {
	justify-content: flex-end;
}

.tui-chat-center {
	display: flex;
	align-items: center;
	justify-content: center;
	height: 28rpx;
	font-size: 28rpx;
	color: #666;
	padding-top: 36rpx;
}

.tui-label {
	display: inline-block;
	background: rgba(0, 0, 0, 0.4);
	color: #fff;
	font-size: 24rpx;
	line-height: 24rpx;
	margin-top: 36rpx;
	padding: 12rpx 16rpx;
	text-align: center;
	border-radius: 8rpx;
	margin-left: 50%;
	transform: translateX(-50%);
}

.tui-img-chatbox {
	position: relative;
}

.tui-img-chatbox::after {
	content: "";
	position: absolute;
	height: 200%;
	width: 200%;
	border: 1rpx solid #eaeef1;
	transform-origin: 0 0;
	-webkit-transform-origin: 0 0;
	-webkit-transform: scale(0.5);
	transform: scale(0.5);
	left: 0;
	top: 0;
	border-radius: 20rpx;
}

.tui-chat-img {
	max-width: 200rpx;
	/* min-height: 80rpx; */
	display: block;
	border-radius: 10rpx;
}

.tui-chat-flex {
	display: flex;
	align-items: center;
}

.tui-flex-center {
	display: flex;
	align-items: center;
}

.tui-chat-voice {
	width: 40rpx;
	height: 40rpx;
	display: block;
	flex-shrink: 0;
}

.tui-rotate {
	transform: rotate(180deg);
}

.tui-chat-fail {
	width: 50rpx;
	height: 50rpx;
	display: block;
	flex-shrink: 0;
}

.tui-mr {
	margin-right: 16rpx;
}

.tui-ml {
	margin-left: 16rpx;
}

.tui-flex-end {
	justify-content: flex-end;
}

.tui-flex-reverse {
	flex-direction: row-reverse;
}
</style>
