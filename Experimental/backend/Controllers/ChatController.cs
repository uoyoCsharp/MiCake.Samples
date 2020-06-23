using System;
using System.Collections.Generic;
using MiCake.AspNetCore.Security;
using MiCakeDemoApplication.Dto.Chat;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MiCakeDemoApplication.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ChatController : Controller
    {
        public ChatController()
        {
        }

        //当使用了CurrentUser特性的时候，则表明该action需要进行权限验证.
        [HttpGet]
        [Authorize]
        public List<ChatMsgItem> GetMsg([CurrentUser]Guid userId)
        {
            return GetFakeChatMsg();
        }


        private List<ChatMsgItem> GetFakeChatMsg()
        {
            var myId = 1;
            var friendId = 2;

            var chatMessages = new List<ChatMsgItem>();

            chatMessages.Add(new ChatMsgItem()
            {
                Type = 1,
                UserId = friendId,
                Text = "当您看到这条消息的时候，证明您已经在服务端验证成功了"
            });
            chatMessages.Add(new ChatMsgItem()
            {
                Type = 1,
                UserId = friendId,
                Text = "我们不能仅仅通过前端来进行保护API"
            });
            chatMessages.Add(new ChatMsgItem()
            {
                Type = 1,
                UserId = friendId,
                Text = "因为可以通过Fiddler或者PostMan等工具进行提交API"
            });
            chatMessages.Add(new ChatMsgItem()
            {
                Type = 1,
                UserId = myId,
                Text = "哦，好的。"
            });
            chatMessages.Add(new ChatMsgItem()
            {
                Type = 1,
                UserId = friendId,
                Text = "而有的API仅仅只能当前用户访问，比如现在我们的聊天，我们根据当前传入的用户id来获取该用户的对话记录"
            });
            chatMessages.Add(new ChatMsgItem()
            {
                Type = 1,
                UserId = friendId,
                Text = "有的不法分子通过抓包，传入了其它用户的id就得到了其它用户的聊天记录，这样信息就泄露了"
            });
            chatMessages.Add(new ChatMsgItem()
            {
                Type = 1,
                UserId = friendId,
                Text = "所以我们千万要避免这种情况，要对当前传入的ID进行验证"
            });
            chatMessages.Add(new ChatMsgItem()
            {
                Type = 1,
                UserId = friendId,
                Text = "放心,MiCake提供了[CurrentUser]的特性，当在Action中使用了该标记，那么将进行自动验证"
            });
            chatMessages.Add(new ChatMsgItem()
            {
                Type = 1,
                UserId = myId,
                Text = "咦~还有这么好的事情？ 我刚刚用MiCake来生成的JwtToken，感觉也很简单"
            });
            chatMessages.Add(new ChatMsgItem()
            {
                Type = 1,
                UserId = friendId,
                Text = "对呀，它不仅仅提供了DDD的功能，还对AspnetCore的一些基础功能进行了扩展"
            });
            chatMessages.Add(new ChatMsgItem()
            {
                Type = 1,
                UserId = friendId,
                Text = "https://github.com/uoyoCsharp/MiCake，去给它点个star吧！"
            });

            return chatMessages;
        }
    }
}