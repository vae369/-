using Mir.Models.Forum;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace Mir2.Helper
{
    public interface IForumBase
    {

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userName">帐号</param>
        /// <param name="passWord">密码</param>
        /// <param name="verCode">验证码</param>
        /// <returns></returns>
        int Login(string userName, string passWord, string verCode);

        /// <summary>
        /// 退出
        /// </summary>
        bool Logout(Topic topic);

        /// <summary>
        /// 获取验证码
        /// </summary>
        Bitmap GetVerCode();

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        UserInfo GetUserInfo(UserInfo user);

        /// <summary>
        /// 回帖
        /// </summary>
        string PublishReply(Topic topic, UserInfo user, string message, bool IsForumReply);

        /// <summary>
        /// 自动签到
        /// </summary>
        bool AutoSingIn(UserInfo user);


    }
}
