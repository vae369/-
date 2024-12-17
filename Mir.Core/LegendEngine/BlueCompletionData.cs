using Mir.Core.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mir.Core.LegendEngine
{
    public class BlueCompletionData
    {
        public static List<CompletionData> CompletionDataList = new List<CompletionData>();

        public static Dictionary<string, string> BlueCheckedData = new Dictionary<string, string>();
        static BlueCompletionData()
        {
            CompletionData cd = null;
            try
            {
                if (CompletionDataList.Count == 0 || BlueCheckedData.Count == 0)
                {
                    cd = new CompletionData("检测人物宝宝名字", "[@main]\r\n#IF\r\nCHECKSLAVENAME GameOfMir\r\n#ACT\r\nSENDMSG 5 提示:你的宝宝叫GameOfMir\r\n#ELSEACT\r\nSENDMSG 5 提示:你的宝宝不叫GameOfMir");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检测人物宝宝名字", "CHECKSLAVENAME");

                    cd = new CompletionData("检测是否在摆摊", "#IF\r\nCHECKSHOPSTALLSTATUS\r\n#SAY\r\n正在摆摊\r\n#ELSE\r\n没有摆摊");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检测是否在摆摊", "CHECKSHOPSTALLSTATUS");

                    cd = new CompletionData("检查人物的荣誉值一", "#IF\r\nCHECKGAMEGLORY &gt; 100\r\n#SAY\r\n你的荣誉值大于100点.");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查人物的荣誉值一", "CHECKGAMEGLORY");

                    cd = new CompletionData("检查人物的荣誉值二", "#IF\r\nCHECKNATIONCREDIT &gt; 100\r\n#SAY\r\n你的荣誉值大于100点.");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查人物的荣誉值二", "CHECKNATIONCREDIT");
                      
                    cd = new CompletionData("检测人物是否是离线挂机", "功能:检测人物是否是离线挂机");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检测人物是否是离线挂机", "CHECKOFFLINE");

                    cd = new CompletionData("检测人物是否佩带指定物品", "[@TEST]\r\n#IF\r\nCHECKITEMW  力量戒指 1\r\n#elsesay\r\n你身上没有力量戒指\\ \\\r\n#elseact\r\nbreak");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检测人物是否佩带指定物品", "CHECKITEMW");

                    cd = new CompletionData("检测人物元素属性", "(0)暴击几率增加 1~100%\r\n(1)增加攻击伤害 1~100%\r\n(2)物理伤害减少 1~100%\r\n(3)魔法伤害减少 1~100%\r\n(4)忽视目标防御 1~100%\r\n(5)所有伤害反弹 1~100%\r\n(6)增加目标暴率 1~100%\r\n(7)人物体力增加 1~100%\r\n(8)人物魔力增加 1~100%\r\n(9)怒气恢复增加 1~100%\r\n(10)合击攻击增加 1~100%\r\n[@main]\r\n#IF\r\nCHECKNEWITEMVALUE 0 0 &gt; 10\r\n#SAY\r\n暴击几率大于10%");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检测人物元素属性", "CHECKNEWITEMVALUE");

                    cd = new CompletionData("检测人物PK值", "[@Main]\r\n#IF\r\nCHECKPKPOINTEX  &gt; 100\r\n#SAY\r\nPK值大于 100\r\n#ELSESAY\r\nPK值小于 100");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检测人物PK值", "CHECKPKPOINTEX");
                    

                    cd = new CompletionData("检查人物称号数量", "[@检查称号数量]\r\n#IF\r\nCHECKFENGHAOCOUNT &gt; 29\r\n#ACT\r\nSENDMSG 6 已经有了所有称号");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查人物称号数量", "CHECKFENGHAOCOUNT");
                    
                    cd = new CompletionData("检查装备绑定状态", "0 禁止扔 1 禁止交易 2 禁止存 3 禁止修 4 禁止出售 5 禁止爆出 6 丢弃消失\r\n[@衣服禁止扔]\r\n#IF\r\nCheckItemBind 0\r\n#ACT\r\nGOTO @禁止扔\r\n#ELSEACT\r\nSENDMSG 6 请先绑定\r\n\r\n[@禁止扔]\r\n#IF\r\nCheckItemState 0 0\r\n#ACT\r\nSENDMSG 6 该装备已经设置过禁止扔\r\n#ELSEACT\r\nSetItemState 0 0 1\r\nSENDMSG 6 该装备禁止扔");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查装备绑定状态", "CHECKITEMSTATE");

                    cd = new CompletionData("检查字符串是否在文件中", "CheckTextList 文件路径 字符串\r\n\r\n\r\n检查字符串是否在指定文件中 (完整字符串或包含字符)\r\n\r\n示例\r\n\r\n[@CheckTextList]\r\n\r\n#IF\r\nCheckTextList ..\\QuestDiary\\StrList.txt &lt;$STR(S2)&gt;\r\n#SAY\r\n列表里的某一行字符包含&lt;$STR(S2)&gt;\r\n#ELSESAY\r\n某一行字符包含&lt;$STR(S2)&gt;");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查字符串是否在文件中", "CHECKTEXTLIST");

                    cd = new CompletionData("取反", "翻转值！\r\n\r\n实例\r\n\r\n#IF\r\nNOT INSAFEZONE\r\n#SAY\r\n需要在安全区使用摆摊功能！\r\n#ACT\r\nForbidMyShop");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("取反", "NOT");

                    cd = new CompletionData("检测当前物品是否已经绑定", "CheckItemBind 装备位置(-1~13,-1时为OK框中物品)\r\n使用示例\r\n\r\n#IF\r\nCheckItemBind 1\r\n#SAY\r\n你的武器已经绑定");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检测当前物品是否已经绑定", "CHECKITEMBIND");

                    cd = new CompletionData("检测行会成员人数", "CheckGuildMemberCount 控制符 &lt;.=.&gt;  数量\r\n\r\n使用示例 #IF\r\nCheckGuildMemberCount  &gt; 100\r\n#SAY\r\n行会成员大于100人.");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检测行会成员人数", "CHECKGUILDMEMBERCOUNT");

                    cd = new CompletionData("检测第四个连击技能是否开启", "使用示例\r\n\r\n[@main]\r\n#if\r\nCHECKOPENLASTSKILL\r\n#say\r\n第四个连击技能已经开启\r\n#elseact\r\nOPENLASTSKILL");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检测第四个连击技能是否开启", "CHECKOPENLASTSKILL");

                    cd = new CompletionData("检测行会建筑度", "格式： CHECKGUILDBUILDPOINT  控制符（&lt;&gt;=）数字");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检测行会建筑度", "CHECKGUILDBUILDPOINT");

                    cd = new CompletionData("检测行会行会人气度", "格式： CHECKGUILDAURAEPOINT 控制符（&lt;&gt;=）数字");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检测行会行会人气度", "CHECKGUILDAURAEPOINT");

                    cd = new CompletionData("检测行会安定度", "格式： CHECKGUILDSTABILITYPOINT 控制符（&lt;&gt;=）数字");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检测行会安定度", "CHECKGUILDSTABILITYPOINT");

                    cd = new CompletionData("检测行会繁荣度", "格式: CHECKGUILDFLOURISHPOINT 控制符（&lt;&gt;=）数字");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检测行会繁荣度", "CHECKGUILDFLOURISHPOINT");

                    cd = new CompletionData("检查地图内的人物数量", "[@Main]\r\n#if\r\nCheckMapHumanCount  3  &lt; 100\r\n#say\r\n地图3内人数小于100人\r\n#elsesay\r\n地图3内人数多于100人");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查地图内的人物数量", "CHECKMAPHUMENCOUNT");

                    cd = new CompletionData("检查是否在攻城区域", "使用示例\r\n\r\n#IF\r\nCheckInWarArea\r\n#SAY\r\n在攻城区域");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查是否在攻城区域", "CHECKINWARAREA");

                    cd = new CompletionData("检测玩家指定技能的等级", "CheckSkill 技能名称 控制符(&gt;,&lt;,=) 参数1 参数2\r\n\r\n技能名称 要检测的技能名称\r\n控制符 操作符号.可选 &gt;、&lt;、=\r\n参数1 要检测的 技能/技能强化 等级\r\n参数2 0/1 0检测技能等级,1检测技能强化等级 参数2未设置时,默认为0\r\n\r\n使用示例\r\n\r\n[@CheckSkill1]\r\n#IF\r\nCheckSkill 雷电术 = 3 0  //判断人物雷电术等级是否为3级\r\n#ACT\r\nSendMsg 6 你的雷电术等级为3级\r\nBreak\r\n#ELSEACT\r\nSendMsg 6");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检测玩家指定技能的等级", "CHECKSKILL");

                    cd = new CompletionData("检测人物指定位置装备类型", "CHECKITEMTYPE 物品位置 物品类型\r\n\r\n使用示例\r\n\r\n[@test]\r\n\r\n#if\r\nCHECKITEMTYPE 1 5\r\n#act\r\nSendMsg 6 武器位置佩戴的是stdmode为5的武器！");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检测人物指定位置装备类型", "CHECKITEMTYPE");

                    cd = new CompletionData("如果", "如果，后面跟检查命令！");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("如果", "#IF");

                    cd = new CompletionData("或者", "符合以下任何一个条件则向下继续执行脚本！");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("或者", "#OR");

                    cd = new CompletionData("否则显示", "否则显示，后面跟显示的对话框信息！");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("否则显示", "#ELSESAY");

                    cd = new CompletionData("否则执行", "否则执行，后面跟执行命令！");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("否则执行", "#ELSEACT");

                    cd = new CompletionData("显示", "显示，后面跟显示的对话框字符！");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("显示", "#SAY");

                    cd = new CompletionData("执行", "执行，后面跟执行命令！");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("执行", "#ACT");

                    cd = new CompletionData("消息框", "发送消息提示");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("消息框", "SENDMSG");

                    cd = new CompletionData("检查地图内人数", "功能：\r\n   可以检查一个地图内的人物数量 \r\n\r\n格式：\r\nCHECKMAPHUMANCOUNT 地图号 < = > 数量 \r\n\r\n[@Main]\r\n#if\r\nCHECKMAPHUMANCOUNT 3 < 100\r\n#say\r\n地图3内人数小于100人\r\n#elsesay\r\n地图3内人数多于100人");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查地图内人数", "CHECKMAPHUMANCOUNT");

                    cd = new CompletionData("检查地图范围内怪物数", "功能：\r\n    可以检查一个坐标范围内怪物数量。 \r\n\r\n格式：\r\nCHECKRANGEMONCOUNT 地图号 X坐标 Y坐标 范围 控制符 < = > 数量 \r\n\r\n[@Main]\r\n#IF\r\n  CHECKRANGEMONCOUNT 3 330 330 10 < 100\r\n#SAY\r\n地图3的X330Y330十的范围内怪物少于100只\r\n#ELSESAY\r\n地图3的X330Y330十的范围内怪物多于100只");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查地图范围内怪物数", "CHECKRANGEMONCOUNT");

                    cd = new CompletionData("检查地图范围内指定名称怪物数", "功能：\r\n    可以检查一个坐标范围内指定的怪物数量。 \r\n\r\n格式：\r\nCHECKRANGEMONCOUNTEX 地图号 X坐标 Y坐标 怪物名称 控制符 < = > 数量\r\n\r\n[@Main]\r\n#IF\r\nCHECKRANGEMONCOUNTEX K004 51 43 猪 > 1 　//检查K004(51:43)名字为猪的怪物数量是否 >0,K004=SELF时,检测自己当前地图 \r\n#SAY\r\n地图K004的X51,Y43坐标上的猪大于1只。\r\n#ELSESAY\r\n地图K004的X51,Y43坐标上的猪小于1只。 \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查地图范围内指定名称怪物数", "CHECKRANGEMONCOUNTEX");

                    cd = new CompletionData("检查乾坤玉璧是否解封", "功能：\r\n   CHECKNIMBUSITEMCOUNT //检查乾坤玉璧是否解封 \r\n\r\n格式：\r\nCHECKNIMBUSITEMCOUNT 物品名是否解封(0=未解封,1= 已解封) < = > 数量//检测包裹指定玉璧数量(四个参数) \r\n\r\n;========================================== \r\n\r\n[@main] \r\n#IF\r\nCHECKNIMBUSITEMCOUNT 乾坤玉璧 1 > 9 \r\n#SAY\r\n你的10个乾坤玉璧都已经解封。\r\n#ELSESAY\r\n你的乾坤玉璧没有解封。\\\r\n;==========================================");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查乾坤玉璧是否解封", "CHECKNIMBUSITEMCOUNT");

                    cd = new CompletionData("检查人物HP值", "功能：\r\n    检查人物HP值上限及下限值 \r\n\r\n格式：\r\n\r\nCHECKHP 控制符 < = > ? HP下限 控制符 < = > ? HP上限 \r\n\r\n;========================================== \r\n[@checklevel0]\r\n#IF\r\nCHECKHP > 30 > 40\r\n#SAY\r\n你的HP大于30-40");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查人物HP值", "CHECKHP");

                    cd = new CompletionData("检查人物MP值", "功能：\r\n    检查人物MP值上限及下限值 \r\n\r\n格式：\r\n\r\nCHECKMP 控制符 < = > ? MP下限 控制符 < = > ? MP上限 \r\n\r\n;========================================== \r\n[@checklevel0]\r\n#IF\r\nCHECKMP > 30 > 40\r\n#SAY\r\n你的MP大于30-40\r\n;==========================================\r\n");
                    BlueCheckedData.Add("检查人物MP值", "CHECKMP");

                    CompletionDataList.Add(cd);
                    cd = new CompletionData("检查人物PK值", "功能：\r\n    检查人物PK值 \r\n\r\n格式：\r\n\r\nCHECKPKPOINT 2   //1点PK值 等于100点 \r\n\r\n;========================================== \r\n[@CHECKPKPOINT]\r\n#IF\r\nCHECKPKPOINT 2\r\n#SAY\r\n你的PK点数大于200点。 \r\n;==========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查人物PK值", "CHECKPKPOINT");

                    cd = new CompletionData("检查人物仓库是否解锁", "功能：\r\n    检查仓库是否解锁 \r\n\r\n格式：\r\n;========================================== \r\n;检查仓库是否解锁\r\n[@CHECKISLOCK]\r\n#IF\r\nISLOCKPASSWORD\r\n#SAY\r\n您的仓库锁定中。\r\n#ELSESAY\r\n您的仓库已经解锁。\r\n\r\n;========================================= =\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查人物仓库是否解锁", "ISLOCKPASSWORD");

                    cd = new CompletionData("检查人物任务进度", "功能：\r\n   检查任务进度\r\n\r\n格式：\r\nCHECKMISSION ID < = > 步骤 \r\nCHECKMISSION ID = 0 //任务不存在\r\nCHECKMISSION ID > 2 //任务执行到步骤2以上,否则任务不存在或未执行到步骤2以上\r\n\r\n;========================================== \r\n\r\n[@main] \r\n#IF\r\nCHECKMISSION 1 > 1 \r\n#SAY\r\n你目前的任务做到了第一步的第一小节。\r\n#ELSESAY\r\n你都还没开始任务。\\\r\n;==========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查人物任务进度", "CHECKMISSION");

                    cd = new CompletionData("检查人物保存型变量", "功能：CHECKINTS 0~9 >/</= 数量\r\n\r\n变量显示：<$INTS0>~<$INTS9> \r\n\r\n#IF\r\nCHECKINTS 0~9 < = > ? 数量\r\n#ACT\r\nINTS 0 + 1\r\nINTS 0 = 1\r\nINTS 0 - 1 \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查人物保存型变量", "CHECKINTS");

                    cd = new CompletionData("检查人物元宝数量", "功能：\r\n   检查当前人物身上有多少元宝 \r\n\r\n格式：\r\nCHECKGAMEGOLD 控制符 < = > ? 元宝数量(1 - 2100000000) \r\n\r\n;==========================================\r\n;检查元宝是否等于指定数量\r\n[@CHECKGAMEGOLD0]\r\n#IF\r\nCHECKGAMEGOLD = 50 \r\n#SAY\r\n您元宝等于50颗。\r\n#ELSESAY\r\n您没有等于50颗元宝。\r\n;==========================================\r\n\r\n;==========================================\r\n;检查元宝是否大于指定数量\r\n[@CHECKGAMEGOLD1]\r\n#IF\r\nCHECKGAMEGOLD > 50\r\n#SAY\r\n您元宝大于50颗。\r\n#ELSESAY\r\n您没有大于50颗元宝。\r\n;==========================================\r\n\r\n;==========================================\r\n;检查元宝是否小于指定数量\r\n[@CHECKGAMEGOLD2]\r\n#IF\r\nCHECKGAMEGOLD < 50\r\n#SAY\r\n您元宝小于50颗。\r\n#ELSESAY\r\n您没有小于50颗元宝。\r\n;==========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查人物元宝数量", "CHECKGAMEGOLD");

                    cd = new CompletionData("检查人物内功等级", "功能：\r\nCHECKIPLEVEL 操作符 < = > ? 数值 //检查内功等级\r\n\r\n格式：\r\n\r\n[@CHECKIPLEVEL]\r\n#IF\r\nCHECKIPLEVEL > 10\r\n#SAY\r\n目前内功等级达到了10级。 \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查人物内功等级", "CHECKIPLEVEL");

                    cd = new CompletionData("检查人物包裹是否有某样东西", "功能：\r\n    检测包裹是否有某样东西 \r\n\r\n格式：\r\nCHECKITEM 物品 数量\r\n\r\n;==========================================\r\n;检测包裹是否有某样东西\r\n[@MAIN]\r\n#IF \r\nCHECKITEM 裁决之杖 2 \r\n#SAY\r\n你的包裹里有裁决之杖2把。\r\n#ELSESAY\r\n你的包裹里没有2把裁决之杖。\r\n;==========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查人物包裹是否有某样东西", "CHECKITEM");

                    cd = new CompletionData("检查人物名称是否与IP匹配", "功能：\r\n    可以检查人物名称是否与IP匹配。 \r\n\r\n格式：\r\nCHECKNAMEIPLIST NameIPList.txt\r\n\r\n;==========================================\r\n;检查人物的名称是否与IP地址匹配\r\n[@checklevel0]\r\n#IF\r\nCHECKNAMEIPLIST NameIPList.txt\r\n#SAY\r\n亲爱的管理员,欢迎您进入游戏管理地图。\r\n#ELSESAY\r\n您的人物不是管理员不能进入游戏管理地图。\r\n;==========================================\r\n\r\n列表文件格式:\r\n此文件们于目录：Mir200\\Envir\\\r\n\r\nNameIPList.txt\r\n\r\n;==========================================\r\n;登录人物名称 　 　IP\r\nMIRS　 　 　 　 192.168.1.123\r\nbaidu.com 　 　 192.168.1.124\r\n;==========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查人物名称是否与IP匹配", "CHECKNAMEIPLIST");

                    cd = new CompletionData("检查人物在列表中的位置", "功能：\r\n        检查人物在列表中的位置。 \r\n\r\n格式：\r\n        CHECKNAMELISTPOSITION List.txt 10\r\n\r\n;==========================================\r\n;\r\n[@main]\r\n#IF\r\nCHECKNAMELISTPOSITION AccountIPList.txt 10\r\n#SAY\r\n您在前10名。\r\n#ELSESAY\r\n您不在前10名。\r\n;==========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查人物在列表中的位置", "CHECKNAMELISTPOSITION");

                    cd = new CompletionData("检查人物在线时长", "功能：\r\n    检查人物在线时长。 \r\n\r\n格式：\r\n    ONLINELONGMIN < = > ?\r\n;==========================================\r\n#if\r\n ONLINELONGMIN > 10\r\n#say\r\n在线时间大于 10分钟\r\n;==========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查人物在线时长", "ONLINELONGMIN");

                    cd = new CompletionData("检查人物声望点", "功能：\r\n   检查人物声望点 \r\n\r\n格式：\r\nCHECKCREDITPOINT 控制符 < = > ? 声望点数(1 - 255) \r\n\r\n;==========================================\r\n;检查声望点是否等于指定点数\r\n[@CHECKCREDITPOINT0]\r\n#IF\r\nCHECKCREDITPOINT = 50 \r\n#SAY\r\n您声望点等于50级。\r\n#ELSESAY\r\n您声望点不等于50级。\r\n;==========================================\r\n\r\n;==========================================\r\n;检查声望点是否大于指定点数\r\n[@CHECKCREDITPOINT1]\r\n#IF\r\nCHECKCREDITPOINT > 50\r\n#SAY\r\n您声望点大于50级。\r\n#ELSESAY\r\n您声望点不大于50级。\r\n;==========================================\r\n\r\n;==========================================\r\n;检查声望点是否小于指定点数\r\n[@CHECKCREDITPOINT2]\r\n#IF\r\nCHECKCREDITPOINT < 50\r\n#SAY\r\n您声望点小于50级。\r\n#ELSESAY\r\n您声望点不小于50级。\r\n;==========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查人物声望点", "CHECKCREDITPOINT");

                    cd = new CompletionData("检查人物宝宝名称", "功能：\r\n    可以检查玩家所带宠物的名称。 \r\n\r\n格式：\r\nCHECKSLAVENAME 属下名字\r\n\r\n;==========================================\r\n;检查宝宝数量是否为指定数量\r\n[@CHECKSLAVENAME]\r\n#IF \r\nCHECKSLAVENAME 神兽\r\n#SAY\r\n你的宝宝是神兽。\r\n#ELSESAY\r\n你的宝宝不是神兽。\r\n;==========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查人物宝宝名称", "CHECKSLAVENAME");

                    cd = new CompletionData("检查人物宝宝数量", "功能：\r\n    可以检查玩家所带宠物的数量。 \r\n\r\n格式：\r\nCHECKSLAVECOUNT  控制符  < = >?  数量\r\n\r\n;==========================================\r\n;检查宝宝数量是否为指定数量\r\n[@CHECKSLAVECOUNT0]\r\n#IF\r\nCHECKSLAVECOUNT = 5\r\n#SAY\r\n你的宝宝数量为5个。\r\n#ELSESAY\r\n你的宝宝数量不为5个。\r\n;========================================= =\r\n\r\n;==========================================\r\n;检查宝宝数量是否为大于数量\r\n[@CHECKSLAVECOUNT1]\r\n#IF\r\nCHECKSLAVECOUNT > 5\r\n#SAY\r\n你的宝宝数量超过5个。\r\n#ELSESAY\r\n你的宝宝数量不超过5个。\r\n;==========================================\r\n\r\n;==========================================\r\n;检查宝宝数量是否为小于数量\r\n[@CHECKSLAVECOUNT2]\r\n#IF\r\nCHECKSLAVECOUNT < 5\r\n#SAY\r\n你的宝宝数量小于5个。\r\n#ELSESAY\r\n你的宝宝数量不小于5个。\r\n;==========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查人物宝宝数量", "CHECKSLAVECOUNT");

                    cd = new CompletionData("检查人物宝宝最高等级", "功能：\r\n   检查宝宝等级 \r\n\r\n格式：\r\n脚本功能:\r\n检查宝宝的等级。 \r\n\r\n命令格式:\r\nCHECKSLAVELEVEL 控制符 < = > ? 等级数(7)\r\n\r\n;==========================================\r\n;检查宝宝的等级是否等于指定级别\r\n[@checklevel0]\r\n#IF\r\nCHECKSLAVELEVEL = 5 \r\n#SAY\r\n宝宝的等级等于5级。\r\n#ELSESAY\r\n宝宝的等级不等于5级。\r\n;==========================================\r\n\r\n;==========================================\r\n;检查宝宝的等级是否大于指定级别\r\n[@checklevel1]\r\n#IF\r\nCHECKSLAVELEVEL > 5\r\n#SAY\r\n宝宝的等级大于5级。\r\n#ELSESAY\r\n宝宝的等级不大于5级。\r\n;==========================================\r\n\r\n;==========================================\r\n;检查宝宝的等级是否小于指定级别\r\n[@checkposelevel2]\r\n#IF\r\nCHECKSLAVELEVEL < 5\r\n#SAY\r\n宝宝的等级小于5级。\r\n#ELSESAY\r\n宝宝的等级不小于5级。\r\n;==========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查人物宝宝最高等级", "CHECKSLAVELEVEL");

                    cd = new CompletionData("检查人物帐号与IP是否匹配", "功能：\r\n    可以检查登录帐号与IP是否匹配。 \r\n\r\n格式：\r\nCHECKACCOUNTIPLIST AccountIPList.txt\r\n\r\n;==========================================\r\n;检查人物的帐号是否与指定IP匹配\r\n[@checklevel0]\r\n#IF\r\n  CHECKACCOUNTIPLIST AccountIPList.txt\r\n#SAY\r\n您的登录帐号与指定IP匹配。\r\n#ELSESAY\r\n您的登录帐号与指定IP不匹配。\r\n;==========================================\r\n\r\n列表文件格式:\r\n此文件们于目录：Mir200\\Envir\\\r\n\r\nAccountIPList.txt\r\n\r\n;==========================================\r\n;登录帐号 IP\r\n12345         192.168.1.123\r\n54321         192.168.1.124\r\n;==========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查人物帐号与IP是否匹配", "CHECKACCOUNTIPLIST");

                    cd = new CompletionData("检查人物当前地图", "能：\r\n   检查人物当前地图 \r\n\r\n格式：\r\nISONMAP 地图号\r\n\r\n;==========================================\r\n[@Main]\r\n#IF\r\nISONMAP 3\r\n#SAY\r\n你现在所在地图是盟重！\r\n#ELSESAY\r\n你现在所在地图不是盟重！\r\n;==========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查人物当前地图", "ISONMAP");

                    cd = new CompletionData("检查人物当前经验值", "功能：\r\n检查玩家当前经验值。 \r\n\r\n格式：\r\n\r\nCHECKEXP 控制符 < = > ? 经验值(1 - 4000000000) \r\n\r\n;==========================================\r\n;检查人物的经验值是否等于指定值\r\n[@checkexp2]\r\n#IF\r\nCHECKEXP = 5000 \r\n#SAY\r\n您的经验值等于5000。\r\n#ELSESAY\r\n您的经验值不等于5000。\r\n;==========================================\r\n\r\n;==========================================\r\n;检查人物的经验值是否大于指定值\r\n[@checkexp2]\r\n#IF\r\nCHECKEXP > 5000\r\n#SAY\r\n您的经验值大于5000级。\r\n#ELSESAY\r\n您的经验值不大于5000。\r\n;==========================================\r\n\r\n;==========================================\r\n;检查人物的经验值是否小于指定值\r\n[@checkexp2]\r\n#IF\r\nCHECKEXP < 5000\r\n#SAY\r\n您的经验值小于5000级。\r\n#ELSESAY\r\n您的经验值不小于5000。\r\n;==========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查人物当前经验值", "CHECKEXP");

                    cd = new CompletionData("检查人物技能", "功能：\r\n\r\n1.检查人物技能\r\n\r\nCHECKMAGICNAME 技能名 \r\n\r\n2.检查人物技能级别\r\n\r\nCHECKMAGICLEVEL 技能名 < = > 等级 \r\n================================\r\n\r\n例1：\r\n#IF\r\nCHECKMAGICNAME 烈火剑法\r\n#SAY\r\n你学习了烈火剑法。 \r\n\r\n================================\r\n例2： \r\n#IF \r\nCHECKMAGICLEVEL 雷电术 > 2\r\n#SAY\r\n你的雷电术大于2级！ \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查人物技能", "CHECKMAGICNAME");

                    cd = new CompletionData("检查人物攻击力", "功能：\r\n    检查人物攻击力上限及下限值 \r\n\r\n格式：\r\n\r\nCHECKDC 控制符 < = > ? 攻击下限 控制符 < = > ? 攻击上限 \r\n\r\n;========================================== \r\n[@checklevel0]\r\n#IF\r\nCHECKDC > 30 > 40\r\n#SAY\r\n你的攻击力大于30-40 \r\n;==========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查人物攻击力", "CHECKDC");

                    cd = new CompletionData("检查人物攻击模式", "功能：\r\n    检查人物攻击模式。 \r\n\r\n格式：\r\nCHECKATTACKMODE < = > 0~6 //检测攻击模式 0 = 全体 1 = 和平 2=夫妻 3=师徒 4=编组 5= 行会 6= 善恶\r\n\r\nCHECKATTACKMODE < = > 0~6 //检测攻击模式\r\n\r\n0 全体模式\r\n1 和平模式\r\n2 夫妻模式\r\n3 师徒模式\r\n4 编组模式\r\n5 行会模式\r\n6 善恶模式 \r\n\r\n#IF\r\nCHECKATTACKMODE > 0\r\n#SAY\r\n你现在的攻击模式是全体模式。\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查人物攻击模式", "CHECKATTACKMODE");

                    cd = new CompletionData("检查人物是否为守城方", "功能：\r\n\r\nISDEFENSEGUILD //检查人物是否为守城方\r\n\r\n格式：\r\n\r\n;==========================================\r\n\r\n[@main]\r\n#IF\r\nISDEFENSEGUILD\r\n#SAY\r\n目前你所在的行会是守城方！\r\n#ELSESAY\r\n你所在的行会不是今天的守城方！\\ \\\r\n<返回/@MAIN>\r\n;========================================== \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查人物是否为守城方", "ISDEFENSEGUILD");

                    cd = new CompletionData("检查人物是否为守城方联盟行会", "功能：\r\n\r\nISDEFENSEALLYGUILD //检查人物是否为守城方联盟行会\r\n\r\n格式：\r\n\r\n;==========================================\r\n\r\n[@main]\r\n#IF\r\nISDEFENSEALLYGUILD\r\n#SAY\r\n目前你所在的行会是守城方联盟行会！\r\n#ELSESAY\r\n你所在的行会不是今天的守城方联盟行会！\\ \\\r\n<返回/@MAIN>\r\n;========================================== \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查人物是否为守城方联盟行会", "ISDEFENSEALLYGUILD");

                    cd = new CompletionData("检查人物是否为攻城方", "功能：\r\n\r\nISATTACKGUILD //检查人物是否为攻城方\r\n\r\n格式：\r\n\r\n;==========================================\r\n\r\n[@main]\r\n#IF\r\nISATTACKGUILD\r\n#SAY\r\n目前你所在的行会是攻城方！\r\n#ELSESAY\r\n你所在的行会不是今天的攻城方！\\ \\\r\n<返回/@MAIN>\r\n;========================================== \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查人物是否为攻城方", "ISATTACKGUILD");

                    cd = new CompletionData("检查人物是否为攻城方联盟行会", "功能：\r\n\r\nISATTACKALLYGUILD //检查人物是否为攻城方联盟行会\r\n\r\n格式：\r\n\r\n;==========================================\r\n\r\n[@main]\r\n#IF\r\nISATTACKALLYGUILD\r\n#SAY\r\n目前你所在的行会是攻城方联盟行会！\r\n#ELSESAY\r\n你所在的行会不是今天的攻城方联盟行会！\\ \\\r\n<返回/@MAIN>\r\n;========================================== \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查人物是否为攻城方联盟行会", "ISATTACKALLYGUILD");

                    cd = new CompletionData("检查人物是否为新人", "功能：\r\n    可以检查玩家是否为新人(即刚注册进入游戏的人,退出后再进就不是新人了)。 \r\n\r\n格式：\r\n============================\r\n    #IF\r\n    ISNEWHUMAN\r\n    #SAY\r\n    你是新人！\r\n    #ELSESAY\r\n    你不是新人！\r\n===========================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查人物是否为新人", "ISNEWHUMAN");

                    cd = new CompletionData("检查人物是否为沙城城主", "功能：\r\n    可以检查玩家是否为沙城老大。 \r\n\r\n格式：\r\n============================\r\n    #IF\r\n    ISCASTLEMASTER\r\n    #SAY\r\n    你是沙城老大！\r\n    #ELSESAY\r\n    你不是沙城老大！\r\n============================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查人物是否为沙城城主", "ISCASTLEMASTER");

                    cd = new CompletionData("检查人物是否为沙城成员", "功能：\r\n    可以检查玩家是否为沙城成员。 \r\n\r\n格式：\r\n============================\r\n    #IF\r\n    ISCASTLEGUILD\r\n    #SAY\r\n    你是沙城成员！\r\n    #ELSESAY\r\n    你不是沙城成员！\r\n============================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查人物是否为沙城成员", "ISCASTLEGUILD");

                    cd = new CompletionData("检查人物是否为管理员", "功能：\r\n    检查人物是否为管理员\r\n\r\n格式：\r\n;==========================================\r\n;检查人物是否为系统管理员\r\n[@IsSysOp]\r\n#IF\r\nISADMIN\r\n#SAY\r\n你是系统管理员。\r\n#ELSESAY\r\n你不是系统管理员。\r\n;==========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查人物是否为管理员", "ISADMIN");

                    cd = new CompletionData("检查人物是否为组长", "功能：\r\nISGROUPMASTER //检查人物是否为编组的组长。 \r\n\r\n格式：\r\n============================\r\n#IF\r\nISGROUPMASTER\r\n#SAY\r\n你是组长\r\n#ELSESAY\r\n你不是组长 \r\n===========================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查人物是否为组长", "ISGROUPMASTER");

                    cd = new CompletionData("检查人物是否为行会掌门人", "功能：\r\n    可以检查玩家是否为行会老大。\r\n\r\n格式：\r\n============================\r\n    #IF\r\n    ISGUILDMASTER\r\n    #SAY\r\n    你是行会老大！\r\n    #ELSESAY\r\n    你不是行会老大！\r\n============================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查人物是否为行会掌门人", "ISGUILDMASTER");

                    cd = new CompletionData("检查人物是否付费", "功能：\r\n    检查人物是否付费 \r\n\r\n格式：\r\n\r\nCHECKPAYMENT 数字 (代表天数) \r\n\r\n;==========================================\r\n#if\r\nCHECKPAYMENT 2\r\n#say\r\n您目前剩余游戏时间2天。 \r\n;=========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查人物是否付费", "CHECKPAYMENT");

                    cd = new CompletionData("检查人物是否到名单中", "功能：\r\n检查人物是否到列表中 \r\n\r\n格式：\r\n=========================\r\n[@CHECKNAMELIST]\r\n#IF\r\nCHECKNAMELIST 行会争霸名单.TXT\r\n#SAY\r\n你的名称已经在行会争霸名单里了。\r\n#ELSESAY\r\n你还没有申请行会争霸。\\\r\n=========================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查人物是否到名单中", "CHECKNAMELIST");

                    cd = new CompletionData("检查人物是否加入行会", "功能：\r\n    可以检查玩家是否加入行会。\r\n\r\n格式：\r\n============================\r\n    #IF\r\n    HAVEGUILD\r\n    #SAY\r\n    你已经加入了行会！\r\n    #ELSESAY\r\n    你没加入行会！\r\n============================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查人物是否加入行会", "HAVEGUILD");

                    cd = new CompletionData("检查人物是否在线", "功能：\r\nCHECKONLINE //检查人物是否在线 \r\n\r\nH.CHECKONLINE //检查英雄是否在线 \r\n\r\n格式：\r\n================================\r\n\r\n将会根据你收取或者送出的祝福显示你在玛法大陆的人气,\\\r\n朋友越多,快乐越多,千金易得,挚友难求啊！\\\r\n<请输入祝福对象名称:/@@InPutString7>后,可显示内容！\\ \\\r\n<返回/@main>\\\r\n\r\n[@@InPutString7]\r\n#IF\r\nCheckLevelEx > 0\r\n#ACT\r\nDelayGoto 1 ~InPutString7\r\n\r\n[~InPutString7]\r\n#IF\r\nEQUAL S7 <$USERNAME>\r\n#ACT\r\nMessageBox 不能对自己发送祝福。\r\nBREAK\r\n#IF\r\nCHECKONLINE <$STR(S7)>\r\n#ACT\r\nDEC S7 16 999\r\n#SAY\r\n<浪漫情话/@浪漫情话> <亲密友爱/@亲密友爱> <常用语句/@常用语句>\\ \\\r\n<疯狂搞笑/@疯狂搞笑> <互相调侃/@互相调侃> <星宿神功/@星宿神功>\\ \\\r\n<返回/@main>\\\r\n#ELSEACT\r\nDEC S7 16 999\r\nMessageBox <$STR(S7)>\\不在线,你不能发送祝福。\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查人物是否在线", "CHECKONLINE");

                    cd = new CompletionData("检查人物是否是师傅", "功能：\r\n\r\nCHECKISMASTER //检查人物是否是师傅\r\n\r\n格式：\r\n\r\n;==========================================\r\n\r\n[@main]\r\n#IF\r\nCHECKISMASTER\r\n#SAY\r\n你是师傅！\r\n#ELSESAY\r\n你不是师傅。\\\r\n;========================================== \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查人物是否是师傅", "CHECKISMASTER");

                    cd = new CompletionData("检查人物是否是徒弟", "功能：\r\n\r\nCHECKMASTER //检查人物是否是徒弟\r\n\r\n格式：\r\n\r\n;==========================================\r\n\r\n[@main] \r\n#IF\r\nCHECKMASTER\r\n#SAY\r\n你是徒弟！\r\n#ELSESAY\r\n你不是徒弟！\\\r\n\r\n;========================================== \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查人物是否是徒弟", "CHECKMASTER");

                    cd = new CompletionData("检查人物是否有死亡地点", "功能：\r\n   检查人物是否有死亡地点.可以让人飞到上一次死亡地点。 \r\n\r\n格式：\r\nCHECKSIGNMAP\r\n;==========================================\r\n[@CHECKSIGNMAP]\r\n#IF\r\nCHECKSIGNMAP\r\n#SAY\r\n是否回到上次死亡的地点？\\\r\n<是/@SignMap> <否/@exit>\r\n\r\n[@SignMap]\r\n#ACT\r\nGMEXECUTE SignMove SELF \r\n;==========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查人物是否有死亡地点", "CHECKSIGNMAP");

                    cd = new CompletionData("检查人物是否结婚", "功能：\r\n\r\nCHECKMARRY //检查人物是否结婚\r\n\r\n格式：\r\n\r\n[@main]\r\n#IF\r\nCHECKMARRY \r\n#SAY\r\n你已经结婚了！\\ \r\n#ELSESAY\r\n你还没有结婚。\\\r\n<返回/@MAIN>\r\n;==========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查人物是否结婚", "CHECKMARRY");

                    cd = new CompletionData("检查人物泡点数量", "功能：\r\n   检查当前人物身上有多少泡点 \r\n\r\n格式：\r\nCHECKGAMEPOINT 控制符 < = > ? 泡点数量(1 - 2100000000) \r\n\r\n;==========================================\r\n;检查泡点是否等于指定数量\r\n[@CHECKGAMEPOINT]\r\n#IF\r\nCHECKGAMEPOINT = 50 \r\n#SAY\r\n您的泡点数量等于50点。\r\n#ELSESAY\r\n您的泡点不足50点。 \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查人物泡点数量", "CHECKGAMEPOINT");

                    cd = new CompletionData("检查人物灵气值", "功能：\r\n    检查人物灵气值。 \r\n\r\n格式：\r\nCHECKNIMBUS  < = > ? //检查人物灵气数量 \r\n\r\n#IF\r\nCHECKNIMBUS > 200\r\n#SAY\r\n你身上的灵气大于200。\r\n\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查人物灵气值", "CHECKNIMBUS");

                    cd = new CompletionData("检查人物灵符数量", "功能：\r\nCHECKGAMEGIRD //检查人物灵符数量 \r\n\r\n格式：\r\n================================\r\n检查人物灵符\r\n\r\n#IF\r\nCHECKGAMEGIRD > 0\r\n#ACT\r\nGAMEGIRD - 1\r\nmapmove D5071C 10 14\r\n#ELSESAY\r\n你给我的灵符在哪呢?要不你先去兑换一些?\n\r\n================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查人物灵符数量", "CHECKGAMEGIRD");

                    cd = new CompletionData("检查人物登陆IP", "功能：\r\n\r\nCHECKIPLIST IP.txt //检查人物登陆IP \r\n\r\n格式：\r\n\r\n;==========================================\r\n;按登录IP检查\r\n[@main]\r\n#IF\r\nCHECKIPLIST IP.txt\r\n#SAY\r\n你所在的IP地址属于会员网吧。\r\n#ELSESAY\r\n你的IP地址不属于会员网把,不能获得奖励！\r\n;==========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查人物登陆IP", "CHECKIPLIST");

                    cd = new CompletionData("检查人物登陆帐号", "功能：\r\n\r\nCHECKACCOUNTLIST 帐号.txt //检查人物登陆帐号 \r\n\r\n格式：\r\n\r\n;==========================================\r\n;按登录帐号检查\r\n[@main]\r\n#IF\r\nCHECKACCOUNTLIST 帐号.txt\r\n#SAY\r\n你是会员\r\n#ELSESAY\r\n你不是会员\r\n;=========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查人物登陆帐号", "CHECKACCOUNTLIST");

                    cd = new CompletionData("检查人物的会员时间", "功能：\r\n    检查人物的会员时间。 \r\n\r\n格式：\r\nCHECKUSERDATE 会员.txt　<　30　p0　p1\r\n　检查命令 　会员名单　控制符　天数　使用天数　剩余天数(可用<$STR(p1)>在脚本中显示) \r\n\r\n注：如果要检查忽略人物名字就在p1 后面加个参数 1\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查人物的会员时间", "CHECKUSERDATE");

                    cd = new CompletionData("检查人物的会员等级", "功能：\r\n    可以检查人物的会员等级。 \r\n\r\n格式：\r\nCHECKMEMBERLEVEL 类型(1-65535) 控制符 < = > ?  等级数(1 - 65535)\r\n\r\n;==========================================\r\n;检查人物的会员等级是否为指定等级\r\n[@checkmemberlevel0]\r\n#IF\r\n  CHECKMEMBERLEVEL 2 = 5\r\n#SAY\r\n您的会员等级为5。\r\n#ELSESAY\r\n您的会员等级不为5。\r\n;==========================================\r\n\r\n;==========================================\r\n;检查人物的会员等级是否大于指定等级\r\n[@checkmemberlevel1]\r\n#IF\r\n  CHECKMEMBERLEVEL 2 > 5\r\n#SAY\r\n您的会员等级大于5。\r\n#ELSESAY\r\n您的会员等级不大于5。\r\n;==========================================\r\n\r\n;==========================================\r\n;检查人物的会员等级是否小于指定等级\r\n[@checkmemberlevel1]\r\n#IF\r\n  CHECKMEMBERLEVEL 2 < 5\r\n#SAY\r\n您的会员等级小于5。\r\n#ELSESAY\r\n您的会员等级不小于5。\r\n;========================================== \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查人物的会员等级", "CHECKMEMBERLEVEL");

                    cd = new CompletionData("检查人物的会员类型", "功能：\r\n    可以检查人物的会员类型。 \r\n\r\n格式：\r\nCHECKMEMBERTYPE 控制符 < = > ?  类型数(1 - 65535)\r\n\r\n;==========================================\r\n;检查人物的会员类型是否为指定类型\r\n[@checkmember0]\r\n#IF\r\n  CHECKMEMBERTYPE = 5\r\n#SAY\r\n您的会员类型为5。\r\n#ELSESAY\r\n您的会员类型不为5。\r\n;==========================================\r\n\r\n;==========================================\r\n;检查人物的会员类型是否大于指定类型\r\n[@checkmember1]\r\n#IF\r\n  CHECKMEMBERTYPE > 5\r\n#SAY\r\n您的会员类型大于5。\r\n#ELSESAY\r\n您的会员类型不大于5。\r\n;==========================================\r\n\r\n;==========================================\r\n;检查人物的会员类型是否小于指定类型\r\n[@checkmember1]\r\n#IF\r\n  CHECKMEMBERTYPE < 5\r\n#SAY\r\n您的会员类型小于5。\r\n#ELSESAY\r\n您的会员类型不小于5。\r\n;========================================== \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查人物的会员类型", "CHECKMEMBERTYPE");

                    cd = new CompletionData("检查人物的性别", "功能：\r\n    可以检查人物的性别。 \r\n\r\n格式：\r\nGENDER 性别(MAN,男,WOMAN,女)\r\n\r\n;==========================================\r\n;检查人物是否为男的\r\n[@男]\r\n#IF\r\nGENDER MAN\r\n#SAY\r\n你的人物是男的。\r\n#ELSESAY\r\n你的人物不是男的。\r\n;==========================================\r\n\r\n;检查人物是否为女的\r\n[@女]\r\n#IF\r\nGENDER WOMAN\r\n#SAY\r\n你的人物是女的。\r\n#ELSESAY\r\n你的人物不是女的。\r\n;========================================== \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查人物的性别", "GENDER");

                    cd = new CompletionData("检查人物的经脉等级", "功能：\r\n    CHECKVENATIONLEVEL V 控制符 < = > ? P //检测经脉等级\r\n\r\nV //范围：0~3 表示四条经脉之一\r\nP //范围：0~5 要检测的重数\r\n\r\n格式：\r\n\r\n;==========================================\r\n[@CHECKVENATIONLEVEL]\r\n#IF\r\nCHECKVENATIONLEVEL 0 > 1 \r\n#SAY\r\n你的冲脉已经打通了通骨。\r\n;========================================== \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查人物的经脉等级", "CHECKVENATIONLEVEL");

                    cd = new CompletionData("检查人物的转生等级", "功能：\r\n    检查人物的转生等级 \r\n\r\n命令格式:\r\n    CHECKRENEWLEVEL 控制符 < = > ?  转生等级数(1 - 255) \r\n\r\n;==========================================\r\n;检查人物的转生等级是否等于指定级别\r\n[@checklevel0]\r\n#IF\r\nCHECKRENEWLEVEL = 5\r\n#SAY\r\n您的转生等级等于5级。\r\n#ELSESAY\r\n您的转生等级不等于5级。\r\n;==========================================\r\n\r\n;==========================================\r\n;检查人物的转生等级是否大于指定级别\r\n[@checklevel1]\r\n#IF\r\nCHECKRENEWLEVEL > 5\r\n#SAY\r\n您的转生等级大于5级。\r\n#ELSESAY\r\n您的转生等级不大于5级。\r\n;==========================================\r\n\r\n;==========================================\r\n;检查人物的转生等级是否小于指定级别\r\n[@checkposelevel2]\r\n#IF\r\nCHECKRENEWLEVEL < 5\r\n#SAY\r\n您的转生等级小于5级。\r\n#ELSESAY\r\n您的转生等级不小于5级。\r\n;==========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查人物的转生等级", "CHECKRENEWLEVEL");

                    cd = new CompletionData("检查人物等级(改进)", "功能：\r\n   检查玩家等级。 \r\n\r\n格式：\r\nCHECKLEVELEX 控制符 < = > ?  等级数(1 - 65535) \r\n\r\n;==========================================\r\n;检查人物的等级是否等于指定级别\r\n[@checklevel0]\r\n#IF\r\n  CHECKLEVELEX = 50\r\n#SAY\r\n您的等级等于50级。\r\n#ELSESAY\r\n您的等级不等于50级。\r\n;==========================================\r\n\r\n;==========================================\r\n;检查人物的等级是否大于指定级别\r\n[@checklevel1]\r\n#IF\r\n  CHECKLEVELEX > 50\r\n#SAY\r\n您的等级大于50级。\r\n#ELSESAY\r\n您的等级不大于50级。\r\n;==========================================\r\n\r\n;==========================================\r\n;检查人物的等级是否小于指定级别\r\n[@checkposelevel2]\r\n#IF\r\n  CHECKLEVELEX < 50\r\n#SAY\r\n您的等级小于50级。\r\n#ELSESAY\r\n您的等级不小于50级。\r\n;==========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查人物等级(改进)", "CHECKLEVELEX");

                    cd = new CompletionData("检查人物职业", "功能：\r\nCHECKJOB(WARRIOR ,WIZARD,TAOIST) //检查人物职业 \r\n\r\n格式：\r\n================================\r\n检查人物职业\r\n\r\n#IF\r\nCHECKJOB WARRIOR\r\n#SAY\r\n是不是战士不好玩呀,换换别的职业试试吧！\\ \\\r\n『<玩玩法师/@法师>』\\\r\n『<玩玩道士/@道士>』\\ \\\r\n〖<返回/@管理>〗〖<关闭/@EXIT>〗\r\n#ACT\r\nBREAK\r\n#IF\r\nCHECKJOB WIZARD\r\n#SAY\r\n是不是法师不好玩呀,换换别的职业试试吧！\\ \\\r\n『<玩玩战士/@战士>』\\\r\n『<玩玩道士/@道士>』\\ \\\r\n〖<返回/@管理>〗〖<关闭/@EXIT>〗\r\n#ACT\r\nBREAK\r\n#IF\r\nCHECKJOB TAOIST\r\n#SAY\r\n是不是道士不好玩呀,换换别的职业试试吧！\\ \\\r\n『<玩玩战士/@战士>』\\\r\n『<玩玩法师/@法师>』\\ \\\r\n〖<返回/@管理>〗〖<关闭/@EXIT>〗\r\n================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查人物职业", "CHECKJOB");

                    cd = new CompletionData("检查人物包裹空格数", "功能：\r\n    检查人物包裹空格数 \r\n\r\n格式：\r\n\r\nCHECKBAGSIZE 数字 (代表剩余包袱空格) \r\n\r\n;==========================================\r\n#IF\r\nCHECKBAGSIZE 5 \r\n#SAY\r\n您目前剩余包袱空格5个。 \r\n;=========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查人物包裹空格数", "CHECKBAGSIZE");

                    cd = new CompletionData("检查人物行会名称", "功能：\r\n   检查行会名称\r\n\r\n格式：\r\n================================\r\n#if\r\nCHECKOFGUILD 行会名称\r\n#say\r\n行会正确\r\n================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查人物行会名称", "CHECKOFGUILD");

                    cd = new CompletionData("检查人物行会是否到列表中", "功能：\r\n检查行会是否到列表中\r\n\r\n格式：\r\n================================\r\n[@CHECKGUILDLIST]\r\n#IF\r\nCHECKGUILDLIST 行会争霸.TXT\r\n#SAY\r\n列表中有行会。\r\n#ELSESAY\r\n列表中没有行会\r\n================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查人物行会是否到列表中", "CHECKGUILDLIST");

                    cd = new CompletionData("检查人物身上指定位置是否戴装备", "功能：\r\n    可以检查人物身上指定位置是否戴装备。 \r\n\r\n格式：\r\nCHECKUSEITEM 物品位置(0-12)\r\n\r\n;==========================================\r\n物品位置:\r\n0 盔甲\r\n1 武器\r\n2 照明物(蜡烛,此物品属性升级无效)\r\n3 项链\r\n4 头盔\r\n5 右手镯\r\n6 左手镯\r\n7 右戒指\r\n8 右戒指\r\n9 无(放护身符位置)\r\n10 腰带\r\n11 鞋子\r\n12 宝石\r\n\r\n[@checkUseItem]\r\n#if\r\nCHECKUSEITEM 0\r\n#say\r\n你身上穿了衣服。\r\n#elsesay\r\n你还没穿衣服呢。 \r\n;==========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查人物身上指定位置是否戴装备", "CHECKUSEITEM");

                    cd = new CompletionData("检查人物身上指定位置装备类型", "功能：\r\n    可以检查人物身上指定位置装备类型。 \r\n\r\n格式：\r\nCHECKUSEITEM 物品位置(0-12),用来检查人物身上戴物品类型,因为不同类型物品的附加属性值位置不一样,在升级装备物品性时用来控制指定属性值\r\n\r\n;========================================== \r\n\r\n物品位置：\r\n0 盔甲\r\n1 武器\r\n2 照明物\r\n3 项链\r\n4 头盔\r\n5 右手镯\r\n6 左手镯\r\n7 右戒指\r\n8 右戒指\r\n9 无(放护身符位置)\r\n10 腰带\r\n11 鞋子\r\n12 宝石\r\n\r\n物品类型：\r\n5 武器\r\n6 武器\r\n10 衣服\r\n11 衣服\r\n15 头盔\r\n19 项链\r\n20 项链\r\n21 项链\r\n22 戒指\r\n23 戒指\r\n24 手镯\r\n25 护身符 \r\n26 手镯\r\n27 腰带\r\n28 靴子\r\n29 魔血石\r\n30 勋章\r\n17 军鼓\r\n18 马牌\r\n;==========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查人物身上指定位置装备类型", "CHECKUSEITEM");

                    cd = new CompletionData("检查人物道术力", "功能：\r\n    检查人物道术力上限及下限值 \r\n\r\n格式：\r\n\r\nCHECKSC 控制符 < = > ?  道术下限 控制符 < = > ? 道术上限 \r\n\r\n;========================================== \r\n[@checklevel0]\r\n#IF\r\nCHECKSC > 30 > 40\r\n#SAY\r\n你的道术大于30-40 \r\n;==========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查人物道术力", "CHECKSC");

                    cd = new CompletionData("检查人物金刚石数量", "功能：\r\nCHECKGAMEDIAMOND //检查人物金刚石数量 \r\n\r\n格式：\r\n================================\r\n检查人物金刚石数量 控制符 < = > ? 点数 \r\n\r\n#IF\r\nCHECKGAMEDIAMOND > 2000\r\n#SAY\r\n你的金刚石数量超过了2000颗,可以进行锻造。\r\n#ELSESAY\r\n你没有2000颗金刚石,不能进行锻造。\\\r\n================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查人物金刚石数量", "CHECKGAMEDIAMOND");

                    cd = new CompletionData("检查人物金币数量", "功能：\r\nCHECKGOLD //检查人物金币 \r\n\r\n格式：\r\n================================\r\n检查人物金币\r\n\r\n#IF\r\nCHECKGOLD 2000\r\n#ACT\r\nTAKE 金币 2000\r\nMAPMOVE 0 326 38 \r\nclose\r\n#ELSESAY\r\n你没有钱怎么帮你进行导航呢？\\\r\n想想办法吧。\\\r\n\r\n================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查人物金币数量", "CHECKGOLD");

                    cd = new CompletionData("检查人物队伍中成员数量", "功能：\r\nCHECKGROUPCOUNT < = >  数字 M2 //获取组队人员数量到人物变量M2 \r\n\r\n格式：\r\n============================\r\n#IF\r\nCHECKGROUPCOUNT > 1 M2 \r\n#SAY\r\n目前你的队伍中有<$STR(M2)>人。\r\n#ELSESAY\r\n目前你没有组队！\\\r\n<返回/@MAIN>\r\n===========================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查人物队伍中成员数量", "CHECKGROUPCOUNT");

                    cd = new CompletionData("编组地图随机传送", "GROUPMAPTING 地图代码 Y X 随机范围 \r\n//编组地图随机传送");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("编组地图随机传送", "GROUPMAPTING");

                    cd = new CompletionData("检查人物附加属性点", "功能：\r\n    检查人物附加属性点。 \r\n\r\n格式：\r\nCHECKBONUSPOINT 控制符 < = > 点数\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查人物附加属性点", "CHECKBONUSPOINT");

                    cd = new CompletionData("检查人物魔法力", "功能：\r\n    检查人物魔法力上限及下限值 \r\n\r\n格式：\r\n\r\nCHECKMC 控制符 < = > ? 魔法下限 控制符 < = > ? 魔法上限 \r\n\r\n;========================================== \r\n[@checklevel0]\r\n#IF\r\nCHECKMC > 30 > 40\r\n#SAY\r\n你的魔法力大于30-40 \r\n;==========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查人物魔法力", "CHECKMC");

                    cd = new CompletionData("检查地图怪物数量(新)", "功能：\r\n    可以检查一个地图内的怪物数量。 \r\n\r\n格式：\r\nCHECKMAPMONCOUNT 地图号 < = > 数量 地图号可以是Self,表示当前地图 \r\n\r\n;========================================== \r\n[@Main] \r\n#IF \r\nCHECKMAPMONCOUNT Self > 1 \r\n#SAY \r\n当前地图的怪物多于1只\r\n#ELSESAY\r\n当前地图的怪物少于1只\r\n;==========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查地图怪物数量(新)", "CHECKMAPMONCOUNT");

                    cd = new CompletionData("检查地图怪物数量", "功能：\r\n    可以检查一个地图内的怪物数量。 \r\n\r\n格式：\r\nCheckMonMap 地图号  数量 \r\n\r\n[@Main]\r\n#IF\r\nCheckMonMap 3 30\r\n#SAY\r\n地图3的内怪物多于100只\r\n#ELSESAY\r\n地图3的怪物少于100只 \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查地图怪物数量", "CheckMonMap");

                    cd = new CompletionData("检查地图灵气值", "功能：\r\n    检查地图灵气值。 \r\n\r\n格式：\r\nCHECKMAPNIMBUSCOUNT MAP(地图) < = > 数量 //检测地图灵气数量 \r\n\r\n#IF\r\nCHECKMAPNIMBUSCOUNT 3 > 200\r\n#SAY\r\n盟重地图的灵气郁积浓厚,勇士们赶快前往。\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查地图灵气值", "CHECKMAPNIMBUSCOUNT");

                    cd = new CompletionData("检查地图范围内怪物数(新)", "功能：\r\n    可以检查一个坐标范围内指定怪物的数量。 \r\n\r\n格式：\r\nCHECKMAPRANGEMONNAMECOUNT 地图名(Self指当前地图) X Y 范围 怪物名字(*指所有) < > = ? 数量\r\n\r\n;==========================================\r\n[@Main] \r\n#IF \r\nCHECKMAPRANGEMONNAMECOUNT Self 330 330 10 鸡 < 100 \r\n#SAY \r\n当前地图的(330,330)坐标10范围内的鸡少于100只 \r\n#ELSESAY \r\n当前地图的(330,330)坐标10范围内的鸡多于100只 \r\n;==========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查地图范围内怪物数(新)", "CHECKMAPRANGEMONNAMECOUNT");

                    cd = new CompletionData("检查城堡占领天数", "功能：\r\n    检查沙巴克占领天数 \r\n\r\n格式：\r\n================================\r\n#if\r\n  CASTLECHANGEDAY > 7 \r\n#say\r\n你已经占领沙城7天以上.\r\n================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查城堡占领天数", "CASTLECHANGEDAY");

                    cd = new CompletionData("检查城堡战争天数", "功能：\r\n    可以检查上次攻城到现在的天数。 \r\n\r\n格式：\r\n    CASTLEWARAY   控制符 < = >  天数\r\n;==========================================\r\n[@Main]\r\n#IF\r\n    CASTLEWARAY   > 3\r\n#SAY\r\n上次攻城到现在超过三天\r\n#ELSESAY\r\n上次攻城到现在未够三天\r\n;==========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查城堡战争天数", "CASTLEWARAY");

                    cd = new CompletionData("检查城门状态", "功能：\r\n    可以检查沙城门状态。 \r\n\r\n格式：\r\n;==========================================\r\n#if\r\n  CHECKCASTLEDOOR 损坏\r\n#say\r\n城门损坏 \r\n\r\n#if\r\n  CHECKCASTLEDOOR 开启\r\n#say\r\n城门开启\r\n\r\n#if\r\n  CHECKCASTLEDOOR 关闭\r\n#say\r\n城门关闭\r\n;==========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查城门状态", "CHECKCASTLEDOOR");

                    cd = new CompletionData("检查夫妻另一方是否在线", "功能：\r\n\r\nCHECKDEARONLINE //检测夫妻一方是否在线\r\n\r\n格式：\r\n\r\n[@main]\r\n#IF\r\ncheckmarry\r\n#ELSEACT\r\nMESSAGEBOX 你都没结婚,来查看什么？\r\nBREAK\r\n#IF\r\nGENDER MAN\r\n#ACT\r\nDELAYCALL 10 @男方\r\nBREAK\r\n#IF\r\nGENDER WOMAN\r\n#ACT\r\nDELAYCALL 10 @女方\r\nBREAK\r\n\r\n[@男方]\r\n#IF\r\nCHECKDEARONLINE\r\n#SAY\r\n你的老婆当前正在线！\r\n#ELSESAY\r\n你的老婆不在线！\\ \\\r\n<返回/@MAIN>\r\n\r\n[@女方]\r\n#IF\r\nCHECKDEARONLINE\r\n#SAY\r\n你的老公当前正在线！\r\n#ELSESAY\r\n你的老公不在线！\\ \\\r\n<返回/@MAIN>\r\n;==========================================\r\n\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查夫妻另一方是否在线", "CHECKDEARONLINE");

                    cd = new CompletionData("检查夫妻是否在同一地图", "功能：\r\n\r\nCHECKDEARONMAP //检测夫妻一方是否在XXX地图,支持SELF(是否同一地图)\r\n\r\n格式：\r\n\r\n[@main]\r\n#IF\r\ncheckmarry\r\n#ELSEACT\r\nMESSAGEBOX 你都没结婚,来查看什么？\r\nBREAK\r\n#IF\r\nGENDER MAN\r\n#ACT\r\nDELAYCALL 10 @男方\r\nBREAK\r\n#IF\r\nGENDER WOMAN\r\n#ACT\r\nDELAYCALL 10 @女方\r\nBREAK\r\n\r\n[@男方]\r\n#IF\r\nCHECKDEARONMAP SELF\r\n#SAY\r\n你的老婆当前和你在同一地图内！\r\n#ELSESAY\r\n你的老婆和你不在同一地图内！\\ \\\r\n<返回/@MAIN>\r\n\r\n[@女方]\r\n#IF\r\nCHECKDEARONMAP SELF\r\n#SAY\r\n你的老公当前和你在同一地图内！\r\n#ELSESAY\r\n你的老公和你不在同一地图内！\\ \\\r\n<返回/@MAIN>\r\n;==========================================\r\n\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查夫妻是否在同一地图", "CHECKDEARONMA");

                    cd = new CompletionData("检查字符串是否在指定文件中", "功能：\r\n    可以检查字符串是否在指定文件中。 \r\n\r\n格式：\r\nCHECKSTRINGLIST .\\QuestDiary\\变量\\押镖道具.txt $STR(S1) //检查$STR(S1)是否包含在 押镖道具.txt 中\r\n\r\n;==========================================\r\n;检查字符串是否在指定文件中\r\n[@CHECKSTRINGLIST]\r\n#IF \r\nCHECKSTRINGLIST .\\QuestDiary\\变量\\押镖道具.txt $STR(S1)\r\n#SAY\r\n你的是$STR(S1)。\r\n#ELSESAY\r\n你的不是$STR(S1)。 \r\n;==========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查字符串是否在指定文件中", "CHECKSTRINGLIST");

                    cd = new CompletionData("检查对面的是不是师傅", "功能：\r\n\r\n站在对面的是不是师傅 \r\n\r\n格式：\r\n\r\n#IF \r\n\r\nCHECKPOSEISMASTER\r\n\r\n#SAY\r\n\r\n站在你对的面的是您的师傅\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查对面的是不是师傅", "CHECKPOSEISMASTER");

                    cd = new CompletionData("检查对面的是不是徒弟", "功能：\r\n\r\nCHECKPOSEMASTER //检测对面是否是徒弟\r\n\r\n格式：\r\n\r\n#IF\r\nCHECKPOSEMASTER\r\n#SAY\r\n对面的人物还是徒弟\r\n#ELSESAY\r\n对面的人不是徒弟\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查对面的是不是徒弟", "CHECKPOSEMASTER");

                    cd = new CompletionData("检查对面人物是否结婚", "功能：\r\nCHECKPOSEMARRY //检查对方是否结婚 \r\n\r\n格式：\r\n============================\r\n#IF\r\nCHECKPOSEMARRY\r\n#SAY\r\n对面的人已婚。\r\n#ELSESAY\r\n对面的人未婚。\\ \r\n<返回/@MAIN>\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查对面人物是否结婚", "CHECKPOSEMARRY");

                    cd = new CompletionData("检查对面人物的性别", "功能：\r\n    可以检查对面人物的性别(不要求面对面)。 \r\n\r\n格式：\r\nCHECKPOSEGENDER 性别 (MAN,男,WOMAN,女)\r\n\r\n;==========================================\r\n;检查对面人物是否为男的\r\n[@checkposegender0]\r\n#IF\r\n  CHECKPOSEGENDER MAN\r\n#SAY\r\n你对面的人物是男的。\r\n#ELSESAY\r\n你对面的人物不是男的。\r\n;==========================================\r\n\r\n;检查对面人物是否为女的\r\n[@checkposegender1]\r\n#IF\r\n  CHECKPOSEGENDER WOMAN\r\n#SAY\r\n你对面的人物是女的。\r\n#ELSESAY\r\n你对面的人物不是女的。\r\n;==========================================\r\n\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查对面人物的性别", "CHECKPOSEGENDER");

                    cd = new CompletionData("检查对面人物的等级", "功能：\r\n    可以检查对面人物的等级(不要求面对面)。 \r\n\r\n格式：\r\nCHECKPOSELEVEL 控制符 < = > ? 等级数(1 - 65535)\r\n\r\n;==========================================\r\n;检查对面人物的等级是否等于指定级别\r\n[@checkposelevel0]\r\n#IF\r\n  CHECKPOSELEVEL = 50\r\n#SAY\r\n您对面人物的等级等于50级。\r\n#ELSESAY\r\n您对面人物的等级不等于50级。\r\n;==========================================\r\n\r\n;==========================================\r\n;检查对面人物的等级是否大于指定级别\r\n[@checkposelevel1]\r\n#IF\r\n  CHECKPOSELEVEL > 50\r\n#SAY\r\n您对面人物的等级大于50级。\r\n#ELSESAY\r\n您对面人物的等级不大于50级。\r\n;==========================================\r\n\r\n;==========================================\r\n;检查对面人物的等级是否小于指定级别\r\n[@checkposelevel2]\r\n#IF\r\n  CHECKPOSELEVEL < 50\r\n#SAY\r\n您对面人物的等级小于50级。\r\n#ELSESAY\r\n您对面人物的等级不小于50级。\r\n;=========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查对面人物的等级", "CHECKPOSELEVEL");

                    cd = new CompletionData("检查对面人物站的位置", "功能：\r\n    可以检查对面人物站的位置性别(要求面对面)。 \r\n\r\n格式：\r\nCHECKPOSEDIR 控制符(1,2)\r\n\r\n;==========================================\r\n;检查对面人物是否面对面\r\n[@checkposedir0]\r\n#IF\r\n  CHECKPOSEDIR\r\n#SAY\r\n你二个站的位置正确。\r\n#ELSESAY\r\n你二个站的位置不正确。\r\n;==========================================\r\n\r\n;==========================================\r\n;检查对面人物是否面对面,而且要求二个人相同性别\r\n[@checkposelevel0]\r\n#IF\r\n  CHECKPOSEDIR 1\r\n#SAY\r\n你二个站的位置及性别一样。\r\n#ELSESAY\r\n你二个站的位置不正确或性别不一样。\r\n;==========================================\r\n\r\n;==========================================\r\n;检查对面人物是否面对面,而且要求二个人不同性别\r\n[@checkposelevel0]\r\n#IF\r\n  CHECKPOSEDIR 2\r\n#SAY\r\n你二个站的位置及性别不一样。\r\n#ELSESAY\r\n你二个站的位置不正确或性别一样。\r\n;==========================================\r\n\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查对面人物站的位置", "CHECKPOSEDIR");

                    cd = new CompletionData("检查师徒另一方是否在线", "功能：\r\n\r\nCHECKMASTERONLINE //检查师徒另一方是否在线\r\n\r\n格式：\r\n\r\n;==========================================\r\n\r\n[@main]\r\n#IF\r\nHAVEMASTER\r\n#ELSEACT\r\nMESSAGEBOX 你都没有建立师徒关系,来查看什么？\r\nBREAK\r\n#IF\r\nCHECKISMASTER\r\n#ACT\r\nDELAYCALL 10 @师傅\r\nBREAK\r\n#IF\r\nCHECKMASTER\r\n#ACT\r\nDELAYCALL 10 @徒弟\r\nBREAK\r\n\r\n[@师傅]\r\n#IF\r\nCHECKMASTERONLINE\r\n#SAY\r\n你的徒弟当前正在线！\r\n#ELSESAY\r\n你的徒弟不在线！\\ \\\r\n<返回/@MAIN>\r\n\r\n[@徒弟]\r\n#IF\r\nCHECKMASTERONLINE\r\n#SAY\r\n你的师傅当前正在线！\r\n#ELSESAY\r\n你的师傅不在线！\\ \\\r\n<返回/@MAIN>\r\n;==========================================\r\n\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查师徒另一方是否在线", "CHECKMASTERONLINE");

                    cd = new CompletionData("检查师徒是否在同一地图", "功能：\r\n\r\nCHECKMASTERONMAP //检测师傅(或徒弟)是否在XXX地图,支持SELF(是否同一地图)\r\n\r\n格式：\r\n\r\n;==========================================\r\n\r\n[@main]\r\n#IF\r\nHAVEMASTER\r\n#ELSEACT\r\nMESSAGEBOX 你都没有建立师徒关系,来查看什么？\r\nBREAK\r\n#IF\r\nCHECKISMASTER\r\n#ACT\r\nDELAYCALL 10 @师傅\r\nBREAK\r\n#IF\r\nCHECKMASTER\r\n#ACT\r\nDELAYCALL 10 @徒弟\r\nBREAK\r\n\r\n[@师傅]\r\n#IF\r\nCHECKMASTERONMAP SELF\r\n#SAY\r\n你的徒弟当前和你在同一地图内！\r\n#ELSESAY\r\n你的徒弟和你不在同一地图内！\\ \\\r\n<返回/@MAIN>\r\n\r\n[@徒弟]\r\n#IF\r\nCHECKMASTERONMAP SELF\r\n#SAY\r\n你的师傅当前和你在同一地图内！\r\n#ELSESAY\r\n你的师傅和你不在同一地图内！\\ \\\r\n<返回/@MAIN>\r\n;==========================================\r\n\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查师徒是否在同一地图", "CHECKMASTERONMAP");

                    cd = new CompletionData("检查当前地图脱机人数", "功能：\r\nOFFLINEPLAYERCOUNT //检查当前地图脱机人数 \r\n\r\n格式：\r\n#IF\r\n\r\nOFFLINEPLAYERCOUNT > < = 数量\r\n\r\n与检查当前地图命令配合,就可以检测某地图脱机人数的数量\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查当前地图脱机人数", "OFFLINEPLAYERCOUNT");

                    cd = new CompletionData("检查当前日期", "功能：\r\n   检查当前日期 \r\n\r\n格式： \r\n\r\nCHECKCURRENTDATE < = > 2008-04-05 \r\n\r\n//检测日期是否 < = > 2008-04-05 检测后日期差存放在人物的$STR(M0)中, 如果检测的日期要迟于当前机器日期,$STR(M0)为负数 (提示：文本中按F5可获得当前日期时间) \r\n\r\n;========================================== \r\n;检查当天是否等于指定日期\r\n[@CHECKCURRENTDATE0]\r\n#IF\r\nCHECKCURRENTDATE = 2010-05-18\r\n#SAY\r\n今天是：<$DATETIME>,恭喜你现在可以领取奖品了。\r\n#ELSESAY\r\n今天是：<$DATETIME>,\\\r\n只有在2010年5月18号当天才可以领取奖品。\\\r\n;==========================================\r\n\r\n;==========================================\r\n;检查当天是否大于指定日期\r\n[@CHECKCURRENTDATE1]\r\n#IF\r\nCHECKCURRENTDATE > 2010-06-18\r\n#SAY\r\n今天是：<$DATETIME>,恭喜你现在可以领取奖品了。\r\n#ELSESAY\r\n今天是：<$DATETIME>,\\\r\n距2010年6月18号领取奖品时间过去了<$STR(M0)>天。\\\r\n;==========================================\r\n\r\n;==========================================\r\n;检查当天是否小于指定日期\r\n[@CHECKCURRENTDATE2]\r\n#IF\r\nCHECKCURRENTDATE < 2010-07-18\r\n#SAY\r\n今天是：<$DATETIME>。\r\n#ELSESAY\r\n今天是：<$DATETIME>,\\\r\n距2010年7月18号领取奖品时间还剩下<$STR(M0)>天。\\\r\n;==========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查当前日期", "CHECKCURRENTDATE");

                    cd = new CompletionData("检查指定类型装备的属性点", "功能：\r\n   检查指定类型装备的属性点 \r\n\r\n格式：\r\nCHECKITEMADDVALUE 指定类型 属性位置(0-12) < = > ? 检查数值(0-255) \r\n\r\n;========================================== \r\n;检查装备指定属性幸运点是否等于5或大于5\r\n[@CHECKITEMADDVALUE]\r\n#IF\r\nCHECKITEMADDVALUE 1 3 ? 5\r\n#SAY\r\n超级祝福油只能提升5点幸运以下的武器！\\\r\n目前你的武器幸运已经达到5点或5点以上\\\r\n超级祝福油将无效。\\\r\n#ACT\r\nBREAK\r\n#IF\r\nCHECKUSEITEM 1\r\n#ACT\r\nUPGRADEITEMEX 1 3 0 1 0\r\n#ELSEACT\r\nmessagebox 请把武器带在身上！否则无法升级！\r\n;==========================================\r\n连击引擎属性位置 ：内容太多不好显示");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查指定类型装备的属性点", "CHECKITEMADDVALUE");

                    cd = new CompletionData("检查放入装备的名称特征字符", "功能：\r\n   检查放入装备的名称特征字符 \r\n\r\n格式：\r\nCHECKPOSDLGITEMNAME 特征字符 检查条件需要配合QUERYITEMDLG命令\r\n\r\n;========================================== \r\n\r\n[@main]\r\n#ACT\r\nDELAYCALL 10 @DELAY_UPGRADEDLGITEM\r\n\r\n[@DELAY_UPGRADEDLGITEM]\r\n#ACT\r\nQUERYITEMDLG 查询装备特征字符 @CHECKPOSDLGITEMNAME 0\r\n\r\n[@CHECKPOSDLGITEMNAME] \r\n#IF\r\nCHECKPOSDLGITEMNAME 星王 \r\n#SAY\r\n你放入的<$DLGITEMNAME>,正是我需要的装备系列！\r\n#ELSESAY\r\n你提交的是什么物品？我要的可是星王套装啊！\\\r\n;=========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查放入装备的名称特征字符", "CHECKPOSDLGITEMNAME");

                    cd = new CompletionData("检查放入装备指定的属性点", "功能：\r\n   检查放入装备指定的属性点 \r\n\r\n格式：\r\nCHECKDLGITEMADDVALUE 属性位置(0-12) ><= 检查数值(0-255) 检查条件需要配合QUERYITEMDLG命令\r\n\r\n;========================================== \r\n\r\n[@main]\r\n#ACT\r\nDELAYCALL 10 @DELAY_UPGRADEDLGITEM\r\n\r\n[@DELAY_UPGRADEDLGITEM]\r\n#ACT\r\nQUERYITEMDLG 查询装备合成需求 @CHECKDLGITEMADDVALUE 0\r\n\r\n[@CHECKDLGITEMADDVALUE] \r\n#IF\r\nCHECKDLGITEMTYPE WEAPON\r\nCHECKDLGITEMNAME 木剑 \r\nCHECKDLGITEMADDVALUE 3 ? 10\r\n#ACT\r\nGETDLGITEMVALUE M3 3\r\n#SAY\r\n你的<$DLGITEMNAME>目前幸运+<$STR(M3)>\r\n#ELSESAY\r\n你提交的是什么物品？我要的可是木剑,是武器啊！\\\r\n;==========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查放入装备指定的属性点", "CHECKDLGITEMADDVALUE");

                    cd = new CompletionData("检查放入装备的全名", "功能：\r\n   检查放入装备的名称全名 \r\n\r\n格式：\r\nCHECKDLGITEMNAME 名称 检查条件需要配合QUERYITEMDLG命令\r\n\r\n;========================================== \r\n\r\n[@main]\r\n#ACT\r\nDELAYCALL 10 @DELAY_UPGRADEDLGITEM\r\n\r\n[@DELAY_UPGRADEDLGITEM]\r\n#ACT\r\nQUERYITEMDLG 查询装备特征字符 @CHECKDLGITEMNAME 0\r\n\r\n[@CHECKDLGITEMNAME]\r\n#IF\r\nCHECKDLGITEMNAME 星王项链(战)\r\n#SAY\r\n你放入的<$DLGITEMNAME>,正是我需要的装备系列！\r\n#ELSESAY\r\n你提交的是什么物品？我要的可是星王项链(战)啊！\\\r\n;==========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查放入装备的全名", "CHECKDLGITEMNAME");

                    cd = new CompletionData("检查放入装备的类型", "功能：\r\n   检查放入装备的类型 \r\n\r\nCHECKDLGITEMTYPE　DRESS //检测是否衣服\r\n　　　　　　　　　WEAPON //检测是否武器\r\n　　　　　　　　　MEDAL //勋章\r\n　　　　　　　　　NECKLACE //项链\r\n　　　　　　　　　HELMET //头盔\r\n　　　　　　　　　ARMRING //手镯\r\n　　　　　　　　　RING //戒指\r\n　　　　　　　　　BOOTS //靴子\r\n　　　　　　　　　BELT //腰带\r\n　　　　　　　　　BUJUK //宝石\r\n\r\n格式：\r\nCHECKDLGITEMTYPE 类型 检查条件需要配合QUERYITEMDLG命令\r\n\r\n;========================================== \r\n\r\n[@main]\r\n#ACT\r\nDELAYCALL 10 @DELAY_UPGRADEDLGITEM\r\n\r\n[@DELAY_UPGRADEDLGITEM]\r\n#ACT\r\nQUERYITEMDLG 查询装备特征字符 @CHECKDLGITEMTYPE 0\r\n\r\n[@CHECKDLGITEMTYPE]\r\n#IF\r\nCHECKDLGITEMNAME WEAPON\r\n#SAY\r\n你放入的<$DLGITEMNAME>,正是我需要的装备系列！\r\n#ELSESAY\r\n你提交的是什么物品？我要的可是武器啊！\\\r\n;==========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查放入装备的类型", "CHECKDLGITEMTYPE");

                    cd = new CompletionData("检查是否建立师徒关系", "功能：\r\n\r\nHAVEMASTER //检查是否建立师徒关系\r\n\r\n格式：\r\n\r\n[@main]\r\n#IF\r\nHAVEMASTER\r\n#SAY\r\n你建立了师徒关系！\r\n#ELSESAY\r\n你都没有建立师徒关系。\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查是否建立师徒关系", "HAVEMASTER");

                    cd = new CompletionData("检查是否攻城期间", "功能：\r\nCHECKCASTLEWAR //检查是否攻城期间 \r\n\r\nISONCASTLEWAR //同上 \r\n\r\n格式：\r\n============================\r\n#IF\r\nCHECKCASTLEWAR\r\n#SAY\r\n现在是沙巴克争夺战时间,赶快组织成员进攻沙城吧！\r\n#ELSESAY\r\n目前不是沙巴克争夺战时间。\\\r\n<返回/@MAIN>\r\n===========================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查是否攻城期间", "CHECKCASTLEWAR");

                    cd = new CompletionData("检查是否被人物所杀", "功能：\r\n　　　检测是否被人物所杀 \r\n\r\n格式：\r\n　　　 KillByHum\r\n\r\n;==========================================\r\n[@KillByHum]\r\n#If\r\nKillByHum\r\n#Act \r\nSendMsg 5 [提示]:你被<$KILLER>杀害.!\r\nClose\r\n#ElseAct\r\nSendMsg 5 [提示]:你被<$MONKILLER>杀害.!\r\nClose\r\n;==========================================\r\n\r\n注意：\r\n如果被玩家宝宝所杀.反馈的也会是玩家信息 \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查是否被人物所杀", "KILLBYHUM");

                    cd = new CompletionData("检查聚灵珠经验是否已满", "功能：\r\n   CHECKITEMDURACOUNT //检查聚灵珠经验是否已满 \r\n\r\n格式：\r\nCHECKITEMDURACOUNT 物品名是否聚满(0= 未满的,1= 已满的) < = >  数量//检测包裹指定物品名是否满持久数量(可用于聚灵珠等...) \r\n\r\n;========================================== \r\n\r\n[@main] \r\n#IF\r\nCHECKITEMDURACOUNT 聚灵珠(大) 1 > 9 \r\n#SAY\r\n你的10个聚灵珠(大)已经都已经累积满了。\r\n#ELSESAY\r\n你没有足够已满经验的聚灵珠(大)。\\\r\n;========================================== \r\n\r\n \r\n\r\n相关DB设置：聚灵珠增加使用等级限制,设置db的needLevel\r\n\r\n乾坤玉璧设置：聚灵珠\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查聚灵珠经验是否已满", "CHECKITEMDURACOUNT");

                    cd = new CompletionData("检查输入仓库密码错误次数", "功能：\r\n    检查输入仓库密码错误次数 \r\n\r\n格式：\r\n\r\nPASSWORDERRORCOUNT 控制符 < = > ? 范围(1 - 65535) \r\n\r\n;==========================================\r\n;检查输入错误的次数是否等于指定值\r\n[@passworderror1]\r\n#IF\r\nPASSWORDERRORCOUNT = 3 \r\n#SAY\r\n您的错误次数等于3。\r\n#ELSESAY\r\n您的错误次数不等于3。\r\n;==========================================\r\n\r\n;==========================================\r\n;检查输入错误的次数是否大于指定值\r\n[@passworderror2]\r\n#IF\r\nPASSWORDERRORCOUNT > 3\r\n#SAY\r\n您的错误次数大于3。\r\n#ELSESAY\r\n您的错误次数不大于3。\r\n;==========================================\r\n\r\n;==========================================\r\n;检查输入错误的次数是否小于指定值\r\n[@passworderror3]\r\n#IF\r\nPASSWORDERRORCOUNT < 3\r\n#SAY\r\n您的错误次数小于3。\r\n#ELSESAY\r\n您的错误次数不小于3。\r\n;==========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查输入仓库密码错误次数", "PASSWORDERRORCOUNT");

                    cd = new CompletionData("检查人物包裹物品的品质", "功能：\r\n    检查人物包裹物品的品质 \r\n\r\n格式：\r\ncheckdura 物品 品质\r\n\r\n;==========================================\r\n\r\n[@免费]\r\n#IF\r\ncheckitem 金矿 3\r\ncheckdura 金矿 15 //这里检测的是是否有纯度15的金矿\r\n#SAY\r\n啊？你真的把这些白给我？\\\r\n真没想到现如今还有你这样的人,看来还有人情啊。\\\r\n我不能白拿这么贵重的东西。\\\r\n你有没有听说过命运之刃？\\\r\n还有在北门街道小店修理武器之后,有没有仔细看过人？\\ \\\r\n<返回/@kang>\r\n#ACT\r\ntake 金矿 2\r\ntakecheckitem // 收取 checkdura 过的物品\r\nBREAK\r\n;==========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查人物包裹物品的品质", "CHECKDURA");

                    cd = new CompletionData("检查人物押运任务是否进行中", "功能：\r\n\r\nISESCORTING //检测押运任务是否进行中\r\n\r\n格式：\r\n\r\n;==========================================\r\n\r\n[@main] \r\n#IF\r\nISESCORTING\r\n#SAY\r\n你的镖车任务正在进行中！\r\n#ELSESAY\r\n你根本就没有领取镖车任务！\\ \\\r\n<返回/@MAIN> \r\n;==========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查人物押运任务是否进行中", "ISESCORTING");

                    cd = new CompletionData("检查人物是否在安全区", "功能：\r\n检测人物是否在安全区。\r\n\r\n格式：\r\nInSafeZone\r\n;==========================================\r\n[@InSafeZone]\r\n#IF\r\nInSafeZone\r\n#SAY\r\n你在安全区里做什么呢？\r\n;==========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查人物是否在安全区", "INSAFEZONE");

                    cd = new CompletionData("检查人物是否在指定的[地图XY坐标]范围内", "功能：\r\n    检测人物是否在指定的[地图XY坐标]范围内 \r\n\r\n格式：\r\nCHECKINMAPRANGE 地图 X Y 范围\r\n\r\n;==========================================\r\n[@Main]\r\n#IF\r\nCHECKINMAPRANGE 3 330 330 10\r\n#SAY\r\n你在指定坐标范围内。\r\n#ELSESAY\r\n对不起,您没有在指定坐标范围内！\r\n;==========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查人物是否在指定的[地图XY坐标]范围内", "CHECKINMAPRANGE");

                    cd = new CompletionData("检查人物是否重叠", "功能：\r\n检测人物是否重叠。 \r\n\r\n格式：\r\nISDUPMODE\r\n;==========================================\r\n[@ISDUPMODE]\r\n#IF\r\nISDUPMODE\r\n#SAY\r\n请找一个空位置,不能与别人站在一起。\r\n;==========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查人物是否重叠", "ISDUPMODE");

                    cd = new CompletionData("检查人物称号", "功能：\r\n检测人物称号。 操作 < = >\r\n\r\n格式：\r\nCHECKTITLE\r\n;==========================================\r\n[@CHECKTITLE]\r\n#IF \r\nCHECKTITLE 传奇之星 < 1 //检测称号是否存在,小于1则给授予称号\r\n#ACT\r\nCONFERTITLE 传奇之星 //授予称号 \r\n\r\n \r\n\r\n[@CHECKTITLE]\r\n#IF \r\nCHECKTITLE 传奇之星 = 1 //检测称号是否存在,等于1则给删除称号\r\n#ACT\r\nDEPRIVETITLE 传奇之星\r\n#ELSESAY\r\n传奇之星称谓都不存在,你还来删除什么？\r\n;==========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查人物称号", "CHECKTITLE");

                    cd = new CompletionData("检查人物镖车是否在身边", "功能：\r\n\r\nCheckEscortInNear //检测镖车是否在身边\r\n\r\n格式：\r\n\r\n;==========================================\r\n\r\n[@main] \r\n#IF\r\nCheckEscortInNear\r\n#SAY\r\n你的镖车在旁边！\r\n#ELSESAY\r\n你的镖车离你太远了！\\ \\\r\n<返回/@MAIN> \r\n;==========================================\r\n\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查人物镖车是否在身边", "CHECKESCORTINNEAR");

                    cd = new CompletionData("检查地图中所有人是否同一行会", "功能：\r\n\r\nIsSameGuildOnMap 地图名 //检查地图中所有人是否同一行会\r\n\r\n格式：\r\n\r\n;==========================================\r\n\r\n[@main] \r\n#IF\r\nIsSameGuildOnMap 3 \r\n#SAY\r\n你的行会目前霸占了盟重地图！\r\n#ELSESAY\r\n盟重地图还没被你行会占领！\\ \\\r\n<返回/@MAIN> \r\n;========================================== \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查地图中所有人是否同一行会", "ISSAMEGUILDONMAP");

                    cd = new CompletionData("检查当前在线最高各项属性玩家", "功能：\r\nIsHigh L/P/D/M/S //检查是否为当前在线最高等级/PK值/攻击/魔法/道术人物 \r\n\r\n格式：\r\n================================\r\n\r\nIsHigh L/P/D/M/S \r\n　　　参数二为L时表示检查是否为当前在线最高等级人物.P为PK值最高.D为攻击最高.M为魔法力最高.S为道术最高\r\n\r\n[@IsHighPlayer]\r\n#If\r\nIsHigh P\r\n#Act \r\nSendMsg 5 [提示]:您为当前在线人物中最高PK值人物.杀人不眨眼的大恶魔一个.!\r\nClose\r\n\r\n================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查当前在线最高各项属性玩家", "ISHIGH");

                    cd = new CompletionData("检查时间命令", "功能：\r\n\r\n注意：检测时间参数为：HOUR 17 18 时钟17-18点\r\n注意：检测时间参数为：MIN 00 01 分钟00-01点之间,一起检测就是检测是不是17点0分到18点01分之间 \r\n\r\n#IF\r\nHOUR 17 18 \r\nMIN 00 01\r\n#ACT\r\nMAP D001 \r\n#ELSEACT\r\nMESSAGEBOX 对不起：<$USERNAME>,进入的时间为17：00-18：00之间。\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查时间命令", "HOUR");

                    cd = new CompletionData("检查星期命令", "功能：\r\n\r\nDAYOFWEEK 测试星期(MON\\TUE\\WED\\THU\\FRI\\SAT\\SUN) \r\n\r\n[@MAIN]\r\n<星期一/@星期一> <星期二/@星期二> <星期三/@星期三> <星期四/@星期四>\\ \r\n\r\n<星期五/@星期五> <星期六/@星期六> <星期天/@星期天>\\ \r\n\r\n[@星期六]\r\n#If\r\nDAYOFWEEK SAT\r\n#SAY\r\n今天是星期六！\r\n#ELSESAY\r\n今天不是星期六！ \r\n\r\n[@星期五]\r\n#If\r\nDAYOFWEEK FRI\r\n#SAY\r\n今天是星期五！\r\n#ELSESAY\r\n今天不是星期五！\r\n\r\n[@星期四]\r\n#If\r\nDAYOFWEEK THU\r\n#SAY\r\n今天是星期四！\r\n#ELSESAY\r\n今天不是星期四！ \r\n\r\n[@星期三]\r\n#If\r\nDAYOFWEEK WED\r\n#SAY\r\n今天是星期三！\r\n#ELSESAY\r\n今天不是星期三！ \r\n\r\n[@星期二]\r\n#If\r\nDAYOFWEEK TUE\r\n#SAY\r\n今天是星期二！\r\n#ELSESAY\r\n今天不是星期二！ \r\n\r\n[@星期一]\r\n#If\r\nDAYOFWEEK MON\r\n#SAY\r\n今天是星期一！\r\n#ELSESAY\r\n今天不是星期一！ \r\n\r\n[@星期天]\r\n#If\r\nDAYOFWEEK SUN\r\n#SAY\r\n今天是星期天！\r\n#ELSESAY\r\n今天不是星期天！ \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查星期命令", "DAYOFWEEK");

                    cd = new CompletionData("地图允许穿人", "参数：\r\n　　　RUNHUMAN //允许地图穿人 \r\n\r\n说明：\r\n　　　允许地图玩家可穿人。\r\n\r\n[3 盟重省] RUNHUMAN \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("地图允许穿人", "RUNHUMAN");

                    cd = new CompletionData("地图允许穿怪", "参数：\r\n　　　RUNMON //允许地图穿怪 \r\n\r\n说明：\r\n　　　允许地图玩家可穿怪。\r\n\r\n[3 盟重省] RUNMON \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("地图允许穿怪", "RUNMON");

                    cd = new CompletionData("地图优化参数", "参数：\r\n　　　OPTI \r\n\r\n说明：\r\n　　　带有该参数的地图,会提前申请需要的部分内存进行优化。PS：适合用于怪多,人经常去升级,或PK多的地图,建议使用。\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("地图优化参数", "OPTI");

                    cd = new CompletionData("地图允许挖宝", "参数：\r\n　　　DIGITEM(X) //地图允许挖宝 \r\n\r\nX=0不加宝藏\r\nX的密集度范围建议1~200,数字越大,密集度越小,每个地图加载了多少宝藏可以在M2加载时看到,\r\nX取值多少请根据地图大小和个人意愿自行确定\r\n宝藏一定时间内会被挖完,间隔数分钟后会自动适量补充 \r\n\r\n说明：\r\n　　　挖宝区域,地图后有DIGITEM标志的都可以进行挖宝。\r\n\r\n[0159 武器店] DIGITEM(20) \r\n\r\n挖宝配置设置：\r\n\r\n[+] 挖宝物品配置文件：\\Envir\\DigItemList.txt,修改后,在M2菜单-重新加载-地图挖宝配置 生效\r\n挖到的宝物是否提示,请在\\Envir\\HintItemList.txt 添加,可以包括 声望,经验,元宝 等等。\r\n文件内容如下：\r\n-----------------------------------------------------------------------------\r\n;[地图名] 例如：[3] 表示是盟重的挖宝配置\r\n;难度分类 0=灵媒品质(1-50) 1=灵媒品质(51-100) 2=灵媒品质(101-150) 3=灵媒品质(151-250) 4=灵媒品质(250-255)\r\n;物品名称 难度分类 数量 几率 \r\n\r\n[3]←这里代表地图代码\r\n声望 0 50000 30\r\n经验 1 50000 40\r\n金刚石 2 5000 50\r\n内功 3 5000 60\r\n灵符 2 100 70 \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("地图允许挖宝", "DIGITEM");

                    cd = new CompletionData("地图允许摆摊", "参数：\r\n　　　STALL //地图允许摆摊 \r\n\r\n说明：\r\n　　　摆摊区域,地图后有STALL标志的都可以进行摆摊。\r\n\r\n[3 盟重省] STALL \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("地图允许摆摊", "STALL");

                    cd = new CompletionData("地图允许挖矿", "参数：\r\n　　　MINE //地图可以挖矿 \r\n\r\n说明：\r\n　　　地图内可以挖矿。\r\n\r\n[3 盟重省] MINE \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("地图允许挖矿", "MINE");

                    cd = new CompletionData("地图属于安全区域", "参数：\r\n　　　SAFE //地图属于安全区域 \r\n\r\n说明：\r\n　　　安全区域,不允许PK。\r\n\r\n[3 盟重省] SAFE \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("地图属于安全区域", "SAFE");

                    cd = new CompletionData("地图属于战斗区域", "参数：\r\nFIGHT //地图属于战斗区域 (如果设FIGHT3 ,就是行会地图常设定的可复活三次.)  \r\n\r\n刷光圈      地图  X  Y  类型  时间  伤害值   //比如在沙城入口刷新光圈！ \r\n\r\nMOBFIREBURN 3 678 336 5 9999 \r\n\r\n说明：\r\n　　　允许地图玩家可PK,一般和安全区一起使用,此地图无论是怪物或人物死亡都不会爆出物品。\r\n\r\n[3 盟重省] FIGHT \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("地图属于战斗区域", "FIGHT");

                    cd = new CompletionData("地图怪物灵敏", "增加地图参数：SensitiveCreature  \r\n\r\n作用：\r\n一旦人物/宠物进入怪物视觉范围,怪物马上有反应,表现上怪物更为灵活,例如随机到怪物旁边,怪物不会再像以前一样反应迟钝\r\n在开启了SensitiveCreature参数的地图上,道士的隐身技能更能凸显作用。\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("地图怪物灵敏", "SENSITIVECREATURE");

                    cd = new CompletionData("地图智能刷怪", "参数：\r\n　　　NOMANNOMON //地图智能刷怪 \r\n\r\n说明：\r\n　　　智能刷怪地图参数.有人才重新刷怪.节省更多的资源。\r\n\r\n[3 盟重省] NOMANNOMON \r\n");
                    BlueCheckedData.Add("地图智能刷怪", "NOMANNOMON");
                    CompletionDataList.Add(cd);

                    cd = new CompletionData("地图杀人触发", "参数：\r\n　　　KillFUNC(X) //地图杀人触发 \r\n\r\n说明：\r\n　　　人物在该地图杀人.将触发QMapEvent-0.txt的[@KillPlay数字]节.人物在该地图杀怪.将触发QMapEvent-0.txt的[@KillPlayMon数字]节(宝宝杀人杀怪有效,秒杀除外)。\r\n\r\n相关文件：\r\n\r\nMapInfo.txt 设置如下 \r\n\r\n[3 盟重省] KillFUNC(1) \r\n\r\nQFunction-0.txt 设置如下 \r\n\r\n[@PlayDie] ← 死亡触发功能下添加变量 \r\n#IF\r\n#ACT\r\nMOV A13 <$DECEDENT>\r\n\r\n示例：\r\n\r\n;==========================================\r\n[@KillPlay1]\r\n#If\r\nCheckLevelEx > 0 \r\n#Act\r\nSendMsg 0 %s在:%m(%x:%y)杀死:<$STR(A13)> \r\n\r\n[@KillPlayMon1]\r\n#If\r\nCheckLevelEx > 0 \r\n#Act\r\nSendMsg 5 怪物<$MONKILLER>在:%m(%x:%y)把<$USERNAME>干掉了\r\n;==========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("地图杀人触发", "KillFUNC");

                    cd = new CompletionData("天地结晶地图参数", "参数：\r\n　　　地图参数 COLLECTEXP(AA/BB/CC/DD/EE/F1~F4/G1~G4) \r\n\r\n说明：\r\n\r\nAA //增加的经验\r\nBB //增加的内功经验\r\nCC //打怪吸收经验比率,100则收集打怪的100%经验\r\nDD //释放需要元宝比率 注：释放需要元宝 = 释放需要元宝比率 * 经验结晶阶段\r\nEE //增加经验时间间隔(秒)\r\nF1~F4 //1~4阶段的最高经验,中间用“|”号分开\r\nG1~G4 //1~4阶段的最高内功经验\r\n\r\n示例：\r\n-------------------------------------------------------------------------------------\r\n[0122 皇宫] COLLECTEXP(8000/3200/100/3/10/600000|1200000|2400000|4800000/240000|480000|960000|1280000) \r\n\r\n释放触发：QFunction-0.txt：\r\n[@ReleaseCollectExp]\r\n#ACT\r\nQUERYVALUE 2 2 10 @ExpNow 你的天地结晶已经储存了<$COLLECTEXP>经验和<$COLLECTIPEXP>内功经验,你可以支付<$GCEPAYMENT>元宝\\提取其中的<$GAINCOLLECTEXP>经验和<$GAINCOLLECTIPEXP>内功经验 QF \r\n\r\n[@ExpNow]\r\n#IF\r\nCHECKGAMEGOLD ? $GCEPAYMENT\r\n#ACT\r\nGAMEGOLD - $GCEPAYMENT\r\nCHANGEEXP + $GAINCOLLECTEXP\r\nCHANGEIPEXP + $GAINCOLLECTIPEXP\r\nRESETCOLLECTEXPSTATE //恢复天地结晶到初始状态 \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("天地结晶地图参数", "COLLECTEXP(AA/BB/CC/DD/EE/F1~F4/G1~G4)");

                    cd = new CompletionData("新战斗地图参数", "参数：\r\n　　　Fight2 \r\n\r\n说明：\r\n　　　杀人不犯法.但是会爆装备.适合用来做一些战争PK的地图.!\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("新战斗地图参数", "FIGHT2");

                    cd = new CompletionData("地图浑水摸鱼模式", "参数：\r\n　　　SECRET(31|固定名字|2|21) //浑水摸鱼模式\r\n\r\n说明：\r\n\r\n[3 盟重省] SECRET(31|固定名字|2|21)\r\n\r\n括号里的第1参数： //第1参数非0,则本地图的人物、英雄 HP将以百分比显示。\r\n01 禁止说话\r\n02 禁止名字变色\r\n04 禁止看对方装备\r\n08 统一名字 //第2参数就是指定显示的名字\r\n16 统一装备外观 //第3参数指衣服外观,第4参数指武器外观 \r\n\r\n如果要多个功能起作用,相加对应的数字即可,\r\n例如：禁止名字变色(02) + 禁止看对方装备(04) +统一装备外观(16),等于：22,既是第1参数=22\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("地图浑水摸鱼模式", "SECRET");

                    cd = new CompletionData("禁止交易禁止丢装备", "功能：\r\n\r\n地图禁止交易禁止丢装备参数 \r\n\r\n地图参数： NOTHROWITEM //禁止丢装备\r\nNODEAL //禁止交易 \r\n\r\n示例：\r\n\r\n地图参数： NOTHROWITEM //禁止丢装备\r\nNODEAL //禁止交易 \r\n\r\n \r\n\r\n在D:\\MirServer\\Mir200\\Envir\r\n\r\n下的MapInfo.txt里\r\n\r\n列如：\r\n\r\n[0 比奇省区域] NOTHROWITEM NODEAL\r\n\r\n这样这个地图就禁止交易禁止丢装备了\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("禁止交易禁止丢装备", "NOTHROWITEM");

                    cd = new CompletionData("禁止使用技能限制", "功能：\r\n地图禁止使用技能限制,如下： \r\n\r\n示例：\r\n\r\nMIR200文件下地图配置文件里[MAPINFO.TXT]\r\n\r\n比如土城地图：\r\n\r\n[3 土城] NOTALLOWUSEMAGIC(火墙|彻地钉) \r\n\r\n;;;这样就在盟重就不允许使用 火墙和彻地钉 多个魔法使用 | 分开\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("禁止使用技能限制", "NOTALLOWUSEMAGIC");

                    cd = new CompletionData("禁止使用物品限制", "新增：\r\n \r\nNOTALLOWUSEITEMS   //允许物品带圆括号\r\n功能：\r\n地图禁止使用物品限制,如下： \r\n\r\n示例：\r\n\r\nMIR200文件下地图配置文件里[MAPINFO.TXT]\r\n\r\n比如土城地图：\r\n\r\n[3 土城] NOTALLOWUSEITEMS(回城卷|强效金创药) \r\n\r\n;;;这样就在盟重就不允许使用 回城卷和强效金创药 多个物品使用 | 分开 \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("禁止使用物品限制", "NOTALLOWUSEITEMS");

                    cd = new CompletionData("禁止切换攻击模式", "功能：\r\n   地图标志： NOSWITCHATTACKMODE //不允许切换攻击模式。 \r\n\r\n格式：\r\n\r\n地图参数：MapInfo、txt里加\r\n\r\n[G003 行会争霸地图] NOSWITCHATTACKMODE \r\n\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("禁止切换攻击模式", "NOSWITCHATTACKMODE");

                    cd = new CompletionData("禁止召唤英雄", "功能：\r\n地图参数禁止召唤英雄 NORECALLHERO //禁止召唤英雄 \r\n\r\n示例：\r\n\r\n[K004 魔王岭] NORECALLHERO \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("禁止召唤英雄", "NORECALLHERO");

                    cd = new CompletionData("禁止地图内吃药", "参数：\r\n　　　NODRUG //禁止地图内吃药 \r\n\r\n说明：\r\n　　　禁止地图内使用药品。\r\n\r\n[3 盟重省] NODRUG \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("禁止地图内吃药", "NODRUG");

                    cd = new CompletionData("禁止地图随机传送", "参数：\r\n　　　NORANDOMMOVE //禁止地图随机传送 \r\n\r\n说明：\r\n　　　禁止随机传送卷在该地图使用。\r\n\r\n[3 盟重省] NORANDOMMOVE \r\n");
                    BlueCheckedData.Add("禁止地图随机传送", "NORANDOMMOVE");

                    CompletionDataList.Add(cd);
                    cd = new CompletionData("禁止夫妻、师徒互换地图", "参数：\r\n　　　 \r\n\r\n[3 盟重土城] EXCHANGEMAP \r\n\r\n[3 盟重土城] EXCHANGEMAP APPR \r\n\r\n[3 盟重土城] EXCHANGEMAP DEAR \r\n\r\n说明：\r\n\r\n扩展NPC命令：EXCHANGEMAP\r\nEXCHANGEMAP //不限制直接对换地图\r\nEXCHANGEMAP APPR //师徒对换地图\r\nEXCHANGEMAP DEAR //夫妻对换地图 \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("禁止夫妻、师徒互换地图", "EXCHANGEMAP");

                    cd = new CompletionData("禁止夫妻召唤", "参数：\r\n　　　NODEARRECALL //禁止夫妻召唤 \r\n\r\n说明：\r\n　　　禁止夫妻召唤命令在该地图使用。\r\n\r\n[3 盟重省] NODEARRECALL \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("禁止夫妻召唤", "NODEARRECALL");

                    cd = new CompletionData("禁止定座标移动", "参数：\r\n　　　NOPOSITIONMOVE //禁止定座标移动 \r\n\r\n说明：\r\n　　　禁止在地图内指定移动坐标,传送戒指将不能准确的移动到指定的坐标上。\r\n\r\n[3 盟重省] NOPOSITIONMOVE \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("禁止定座标移动", "NOPOSITIONMOVE");

                    cd = new CompletionData("禁止师徒召唤", "参数：\r\n　　　NOMasterRECALL //禁止师徒召唤 \r\n\r\n说明：\r\n　　　禁止师徒召唤命令在该地图使用。\r\n\r\n[3 盟重省] NOMasterRECALL \r\n");
                    BlueCheckedData.Add("禁止师徒召唤", "NOMASTERRECALL");

                    CompletionDataList.Add(cd);
                    cd = new CompletionData("禁止行会召唤", "参数：\r\n　　　NOGUILDRECALL //禁止记忆召唤 \r\n\r\n说明：\r\n　　　禁止行会召唤命令在该地图使用。\r\n\r\n[3 盟重省] NOGUILDRECALL \r\n");
                    BlueCheckedData.Add("禁止行会召唤", "NOGUILDRECALL");
                    CompletionDataList.Add(cd);

                    cd = new CompletionData("禁止记忆召唤", "参数：\r\n　　　NORECALL //禁止记忆召唤 \r\n\r\n说明：\r\n　　　禁止记忆套召唤命令在该地图使用。\r\n\r\n[3 盟重省] NORECALL \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("禁止记忆召唤", "NORECALL");

                    cd = new CompletionData("禁止黄字喊话", "参数：\r\n　　　QUIZ //禁止黄字喊话 \r\n\r\n说明：\r\n　　　禁止在该地图进行喊话发言。\r\n\r\n[3 盟重省] QUIZ \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("禁止黄字喊话", "QUIZ");

                    cd = new CompletionData("进入入本地图播放音乐", "参数：\r\n　　　MUSIC //进入入本地图播放音乐 \r\n\r\n说明：\r\n进入入本地图播放音乐 格式：MUSIC(123) \r\n\r\n123代表客户端的音乐文件名\r\n\r\n[3 盟重省] MUSIC(123) \r\n\r\n当人物进入盟重省地图将自动播放客户端MUSIC\\123.mp3歌曲,需要到客户端下建立MUSIC文件夹。\r\n\r\n注：人物如果是离开该地图,音乐将暂停,进入该地图将继续播放,播放方式为重复循环,每张地图只能存在一首歌曲。\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("进入入本地图播放音乐", "MUSIC(音乐文件名)");

                    cd = new CompletionData("进入地图执行任务脚本", "参数：\r\n　　　CHECKQUEST //进入地图执行任务脚本 \r\n\r\n说明：\r\n进入本地图执行任务脚本 格式：CHECKQUEST(Q001) 说明： Q001 代表脚本名。 \r\n\r\n[3 盟重省] CHECKQUEST(Q001) \r\n\r\n当人物进入地图将触发D:\\MirServer\\Mir200\\Envir\\MapQuest_def\\Q001.txt 脚本 \r\n\r\n注：人物如果是传送进入将不会触发。\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("进入地图执行任务脚本", "CHECKQUEST");

                    cd = new CompletionData("进入地图需要洞口", "参数：\r\n　　　NEEDHOLE //进入地图需要洞口 \r\n\r\n说明：\r\n　　　是否需要洞,配合mapinfo里 xx,xx -> yy,yy使用(MONSTER 任何怪物RACE代码设为95都可.xx,xx为进入洞口坐标,在Mongen.txt里xx坐标刷新一怪物即可) \r\n\r\n[3 盟重省] NEEDHOLE \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("进入地图需要洞口", "NEEDHOLE");

                    cd = new CompletionData("进入本地图后PK死亡掉等级", "参数：\r\n　　　PKLOSTLEVEL //进入本地图后PK死亡掉等级 \r\n\r\n说明：\r\n格式：格式：PKLOSTLEVEL(1) 说明：1代表掉多少等级。 \r\n\r\n[3 盟重省] PKLOSTLEVEL(1) \r\n\r\n当人物进入盟重省地图PK死亡将掉等级1级。\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("进入本地图后PK死亡掉等级", "PKLOSTLEVEL(1)");

                    cd = new CompletionData("进入本地图后PK死亡掉经验", "参数：\r\n　　　PKLOSTEXP //进入本地图后可以PK死亡失去经验 \r\n\r\n说明：\r\n格式：PKLOSTEXP(1000) 说明：1000代表失去多少经验。 \r\n\r\n[3 盟重省] PKLOSTEXP(1000) \r\n\r\n当人物进入盟重省地图PK死亡将失去1000经验。\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("进入本地图后PK死亡掉经验", "PKLOSTEXP(1000)");

                    cd = new CompletionData("进入本地图后可以PK升级", "参数：\r\n　　　PKWINLEVEL //进入本地图后可以PK升级 \r\n\r\n说明：\r\n格式：PKWINLEVEL(1) 说明：1代表升多少级。 \r\n\r\n[3 盟重省] PKWINLEVEL(1) \r\n\r\n当人物进入盟重省地图PK,杀死敌人将获得1级升级奖励。\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("进入本地图后可以PK升级", "PKWINLEVEL(1)");

                    cd = new CompletionData("进入本地图后可以PK得经验", "参数：\r\n　　　PKWINEXP //进入本地图后可以PK得经验 \r\n\r\n说明：\r\n格式：PKWINEXP(1000) 说明：1000代表得多少经验。 \r\n\r\n[3 盟重省] PKWINEXP(1000) \r\n\r\n当人物进入盟重省地图PK,杀死敌人将获得1000经验奖励。\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("进入本地图后可以PK得经验", "PKWINEXP(1000)");

                    cd = new CompletionData("进入本地图后杀怪经验倍数", "参数：\r\n　　　EXPRATE //进入本地图后杀怪经验倍数 \r\n\r\n说明：\r\n格式：EXPRATE(100) 说明：100代表经验倍数,除以100后为实际倍数。 \r\n\r\n[3 盟重省] EXPRATE(200) \r\n\r\n当人物进入盟重省地图将获得打怪双倍经验。\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("进入本地图后杀怪经验倍数", "EXPRATE(200)");

                    cd = new CompletionData("进入本地图后自动减HP", "参数：\r\n　　　DECHP //进入本地图后自动减HP \r\n\r\n说明：\r\n格式：DECHP(1/10) 说明 1/10 1代表减的间隔(秒),10代表一次减多少点。 \r\n\r\n[3 盟重省] DECHP(1/10) \r\n\r\n当人物进入盟重省地图将每1秒自动减少10点HP。\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("进入本地图后自动减HP", "DECHP(1/10)");

                    cd = new CompletionData("进入本地图后自动减游戏点(泡点)", "参数：\r\n　　　DECGAMEPOINT //进入本地图后自动减游戏点(泡点) \r\n\r\n说明：\r\n格式：DECGAMEPOINT(1/10) 说明 1/10     1代表减的间隔(秒),10代表一次减多少点游戏点(泡点)。 \r\n\r\n[3 盟重省] DECGAMEPOINT(1/10) \r\n\r\n当人物进入盟重省地图将每1秒自动减少10点游戏点(泡点)。\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("进入本地图后自动减游戏点(泡点)", "DECGAMEPOINT(1/10)");

                    cd = new CompletionData("进入本地图后自动减游戏币(元宝)", "参数：\r\n　　　DECGAMEGOLD //进入本地图后自动减游戏币(元宝) \r\n\r\n说明：\r\n格式：DECGAMEGOLD(1/10) 说明 1/10 1代表减的间隔(秒),10代表一次减多少点游戏币(元宝)。 \r\n\r\n[3 盟重省] DECGAMEGOLD(1/10) \r\n\r\n当人物进入盟重省地图将每1秒自动减少10点游戏币(元宝)。\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("进入本地图后自动减游戏币(元宝)", "DECGAMEGOLD(1/10)");

                    cd = new CompletionData("进入本地图后自动加HP", "参数：\r\n　　　INCHP //进入本地图后自动加HP \r\n\r\n说明：\r\n格式：INCHP(1/10) 说明 1/10 1代表加的间隔(秒),10代表一次加多少点。 \r\n\r\n[3 盟重省] INCHP(1/10) \r\n\r\n当人物进入盟重省地图将每1秒自动增加10点HP。\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("进入本地图后自动加HP", "INCHP(1/10)");

                    cd = new CompletionData("进入本地图后自动加游戏币(元宝)", "参数：\r\n　　　INCGAMEGOLD //进入本地图后自动加游戏币(元宝) \r\n\r\n说明：\r\n格式：INCGAMEGOLD(1/10) 说明 1/10 1代表加的间隔(秒),10代表一次加多少点游戏币(元宝)。 \r\n\r\n[3 盟重省] INCGAMEGOLD(1/10) \r\n\r\n当人物进入盟重省地图将每1秒自动加10点游戏币(元宝)。\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("进入本地图后自动加游戏币(元宝)", "INCGAMEGOLD(1/10)");

                    cd = new CompletionData("进入本地图后自动加游戏点(泡点)", "参数：\r\n　　　INCGAMEGOLD //进入本地图后自动加游戏点(泡点) \r\n\r\n说明：\r\n进入本地图后自动加游戏点 格式：INCGAMEPOINT(1/10) 说明 1/10 1代表加的间隔(秒),10代表一次加多少点；(用于游戏泡点功能)。 \r\n\r\n[3 盟重省] INCGAMEPOINT(1/10) \r\n\r\n当人物进入盟重省地图将每1秒自动加10点游戏点(泡点)。\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("进入本地图后自动加游戏币(泡点)", "INCGAMEPOINT(1/10)");

                    cd = new CompletionData("进入本地图需要人物指定标志为关闭状态", "参数：\r\n　　　NEEDSET_OFF //进入本地图需要人物指定标志为关闭状态 \r\n\r\n说明：\r\n进入本地图需要人物指定标志为关闭状态 格式：NEEDSET_OFF(001) 说明： 001 代表指定标志。 \r\n\r\n[3 盟重省] NEEDSET_OFF(001) \r\n\r\n当人物进入盟重省地图将检测人物是否打开了指定标志001 即：SET [001] 0 \r\n\r\n注：人物如果是传送进入将不会检测,如果人物已经打开了指定标志为1的话,那么将不能进入地图。\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("进入本地图需要人物指定标志为关闭状态", "NEEDSET_OFF(001)");

                    cd = new CompletionData("进入本地图需要人物指定标志为打开状态", "参数：\r\n　　　NEEDSET_ON //进入本地图需要人物指定标志为打开状态 \r\n\r\n说明：\r\n进入本地图需要人物指定标志为打开状态 格式：NEEDSET_ON(001) 说明： 001 代表指定标志。 \r\n\r\n[3 盟重省] NEEDSET_ON(001) \r\n\r\n当人物进入盟重省地图将检测人物是否打开了指定标志001 即：SET [001] 1 \r\n\r\n注：人物如果是传送进入将不会检测,如果人物没有指定标志为1的话,那么将不能进入地图。\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("进入本地图需要人物指定标志为打开状态", "NEEDSET_ON(001)");

                    cd = new CompletionData("退出游戏后再上游戏时切换地图", "参数：\r\n　　　NORECONNECT //退出游戏后再上游戏时切换地图 \r\n\r\n说明：\r\n　　　进游戏时退出本地图 格式:NORECONNECT(0159) 说明：0159 代表地图号盟重武器店内。 \r\n\r\n[3 盟重省] NORECONNECT(0159) \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("退出游戏后再上游戏时切换地图", "NORECONNECT(0159)");

                    cd = new CompletionData("雷炎洞地图效果", "功能：\r\n\r\n雷炎洞地图效果 \r\n\r\n示例：\r\n\r\n地图参数：MapInfo、txt里加\r\nTHUNDER(100) //雷电,单体攻击,括号里为攻击力,下同\r\nGREATTHUNDER(100) //大雷电,群体攻击\r\nLAVA(100) //喷岩浆,群体攻击\r\nSPURT(100) //喷气,群体攻击 \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("雷炎洞地图效果", "THUNDER(100) //雷电,单体攻击,括号里为攻击力,下同\r\nGREATTHUNDER(100) //大雷电,群体攻击\r\nLAVA(100) //喷岩浆,群体攻击\r\nSPURT(100) //喷气,群体攻击");

                    cd = new CompletionData("MapEvent.txt-地图坐标触发", "功能：\r\n　　　 地图坐标触发.到达指定坐标按参数条件触发 \r\n\r\n说明：\r\n　　　 配置文件Mir200\\Envir\\MapEvent.txt \r\n\r\n;触发标识 \r\n==================\r\n; 标识:(-1 - 800) -1 代表不检查标识\r\n; 值: (0 - 1) \r\n\r\n;触发条件\r\n==================\r\n; 格式: 代码:物品:组队\r\n; 代码: 0:无效 1:扔物品 2:捡物品 3:挖矿 4:走路(不支持物品条件) 5:跑步(不支持物品条件)\r\n; 物品: (物品名称 - *) * 代表不需要物品\r\n; 组队: (0 - 1) 0为不需要组队,1为必须组队才触发(支持) \r\n\r\n;触发机率\r\n==================\r\n; 数字越大,机率越低\r\n; 范围:(0 - 999999) 0 的机率为100% \r\n\r\n;事件类型\r\n==================\r\n; 格式: 代码:内容\r\n; 代码:(现在只支持脚本事件)\r\n; 0:无效 1:调用脚本(调用QFunction-0.txt中的内容) \r\n\r\n;注意事项\r\n==================\r\n;在相同地图座标,不支持相同触发标识及条件(触发条件中的物品名称除外),如果有相同的设置,只有最后一个设置有效\r\n;地图号 座标X 座标Y 触发标识 触发条件 触发机率 事件类型\r\n3 333 333 -1:1 1:回城卷:0 2 1:@MapEventDropItem\r\n3 333 333 -1:1 2:回城卷:0 2 1:@MapEventPickUpItem\r\n3 338 331 -1:1 3:*:0 2 1:@MapEventMine\r\n3 330 330 -1:1 4:*:0 2 1:@MapEventWalk\r\n3 331 335 -1:1 5:*:0 2 1:@MapEventRun\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("MapEvent.txt-地图坐标触发", "\r\n;==========================================\r\n;Mir200\\Envir\\MapEvent.txt中内容\r\n3 333 333 -1:1 1:回城卷:0 2 1:@MapEventDropItem\r\n3 333 333 -1:1 2:回城卷:0 2 1:@MapEventPickUpItem\r\n;==========================================\r\n;QFunction-0.txt中的内容\r\n[@MapEventDropItem] \r\n#Act\r\nSendMsg 1 <$USERNAME>在(%M,%X,%Y)丢掉物品：回城卷\r\n[@MapEventPickUpItem]\r\n#Act\r\nSendMsg 1 <$USERNAME>在(%M,%X,%Y)拣到物品：回城卷\r\n;========================================== < /FONT>\r\n\r\n使用瞬间移动功能,并且 当前地图<>目标地图\r\n瞬移之前,触发 QMapEvent-0.txt 的 [@ONMAPCHANGING]\r\n瞬移之后,触发 QMapEvent-0.txt 的 [@ONMAPCHANGED]\r\n例：\r\nNPC1： //进MAP001地图,限时30分钟\r\n--------------------------------------------\r\nTimeRecall 30\r\nMAP MAP001\r\n\r\nQMapEvent： //在MAP001瞬移到其他地图,清理TimeRecall\r\n--------------------------------------------\r\n[@ONMAPCHANGING]\r\n#IF\r\nISONMAP MAP001\r\n#ACT\r\nBreakTimeRecall\r\n");

                    cd = new CompletionData("NPC变色功能", "功能：\r\n增加NPC变色功能 \r\n\r\n示例：\r\n\r\n;Merchant 文件的配置格式\r\n;脚本名称 地图名称 X坐标 Y坐标 NPC名称 标志 形象 是否属于沙巴克 是否允许移动 移动间隔 是否变色 变色速度(1=0.5秒)/固定颜色(0-6)\r\n测试 3 333 333 测试 0 12 0 0 0 1 1 --- 是否变色为1时,NPC循环变色每0.5秒\r\n测试 3 333 333 测试 0 12 0 0 0 2 1 --- 是否变色为2时,NPC固定颜色0-6之间 \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("NPC变色功能", ";脚本名称 地图名称 X坐标 Y坐标 NPC名称 标志 形象 是否属于沙巴克 是否允许移动 移动间隔 是否变色 变色速度(1=0.5秒)/固定颜色(0-6)\r\n测试 3 333 333 测试 0 12 0 0 0 1 1 --- 是否变色为1时,NPC循环变色每0.5秒\r\n测试 3 333 333 测试 0 12 0 0 0 2 1 --- 是否变色为2时,NPC固定颜色0-6之间");

                    cd = new CompletionData("NPC命令打开卧龙笔记", "功能：\r\n\r\nOPENBOOK 参数1 参数2 参数3 参数4 //打开书页用 \r\n\r\n\r\n格式：\r\n\r\n参数1：指定到客户端“.\\Data\\books\\参数1\\”目录\r\n参数2：结合参数1,指定到客户端“\\Data\\books\\参数1\\”目录下的“参数2.uib”文件\r\n参数3：跳转到NPC的节名\r\nOPENBOOK 1 0 @GotoHill //打开客户端. \\Data\\books\\1\\ 目录下文件,翻页到最后,点“前往卧龙山庄”触发[@GotoHill]\r\nOPENBOOK 5 1 //打开客户端 .\\Data\\books\\5\\1.uib 文件\r\n(注：*.uib是16BitBmp文件,可以自己制作书页) \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("NPC命令打开卧龙笔记", "[@main]\r\n#ACT\r\nOPENBOOK 1 0 @gohill\r\n\r\n;注意此写法,[@Label]+空格+TRUE,表示@gohill允许无条件跳转,如果添加TRUE,\r\n;则只有在#SAY中存在<任意/@Label>才允许无条件跳转,或者任何时候使用goto命令\r\n;此实例中,如果[@gohill]后不加TRUE,OPENBOOK 1 0 @gohill 执行并翻页到最后,将无法跳转到[@gohill]\r\n[@gohill] TRUE\r\n#ACT\r\nMAPMOVE MAP X Y");

                    cd = new CompletionData("NPC命令打开宝箱", "功能：\r\n增加NPC命令打开宝箱 \r\n\r\n增加开宝箱NPC命令：OPENBOX 宝箱名字 \r\n\r\n示例：\r\n\r\n#IF\r\nCHECKBAGSIZE 6\r\n#ELSEACT\r\nMESSAGEBOX 你包袱空格不够！你不能开启节日宝箱！\r\nBREAK\r\n#IF\r\nCHECKITEM 节日钥匙 1\r\n#ACT\r\nTAKE 节日钥匙 1\r\nTAKE 节日宝箱 1\r\nOPENBOX 节日宝箱\r\n#ELSEACT\r\nMESSAGEBOX 你没有节日钥匙,不能开启节日宝箱！\r\nCLOSE \r\n\r\n宝箱配置： \r\n\r\nBoxItem.txt \r\n\r\n;物品名称 数量 获取几率(1-255,数字越大几率越小),引擎将自动计算,按照几率安排中间一格的物品：获取几率设置 222 将无法获取,注：222 只能设置一个道具存在宝箱设置文件里！\r\n金刚石 10 5\r\n金针碎片 5 10\r\n经验 200000 10\r\n经验 350000 10\r\n声望 10 10\r\n书页 10 20\r\n金刚石 30 40\r\n经验 500000 50\r\n经验 800000 80\r\n声望 20 100\r\n精元丹 1 101\r\n经验 1000000 102\r\n\r\n......");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("NPC命令打开宝箱", "#IF\r\nCHECKBAGSIZE 6\r\n#ELSEACT\r\nMESSAGEBOX 你包袱空格不够！你不能开启节日宝箱！\r\nBREAK\r\n#IF\r\nCHECKITEM 节日钥匙 1\r\n#ACT\r\nTAKE 节日钥匙 1\r\nTAKE 节日宝箱 1\r\nOPENBOX 节日宝箱\r\n#ELSEACT\r\nMESSAGEBOX 你没有节日钥匙,不能开启节日宝箱！\r\nCLOSE ");

                    cd = new CompletionData("NPC命令打开网站", "功能：\r\n增加NPC命令：WebBrowser http://www.baidu.com //直接使用登陆器浏览网页,不必切换到IE进行浏览 \r\n\r\n示例：\r\n\r\n[@main]\r\n\r\n我们的官方网站<点击直接进入/@baidu>\\\r\n\r\n[@baidu]\r\n#IF\r\n#ACT\r\nWebBrowser http://www.baidu.com \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("NPC命令打开网站", "WebBrowser");
                    cd = new CompletionData("NPC命令移动宝宝坐标", "功能：\r\n增加NPC命令：ChangeRangeMonPos 怪物名称 原地图 原X 原Y 原范围 新地图 新X 新Y \r\n\r\n示例：\r\n\r\n#IF\r\nCHECKRANGEMONCOUNTEX K004 51 43 魔王弓箭手 > 0\r\n#ACT\r\nMessagebox 6号位置,已经有弓箭手。\r\n#ELSEACT\r\nCHANGERANGEMONPOS 魔王弓箭手 K004 51 43 0 K004 48 47\r\nclose \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("NPC命令移动宝宝坐标", "CHANGERANGEMONPOS");
                    cd = new CompletionData("NPC命令自动寻路", "功能：\r\n增加NPC命令：MOVETOCELL X Y NPC名字 //自动移动到坐标点,“NPC名字”可省略,若NPC存在,到达目标会自动点击NPC\r\n\r\n示例：比如在瞬移地图后,寻路到NPC旁边自动打开对话框\r\n\r\nQMapEvent-0.txt \r\n\r\n;此为地图事件功能脚本,用于实现各种与脚本有关的功能\r\n[@ONMAPCHANGED]\r\n#IF\r\nCHECKMISSION 3 = 14\r\n#ACT\r\nDELAYGOTO 1000 @cd\r\n#SAY\r\n<$USERNAME>,\\\r\n你将自动寻路到6 23这个坐标上！\\ \r\n\r\n \r\n\r\n[@cd]\r\n#IF\r\nISONMAP 0105\r\n#ACT\r\nMOVETOCELL 7 19 比奇项链店老板 \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("NPC命令自动寻路", "MOVETOCELL");
                    cd = new CompletionData("QFunction-22点准时触发", "功能：\r\n[@WallWarStopFunc] 22点准时触发\r\n\r\n格式：\r\n\r\n[@WallWarStopFunc]\r\n#If\r\nIsCastleMaster \r\n#Act\r\nSendMsg 1 [提示]:新一任沙城主已经诞生.我[%s]将号令天下.! \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("QFunction-22点准时触发", "[@WallWarStopFunc]\r\n#If\r\nIsCastleMaster \r\n#Act\r\nSendMsg 1 [提示]:新一任沙城主已经诞生.我[%s]将号令天下.!");
                    cd = new CompletionData("QFunction-人物内功升级触发", "功能：\r\n[@IPLevelUp] 人物内功升级触发 \r\n\r\n格式：\r\n\r\n[@IPLevelUp]\r\n#IF\r\nCHECKIPLEVEL ? 200\r\n#ACT\r\nCHANGEIPLEVEL = 200\r\nSENDMSG 5 系统提示：本服目前封顶内功级别是200级,请不要再进行冲级,否则后果自负。 \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("QFunction-人物内功升级触发", "[@IPLevelUp]\r\n#IF\r\nCHECKIPLEVEL ? 200\r\n#ACT\r\nCHANGEIPLEVEL = 200\r\nSENDMSG 5 系统提示：本服目前封顶内功级别是200级,请不要再进行冲级,否则后果自负。");
                    cd = new CompletionData("QFunction-人物升级触发", "功能：\r\n[@PlayLevelUp] 人物升级触发 \r\n\r\n格式：\r\n\r\n[@PlayLevelUp]\r\n#IF\r\nCHECKLEVELEX ? 70\r\n#ACT\r\nCHANGELEVEL = 70\r\nSENDMSG 5 系统提示：本服目前封顶级别是70级,请不要再进行冲级,否则后果自负。\r\n#IF\r\n#ACT\r\nGIVE 金币 2000\r\nSENDMSG 5 恭喜：你的级别获得提高。 \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("QFunction-人物升级触发", "[@PlayLevelUp]\r\n#IF\r\nCHECKLEVELEX ? 70\r\n#ACT\r\nCHANGELEVEL = 70\r\nSENDMSG 5 系统提示：本服目前封顶级别是70级,请不要再进行冲级,否则后果自负。\r\n#IF\r\n#ACT\r\nGIVE 金币 2000\r\nSENDMSG 5 恭喜：你的级别获得提高。");

                    cd = new CompletionData("QFunction-人物杀人触发", "功能：\r\n[@KillPlay] ;人物杀人触发 \r\n\r\n格式：\r\n\r\n[@KillPlay]\r\n#IF\r\nISONMAP G003\r\nCHECKLEVELEX > 50\r\n#ACT\r\nmapmove 3 330 330\r\nSENDMSG 0 技艺超群的<$USERNAME>在PK场杀死了对手!\r\nbreak \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("QFunction-人物杀人触发", "[@KillPlay]\r\n#IF\r\nISONMAP G003\r\nCHECKLEVELEX > 50\r\n#ACT\r\nmapmove 3 330 330\r\nSENDMSG 0 技艺超群的<$USERNAME>在PK场杀死了对手!\r\nbreak");

                    cd = new CompletionData("QFunction-人物死亡触发", "功能：\r\n[@PlayDie] ;人物死亡触发 \r\n\r\n格式：\r\n\r\n[@PlayDie]\r\n#IF\r\n#ACT\r\nSENDMSG 5 距离复活时间还有%t秒…… 151 0 5 @WantRealive\r\n[@WantRealive]\r\n#IF\r\nCHECKLEVELEX < 8\r\n#ACT\r\nBREAK \r\n\r\n#IF\r\n#ACT\r\nMOV M1 $LEVEL\r\nDIV M1 8 \r\n\r\n#IF\r\nCHECKGAMEGOLD ? $STR(M1)\r\n#ACT\r\nQUERYVALUE 2 2 10 @RealiveNow 是否花费<$STR(M1)>元宝在原地复活？ QF \r\n\r\n[@RealiveNow]\r\n#IF\r\nCHECKGAMEGOLD ? $STR(M1)\r\n#ACT\r\nGAMEGOLD - $STR(M1)\r\nGMEXECUTE 复活 $USERNAME \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("QFunction-人物死亡触发", "[@PlayDie]\r\n#IF\r\n#ACT\r\nSENDMSG 5 距离复活时间还有%t秒…… 151 0 5 @WantRealive\r\n[@WantRealive]\r\n#IF\r\nCHECKLEVELEX < 8\r\n#ACT\r\nBREAK \r\n\r\n#IF\r\n#ACT\r\nMOV M1 $LEVEL\r\nDIV M1 8 \r\n\r\n#IF\r\nCHECKGAMEGOLD ? $STR(M1)\r\n#ACT\r\nQUERYVALUE 2 2 10 @RealiveNow 是否花费<$STR(M1)>元宝在原地复活？ QF \r\n\r\n[@RealiveNow]\r\n#IF\r\nCHECKGAMEGOLD ? $STR(M1)\r\n#ACT\r\nGAMEGOLD - $STR(M1)\r\nGMEXECUTE 复活 $USERNAME\r\n");

                    cd = new CompletionData("QFunction-创建英雄成功触发", "功能：\r\n[@CreateHeroOK] 创建英雄成功触发\r\n\r\n格式：\r\n\r\n[@CreateHeroOK]\r\n#IF\r\nISNEWHUMAN\r\n#ACT\r\nSETMISSION ^ 1 2\r\nSENDMSG 0 恭喜:<$USERNAME>,领取英雄成功。\r\n#SAY\r\n您的英雄已创建成功！\\\r\n<关闭/@EXIT>\\ \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("QFunction-创建英雄成功触发", "[@CreateHeroOK]\r\n#IF\r\nISNEWHUMAN\r\n#ACT\r\nSETMISSION ^ 1 2\r\nSENDMSG 0 恭喜:<$USERNAME>,领取英雄成功。\r\n#SAY\r\n您的英雄已创建成功！\\\r\n<关闭/@EXIT>\\");


                    cd = new CompletionData("QFunction-召唤英雄触发", "功能：\r\n[@HeroLogin] 召唤英雄触发\r\n\r\n格式：\r\n\r\n[@HeroLogin]\r\n#IF\r\nH.ISNEWHUMAN\r\n#ACT\r\nH.CHANGEIPLEVEL = 55 \r\nSENDMSG 5 系统提示：英雄内功等级提升至55级。\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("QFunction-召唤英雄触发", "[@HeroLogin]\r\n#IF\r\nH.ISNEWHUMAN\r\n#ACT\r\nH.CHANGEIPLEVEL = 55 \r\nSENDMSG 5 系统提示：英雄内功等级提升至55级。");


                    cd = new CompletionData("QFunction-地图坐标触发", "功能：\r\n　　　 地图坐标触发.到达指定坐标按参数条件触发 \r\n\r\n说明：\r\n　　　 配置文件Mir200\\Envir\\MapEvent.txt \r\n\r\n;触发标识 \r\n==================\r\n; 标识:(-1 - 800) -1 代表不检查标识\r\n; 值: (0 - 1) \r\n\r\n;触发条件\r\n==================\r\n; 格式: 代码:物品:组队\r\n; 代码: 0:无效 1:扔物品 2:捡物品 3:挖矿 4:走路(不支持物品条件) 5:跑步(不支持物品条件)\r\n; 物品: (物品名称 - *) * 代表不需要物品\r\n; 组队: (0 - 1) 0为不需要组队,1为必须组队才触发(支持) \r\n\r\n;触发机率\r\n==================\r\n; 数字越大,机率越低\r\n; 范围:(0 - 999999) 0 的机率为100% \r\n\r\n;事件类型\r\n==================\r\n; 格式: 代码:内容\r\n; 代码:(现在只支持脚本事件)\r\n; 0:无效 1:调用脚本(调用QFunction-0.txt中的内容) \r\n;==========================================\r\n;Mir200\\Envir\\MapEvent.txt中内容\r\n3 333 333 -1:1 1:回城卷:0 2 1:@MapEventDropItem\r\n3 333 333 -1:1 2:回城卷:0 2 1:@MapEventPickUpItem\r\n;==========================================\r\n;QFunction-0.txt中的内容\r\n[@MapEventDropItem] \r\n#Act\r\nSendMsg 1 <$USERNAME>在(%M,%X,%Y)丢掉物品：回城卷\r\n[@MapEventPickUpItem]\r\n#Act\r\nSendMsg 1 <$USERNAME>在(%M,%X,%Y)拣到物品：回城卷\r\n;==========================================");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("QFunction-地图坐标触发", ";==========================================\r\n;Mir200\\Envir\\MapEvent.txt中内容\r\n3 333 333 -1:1 1:回城卷:0 2 1:@MapEventDropItem\r\n3 333 333 -1:1 2:回城卷:0 2 1:@MapEventPickUpItem\r\n;==========================================\r\n;QFunction-0.txt中的内容\r\n[@MapEventDropItem] \r\n#Act\r\nSendMsg 1 <$USERNAME>在(%M,%X,%Y)丢掉物品：回城卷\r\n[@MapEventPickUpItem]\r\n#Act\r\nSendMsg 1 <$USERNAME>在(%M,%X,%Y)拣到物品：回城卷\r\n;==========================================");


                    cd = new CompletionData("QFunction-大退小退触发", "功能：安全区内有效\r\n[@OnLogout_OffLinePlaying] 大退触发 \r\n\r\n[@OnLogout_SoftClosing] 　 小退触发 \r\n\r\n格式：\r\n\r\n在QManage.txt里写入以下\r\n\r\n[@RESUME]\r\n#ACT\r\nKICK\r\nSETOFFLINEPLAY OFF\r\nMESSAGEBOX 因为你上次下线的时后使用了离线挂机功能!\\管理员为了避免你在游戏中出现数据错误!\\请你小退一下再重新登陆! \r\n\r\n[@Login]\r\n#IF\r\nCHECKLEVELEX > 0\r\n#ACT\r\nSETOFFLINEPLAY ON \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("QFunction-大退小退触发", "在QManage.txt里写入以下\r\n\r\n[@RESUME]\r\n#ACT\r\nKICK\r\nSETOFFLINEPLAY OFF\r\nMESSAGEBOX 因为你上次下线的时后使用了离线挂机功能!\\管理员为了避免你在游戏中出现数据错误!\\请你小退一下再重新登陆! \r\n\r\n[@Login]\r\n#IF\r\nCHECKLEVELEX > 0\r\n#ACT\r\nSETOFFLINEPLAY ON \r\n");

                    cd = new CompletionData("QFunction-挖卧龙怪触发", "功能：\r\n[@GetButchItem怪物名字] 挖卧龙怪触发\r\n\r\n格式：\r\n\r\n[@GetButchItem卧龙庄主]\r\n#IF\r\n#ACT\r\nSENDMSG 1 宝藏守护者:%s在卧龙庄主身上挖到了宝贝！如果你想也得到宝贝请前去卧龙山庄杀守护者然后挖取其身上携带的装备。\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("QFunction-挖卧龙怪触发", "[@GetButchItem卧龙庄主]\r\n#IF\r\n#ACT\r\nSENDMSG 1 宝藏守护者:%s在卧龙庄主身上挖到了宝贝！如果你想也得到宝贝请前去卧龙山庄杀守护者然后挖取其身上携带的装备。");

                    cd = new CompletionData("QFunction-称号触发", "功能：\r\n玩家改变使用称号或刚上线有使用到称号,触发：QFunction-0 的\r\n人物：[@TitleChanged_XX]\r\n英雄：[@HeroTitleChanged_XX]\r\nXX代表物品DB中的Shape \r\n\r\n格式：\r\n\r\n在QFunction-0.txt里写入以下 里面的判断自己写。 \r\n\r\n[@TitleChanged_XX]\r\n#IF\r\nISCASTLEMASTER\r\n#ACT \r\nSENDMSG 0 伟大的沙巴克城主<$USERNAME>进入了游戏!\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("QFunction-称号触发", "在QFunction-0.txt里写入以下 里面的判断自己写。 \r\n\r\n[@TitleChanged_XX]\r\n#IF\r\nISCASTLEMASTER\r\n#ACT \r\nSENDMSG 0 伟大的沙巴克城主<$USERNAME>进入了游戏!\r\n");

                    cd = new CompletionData("QFunction-穿戴装备触发", "功能：\r\n[@TakeOnX] 穿上装备、取下装备触发 \r\n\r\n格式：\r\n\r\n功能：\r\n　　　 穿上装备、取下装备时触发脚本 \r\n\r\n介绍：\r\n　　　 脚本写在QFunction-0.txt中.对应标签为[@TakeOnX]、[@TakeOffX].其中X(0-12)是装备位置.在[@TakeOffX]中.可以检测该位置当前装备(也就是要取下的装备).在[@TakeOnX]中.可以检测到新戴上的装备\r\n\r\n;==========================================\r\n[@TakeOn3]\r\n#If\r\ncheckitemw 金项链 1 \r\n#Act \r\nSendMsg 5 [提示]:你妈的真有钱.穿金带银.\r\n;========================================= =\r\n\r\n;==========================================\r\n[@TakeOff3]\r\n#If\r\ncheckitemw 金项链 1 \r\n#Act \r\nSendMsg 5 [提示]:我靠.不是吧.项链借来的.?\r\n;========================================= =\r\n\r\n物品位置:\r\n0 盔甲\r\n1 武器\r\n2 照明物(蜡烛,此物品属性升级无效)\r\n3 项链\r\n4 头盔\r\n5 右手镯\r\n6 左手镯\r\n7 右戒指\r\n8 右戒指\r\n9 无(放护身符位置)\r\n10 腰带\r\n11 鞋子\r\n12 宝石 \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("QFunction-穿戴装备触发", ";==========================================\r\n[@TakeOn3]\r\n#If\r\ncheckitemw 金项链 1 \r\n#Act \r\nSendMsg 5 [提示]:你妈的真有钱.穿金带银.\r\n;========================================= =\r\n\r\n;==========================================\r\n[@TakeOff3]\r\n#If\r\ncheckitemw 金项链 1 \r\n#Act \r\nSendMsg 5 [提示]:我靠.不是吧.项链借来的.?\r\n;========================================= =\r\n");

                    cd = new CompletionData("QFunction-精练装备失败触发", "功能：\r\n[@BuildItemFail] 精练装备失败触发\r\n\r\n格式：\r\n\r\n[@BuildItemFail]\r\n#IF \r\n#ACT \r\nSENDMSG 0 可惜:<$USERNAME>,精练装备失败了。\r\n#SAY\r\n很可惜你的装备在精练过程中损坏了！\\\r\n<关闭/@EXIT>\\ \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("QFunction-精练装备失败触发", "[@BuildItemFail]\r\n#IF \r\n#ACT \r\nSENDMSG 0 可惜:<$USERNAME>,精练装备失败了。\r\n#SAY\r\n很可惜你的装备在精练过程中损坏了！\\\r\n<关闭/@EXIT>\\");

                    cd = new CompletionData("QFunction-精练装备成功触发", "功能：\r\n[@BuildItemOK] 精练装备成功触发\r\n\r\n格式：\r\n\r\n[@BuildItemOK]\r\n#IF \r\n#ACT \r\nSENDMSG 0 恭喜:<$USERNAME>,精练装备成功。\r\n#SAY\r\n您精练的装备成功了！\\\r\n<关闭/@EXIT>\\ \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("QFunction-精练装备成功触发", "[@BuildItemOK]\r\n#IF \r\n#ACT \r\nSENDMSG 0 恭喜:<$USERNAME>,精练装备成功。\r\n#SAY\r\n您精练的装备成功了！\\\r\n<关闭/@EXIT>\\");

                    cd = new CompletionData("QFunction-英雄内功升级触发", "功能：\r\n[@HeroIPLevelUp]人物内功升级触发\r\n\r\n格式：\r\n\r\n[@HeroIPLevelUp]\r\n#IF\r\nH.CHECKIPLEVEL ? 200\r\n#ACT\r\nH.CHANGEIPLEVEL = 200\r\nSENDMSG 5 系统提示：本服目前封顶内功级别是200级,请不要再进行冲级,否则后果自负。 \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("QFunction-英雄内功升级触发", "[@HeroIPLevelUp]\r\n#IF\r\nH.CHECKIPLEVEL ? 200\r\n#ACT\r\nH.CHANGEIPLEVEL = 200\r\nSENDMSG 5 系统提示：本服目前封顶内功级别是200级,请不要再进行冲级,否则后果自负。");

                    cd = new CompletionData("QFunction-英雄升级触发", "功能：\r\n[@HeroLevelUp] 英雄升级触发 \r\n\r\n格式：\r\n\r\n[@HeroLevelUp]\r\n#IF\r\nH.CHECKLEVELEX ? 70\r\n#ACT\r\nH.CHANGELEVEL = 70\r\nSENDMSG 5 系统提示：本服目前封顶级别是70级,请不要再进行冲级,否则后果自负。 \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("QFunction-英雄升级触发", "[@HeroLevelUp]\r\n#IF\r\nH.CHECKLEVELEX ? 70\r\n#ACT\r\nH.CHANGELEVEL = 70\r\nSENDMSG 5 系统提示：本服目前封顶级别是70级,请不要再进行冲级,否则后果自负。");

                    cd = new CompletionData("QFunction-释放天地结晶触发", "功能：\r\n[@ReleaseCollectExp] ;释放天地结晶里的经验触发 \r\n\r\n格式：\r\n\r\n[@ReleaseCollectExp]\r\n#ACT\r\nQUERYVALUE 2 2 10 @ExpNow 你的天地结晶已经储存了<$COLLECTEXP>经验和<$COLLECTIPEXP>内功经验,你可以支付<$GCEPAYMENT>元宝\\提取其中的<$GAINCOLLECTEXP>经验和<$GAINCOLLECTIPEXP>内功经验 QF \r\n\r\n[@ExpNow]\r\n#IF\r\nCHECKGAMEGOLD ? $GCEPAYMENT\r\n#ACT\r\nGAMEGOLD - $GCEPAYMENT\r\nCHANGEEXP + $GAINCOLLECTEXP\r\nCHANGEIPEXP + $GAINCOLLECTIPEXP\r\nRESETCOLLECTEXPSTATE //恢复天地结晶到初始状态 \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("QFunction-释放天地结晶触发", "[@ReleaseCollectExp]\r\n#ACT\r\nQUERYVALUE 2 2 10 @ExpNow 你的天地结晶已经储存了<$COLLECTEXP>经验和<$COLLECTIPEXP>内功经验,你可以支付<$GCEPAYMENT>元宝\\提取其中的<$GAINCOLLECTEXP>经验和<$GAINCOLLECTIPEXP>内功经验 QF \r\n\r\n[@ExpNow]\r\n#IF\r\nCHECKGAMEGOLD ? $GCEPAYMENT\r\n#ACT\r\nGAMEGOLD - $GCEPAYMENT\r\nCHANGEEXP + $GAINCOLLECTEXP\r\nCHANGEIPEXP + $GAINCOLLECTIPEXP\r\nRESETCOLLECTEXPSTATE //恢复天地结晶到初始状态\r\n");

                    cd = new CompletionData("QFunction-鉴定装备触发", "功能：\r\n鉴定获得特殊技能触发 \r\n\r\n格式：\r\n\r\n关于解读出的特殊技能：\r\n服饰和武器都可以解读出来,但是必须佩带武器,才能拥有指定的技能,\r\n服饰带的技能Lv+1属性,是在武器带了该技能的情况下,用来增加技能的等级。\r\n解读出特殊技能时,触发QFunction的[@SecretProperty_Skill]\r\n同时赋予S98=技能名,S99=装备名,目前支持技能：五岳独尊,召唤巨魔,神龙附体,倚天劈地\r\n例：\r\n[@SecretProperty_Skill]\r\n#ACT\r\nSendScrollMsg [神秘解读]：<$USERNAME>的<$STR(S99)>解读出<$STR(S98)>特技 253 16 \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("QFunction-鉴定装备触发", "[@SecretProperty_Skill]\r\n#ACT\r\nSendScrollMsg [神秘解读]：<$USERNAME>的<$STR(S99)>解读出<$STR(S98)>特技 253 16");

                    cd = new CompletionData("乘法、除法、百分比计算功能", "功能：\r\n乘法、除法、百分比计算功能 \r\n\r\n计算过程中数值不能超过21亿,否则会变成负数,请大家看清楚,是计算过程,不是结果。\r\n\r\n示例：\r\n\r\nMUL //乘法\r\nDIV //除法\r\nPERCENT //百分比 \r\n\r\n示例：\r\n\r\n#IF\r\n#ACT\r\nMUL M1 $STR(M2)\r\n#SAY\r\nM1的边量= M1*M2的值 \r\n\r\n#IF\r\n#ACT\r\nDIV M1 $STR(M2) \r\n#SAY\r\nM1的边量= M1除M2的值 \r\n\r\n#IF\r\n#ACT\r\nPERCENT M1 $STR(M2) \r\n#SAY\r\nM1的边量= M1除M2*100%的值 \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("乘法、除法、百分比计算功能", "#IF\r\n#ACT\r\nMUL M1 $STR(M2)\r\n#SAY\r\nM1的边量= M1*M2的值 \r\n\r\n#IF\r\n#ACT\r\nDIV M1 $STR(M2) \r\n#SAY\r\nM1的边量= M1除M2的值 \r\n\r\n#IF\r\n#ACT\r\nPERCENT M1 $STR(M2) \r\n#SAY\r\nM1的边量= M1除M2*100%的值 \r\n");

                    cd = new CompletionData("全服滚动公告(新)", "功能：\r\n    利用NPC发送信息,全服滚动公告(新)。 \r\n\r\n格式：\r\n   SendScrollMsg 文字 前景色(1~255) 背景色(1~255) \r\n\r\n命令格式：\r\n\r\n[@SendScrollMsg]\r\n#ACT\r\nSendScrollMsg 5 测试信息%t秒后结束... 151 0 \r\n\r\n[@吆喝]\r\n#say\r\n先放上你要吆喝的物品,放置完成后点击<开始吆喝/@开始吆喝>开始叫卖。\\ \\<返回/@back>   <关闭/@exit>\r\n#act\r\nQueryItemDlg 放上吆喝的物品 @ShowMyItem 0 \r\n\r\n[@ShowMyItem]\r\n#act\r\nGetDlgItemId MShowItemID\r\ninc SShowItems <$ITEM(MShowItemID)>\r\ndelaygoto 100 @吆喝 \r\n\r\n[@开始吆喝]\r\n#if\r\ncompval <$STR(SShowItems)> ! \"\"\r\n#act\r\nSendScrollMsg 【<$USERNAME>】出售物品：<$STR(SShowItems)>(点击物品可暂停漂移) 151 16\r\nmov SShowItems \"\"\r\nclose \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("全服滚动公告(新)", "[@SendScrollMsg]\r\n#ACT\r\nSendScrollMsg 5 测试信息%t秒后结束... 151 0 \r\n\r\n[@吆喝]\r\n#say\r\n先放上你要吆喝的物品,放置完成后点击<开始吆喝/@开始吆喝>开始叫卖。\\ \\<返回/@back>   <关闭/@exit>\r\n#act\r\nQueryItemDlg 放上吆喝的物品 @ShowMyItem 0 \r\n\r\n[@ShowMyItem]\r\n#act\r\nGetDlgItemId MShowItemID\r\ninc SShowItems <$ITEM(MShowItemID)>\r\ndelaygoto 100 @吆喝 \r\n\r\n[@开始吆喝]\r\n#if\r\ncompval <$STR(SShowItems)> ! \"\"\r\n#act\r\nSendScrollMsg 【<$USERNAME>】出售物品：<$STR(SShowItems)>(点击物品可暂停漂移) 151 16\r\nmov SShowItems \"\"\r\nclose \r\n");

                    cd = new CompletionData("创建NPC脚本", "功能：\r\n\r\nMobNpc 地图名 X Y NPC名称 脚本文件名 外形(数字) 属沙城(0,1) 脚本文件不带地图名(0,1) //刷NPC；脚本文件不带地图名=1,不需要带地图名\r\n示例：[@MAIN]测试创建NPC：\\\\<创建NPC/@MobNpc>\\\\\r\n[@MobNpc]#ACT MobNpc 3 346 334 盟重流浪汉 镜像NPC/盟重流浪汉 3 0 1");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("创建NPC脚本", "[@MAIN]测试创建NPC：\\\\<创建NPC/@MobNpc>\\\\\r\n[@MobNpc]#ACT MobNpc 3 346 334 盟重流浪汉 镜像NPC/盟重流浪汉 3 0 1");

                    cd = new CompletionData("创建灵气地图", "功能：\r\n    CREATEMAPNIMBUS MAP(地图) RATE(密集度1~255) TIME(秒) //创建地图灵气 \r\n\r\n格式：\r\n    CREATEMAPNIMBUS MAP(地图) RATE(密集度1~255) TIME(秒)\r\n\r\n;==========================================\r\n;创建地图灵气\r\n[@CREATEMAPNIMBUS]\r\n#IF\r\n#ACT\r\n CREATEMAPNIMBUS 3 2 1200 \r\n#SAY\r\n你已经在盟重地图释放了大量灵气。\r\n;==========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("创建灵气地图", ";创建地图灵气\r\n[@CREATEMAPNIMBUS]\r\n#IF\r\n#ACT\r\n CREATEMAPNIMBUS 3 2 1200 \r\n#SAY\r\n你已经在盟重地图释放了大量灵气。");

                    cd = new CompletionData("创建行会信息", "功能：\r\n　　　建立行会 \r\n\r\n格式：\r\n　　　AddGuild 行会名字\r\n　　　建议一个行会\r\n\r\n;==========================================\r\n[@AddGuild]\r\n#If\r\nCheckLevelEx > 0 \r\n#Act\r\nAddGuild Administrators\r\nSendMsg 5 [提示]:行会Administrators正确建立 \r\n;==========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("创建行会信息", "[@AddGuild]\r\n#If\r\nCheckLevelEx > 0 \r\n#Act\r\nAddGuild Administrators\r\nSendMsg 5 [提示]:行会Administrators正确建立");

                    cd = new CompletionData("创建镜像地图", "功能：\r\nNewCopyMap 源地图名 副本地图标题 有效时间(分钟) 是否刷怪(0/1) 结果跳转字段,返回副本地图名在：<$PARAMSTR(0)> \r\n\r\n示例：\r\n\r\n[@MAIN]\r\n测试镜像地图：\\\\\r\n<进入镜像地图/@进入镜像地图> <进入镜像地图/@进入镜像地图>\\\\ \r\n\r\n[@进入镜像地图]\r\n#act\r\nNewCopyMap d717 猪七副本 30 1 @CreateCopyMapResult\r\n[@CreateCopyMapResult]\r\n#if\r\nCompVal <$PARAMSTR(0)> ! \"\"\r\n#act\r\nmap <$PARAMSTR(0)>\r\nmov A副本地图名称 <$PARAMSTR(0)>\r\nMobNpc <$USERNAME> 346 334 盟重流浪汉 镜像NPC/盟重流浪汉 3 0 1\r\nSendMsg 7 创建副本成功,进入副本：<$PARAMSTR(0)>\r\n#elseact\r\nSendMsg 7 创建副本失败！ \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("创建镜像地图", "[@MAIN]\r\n测试镜像地图：\\\\\r\n<进入镜像地图/@进入镜像地图> <进入镜像地图/@进入镜像地图>\\\\ \r\n\r\n[@进入镜像地图]\r\n#act\r\nNewCopyMap d717 猪七副本 30 1 @CreateCopyMapResult\r\n[@CreateCopyMapResult]\r\n#if\r\nCompVal <$PARAMSTR(0)> ! \"\"\r\n#act\r\nmap <$PARAMSTR(0)>\r\nmov A副本地图名称 <$PARAMSTR(0)>\r\nMobNpc <$USERNAME> 346 334 盟重流浪汉 镜像NPC/盟重流浪汉 3 0 1\r\nSendMsg 7 创建副本成功,进入副本：<$PARAMSTR(0)>\r\n#elseact\r\nSendMsg 7 创建副本失败！\r\n");

                    cd = new CompletionData("删除NPC脚本", "功能：\r\nDELNPC 地图名 X Y //删除NPC,支持变量 \r\n\r\n示例：\r\n\r\n[@MAIN]\r\n测试删除NPC\\\\\r\n<删除NPC/@DELNPC>\\\\ \r\n\r\n[@DELNPC]\r\n#ACT \r\nDELNPC 3 346 334 \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("删除NPC脚本", "[@MAIN]\r\n测试删除NPC\\\\\r\n<删除NPC/@DELNPC>\\\\ \r\n\r\n[@DELNPC]\r\n#ACT \r\nDELNPC 3 346 334 \r\n");

                    cd = new CompletionData("删除人物称号系统", "功能：\r\n新称号系统 \r\n\r\n增加减少称号物品DB时,请注意Shape的连续性(递增),\r\nShape 编号\r\nweight = 1,图片和名字分为上下显示\r\nSource颜色,0~5(0=白色、1=绿色、2=蓝色、3=紫色、4=红色、5=金色) \r\nReserved 显示DB中的名字(有部分图自带了名字,所以可以写上1)\r\nLooks 图片在 ui1.wzl 中的开始位置\r\nDuraMax 可使用时间,单位小时\r\n其他就等同于装备属性。 \r\n\r\n玩家改变使用称号或刚上线有使用到称号,触发：QFunction 的\r\n人物：[@TitleChanged_XX]\r\n英雄：[@HeroTitleChanged_XX]\r\nXX代表物品DB中的Shape \r\n\r\n格式：\r\nDEPRIVETITLE 传奇之星\r\n\r\n;==========================================\r\n[@NAME]\r\n#IF \r\nCHECKTITLE 传奇之星 = 1 //检测称号\r\n#ACT\r\nDEPRIVETITLE 传奇之星 //删除称号、DEPRIVETITLE ALL //删除所有称号\r\n;========================================== \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("删除人物称号系统", "[@NAME]\r\n#IF \r\nCHECKTITLE 传奇之星 = 1 //检测称号\r\n#ACT\r\nDEPRIVETITLE 传奇之星 //删除称号、DEPRIVETITLE ALL //删除所有称号");


                    cd = new CompletionData("删除会员人物及时间", "功能：\r\n\r\n删除会员人物及时间\r\n\r\n格式：\r\n\r\n删除会员人物及时间：DELUSERDATE 会员.txt \r\n\r\n#IF\r\n#ACT\r\nDELUSERDATE 会员.txt\r\n\r\n相关命令：\r\nCHECKUSERDATE   会员.txt   <       30    p0        p1\r\n\r\n检查命令    会员名单  控制符  天数  使用天数  剩余天数(可用<$STR(p1)>在脚本中显示)\r\n\r\n注：如果要检查忽略人物名字就在p1 后面加个参数 1\r\n\r\n加入会员人物及时间： ADDUSERDATE 会员.txt \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("删除会员人物及时间", "删除会员人物及时间：DELUSERDATE 会员.txt");

                    cd = new CompletionData("删除列表中人物IP", "功能：\r\n删除列表中人物IP \r\n\r\n格式：\r\n=========================\r\n[@DELNAMELIST]\r\n#IF\r\n#ACT\r\nDELIPLIST IP.TXT\r\n=========================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("删除列表中人物IP", "[@DELNAMELIST]\r\n#IF\r\n#ACT\r\nDELIPLIST IP.TXT");

                    cd = new CompletionData("删除列表中人物名称", "功能：\r\n删除列表中人物名称 \r\n\r\n格式：\r\n=========================\r\n[@DELNAMELIST]\r\n#IF\r\n#ACT\r\nDELNAMELIST 行会争霸名单.TXT\r\n=========================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("删除列表中人物名称", "[@DELNAMELIST]\r\n#IF\r\n#ACT\r\nDELNAMELIST 行会争霸名单.TXT");

                    cd = new CompletionData("删除列表中人物帐号", "功能：\r\n删除列表中人物帐号 \r\n\r\n格式：\r\n=========================\r\n[@DELACCOUNTLIST]\r\n#IF\r\n#ACT\r\nDELACCOUNTLIST 帐号.TXT\r\n=========================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("删除列表中人物帐号", "[@DELACCOUNTLIST]\r\n#IF\r\n#ACT\r\nDELACCOUNTLIST 帐号.TXT");

                    cd = new CompletionData("删除列表中行会名称", "功能：\r\n删除列表中行会名称 \r\n\r\n格式：\r\n=========================\r\n[@DELGUILDLIST]\r\n#IF\r\n#ACT\r\nDELGUILDLIST 行会争霸.TXT\r\n=========================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("删除列表中行会名称", "[@DELGUILDLIST]\r\n#IF\r\n#ACT\r\nDELGUILDLIST 行会争霸.TXT");

                    cd = new CompletionData("删除人物所有技能", "功能：\r\n    清除人物的所有技能。 \r\n\r\n格式：\r\n    CLEARSKILL\r\n\r\n;==========================================\r\n;清除所有技能\r\n[@clearskill]\r\n#IF\r\n#ACT\r\n  CLEARSKILL\r\n#SAY\r\n你的所有技能已经清除了。\r\n;==========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("删除人物所有技能", "[@clearskill]\r\n#IF\r\n#ACT\r\n  CLEARSKILL\r\n#SAY\r\n你的所有技能已经清除了。");

                    cd = new CompletionData("删除人物指定技能", "功能：\r\n    脚本删除指定技能。 \r\n\r\n格式：\r\n    DELSKILL 技能名称\r\n\r\n;==========================================\r\n;删除技能\r\n[@delskill]\r\n#IF\r\n#ACT\r\n  DELSKILL 雷电术\r\n#SAY\r\n你的雷电术已删除了。\r\n;==========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("删除人物指定技能", "[@delskill]\r\n#IF\r\n#ACT\r\n  DELSKILL 雷电术\r\n#SAY\r\n你的雷电术已删除了");

                    cd = new CompletionData("删除非本职业的所有技能", "功能：\r\n    清除人物非本职业的所有技能。 \r\n\r\n格式：\r\n    DELNOJOBSKILL\r\n\r\n;==========================================\r\n;清除非本职业的所有技能\r\n[@clearskill]\r\n#IF\r\n#ACT\r\nDELNOJOBSKILL\r\n#SAY\r\n你的非法技能已经清除了。\r\n;==========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("删除非本职业的所有技能", "[@clearskill]\r\n#IF\r\n#ACT\r\nDELNOJOBSKILL\r\n#SAY\r\n你的非法技能已经清除了。");

                    cd = new CompletionData("动态增加、删除地图通道", "功能：\r\nADDMAPROUTE 源地图 源坐标X 源坐标Y 目标地图 目标坐标X 目标坐标Y 是否相通(0/1)\r\n\r\nDELMAPROUTE 源地图 源坐标X 源坐标Y \r\n\r\n格式：\r\n============================\r\n[@ADDMAPROUTE]\r\n#IF\r\n#ACT\r\nADDMAPROUTE 3 330 330 3 886 331 1 \r\n\r\n[@DELMAPROUTE]\r\n#IF\r\n#ACT\r\nDELMAPROUTE3 330 330\r\n\r\n===========================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("动态增加、删除地图通道", "[@ADDMAPROUTE]\r\n#IF\r\n#ACT\r\nADDMAPROUTE 3 330 330 3 886 331 1 \r\n\r\n[@DELMAPROUTE]\r\n#IF\r\n#ACT\r\nDELMAPROUTE3 330 330\r\n");

                    cd = new CompletionData("发送文字信息", "功能：\r\n\r\n利用NPC发送信息。 \r\n\r\n格式：\r\n==================================================\r\nSENDMSG 信息类型代码 %s信息内容%d 字体颜色(0-255) 背景颜色(0-255) [注意不带颜色按原来的发送]\r\n\r\n原有功能补充说明：SENDMSG NN 文字 FF BB 30 @本NPC触发 // 30秒后执行[@本NPC触发]，注意次时间内NPC不能消失或释放\r\n\r\n信息类型代码：\r\n==================================================\r\n   1、发送普通红色广播信息。\r\n   2、发送普通红色广播信息，并显示NPC名称。\r\n   3、发送普通红色广播信息，并显示人物名称。\r\n   4、在NPC头顶，显示普通说话信息。\r\n   5、发送红色信息给人物\r\n   6、发送绿色信息给人物\r\n   7、发送蓝色信息给人物\r\n8~10、行会聊天\r\n  11、黄字喊话\r\n  12、队伍聊天\r\n  13、私聊信息\r\n\r\n    %s 代表人物名称\r\n    %d 代表NPC名称\r\n==================================================\r\n命令格式：\r\n==================================================\r\n[@test]\r\n#IF\r\n#ACT\r\nSENDMSG 0 %s信息内容%d\r\nSENDMSG 1 %s信息内容%d\r\nSENDMSG 2 %s信息内容%d\r\nSENDMSG 3 %s信息内容%d\r\nSENDMSG 4 %s信息内容%d\r\nSENDMSG 5 %s信息内容%d\r\nSENDMSG 6 %s信息内容%d\r\nSENDMSG 7 %s信息内容%d\r\n==================================================\r\n功能：\r\n\r\n利用NPC发送信息，发送文字信息（新）。 \r\n==================================================\r\n格式：\r\nSENDMSG 类型 发送信息 字体颜色(0..255) 背景颜色(0..255) 延迟时间(秒) @label\r\n//字体颜色 背景颜色 不为空，则发送自定义颜色文字\r\n//延迟时间(秒) 不为空，则发送信息在屏幕中下方，倒数秒数特征字符为%t，见例：\r\n//@label 不为空，则触发当前NPC的 @label 节 \r\n信息类型代码：\r\n\r\n==================================================\r\n[@smsg]\r\n#ACT\r\nSENDMSG 5 测试信息%t秒后结束... 151 0 10 @dc //%t可以不写\r\n[@dc]\r\n#IF\r\n#ACT\r\nGIVE 回城卷 1\r\n\r\n\r\n==================================================\r\n命令格式：\r\n==================================================\r\n[@smsg]\r\n#ACT\r\nSENDMSG 5 普通文字1<LABEL=@QFLabel&HINT=点击运行脚本功能>[执行脚本]</>普通文字2 255 168 65000 //聊天栏置顶文字+执行脚本\r\nSENDMSG 5 普通文字2<PIC=DscStart0.bmp&LABEL=@QFLabel>普通文字2 255 168 65000 //聊天栏置顶文字+按钮执行脚本，DscStart0.bmp必须存在lui.pkg中\r\nSENDMSG 5 <URL=www.XXXXX.com&HINT=点击打开网站>访问官方网</> 255 168\r\n聊天栏置顶文字需要最新客户端支持。 \r\n\r\n@QFLabel 代表将触发QFunction里对应的脚本名 \r\n\r\n注：不兼容 此类格式的使用 SENDMSG 类型 发送信息 字体颜色(0..255) 背景颜色(0..255) 延迟时间(秒) @label\r\n==================================================\r\n功能：\r\n==================================================\r\n利用NPC发送信息，全服滚动公告（新）。 \r\n\r\n格式：\r\nSendScrollMsg 文字 前景色(1~255) 背景色(1~255) \r\n\r\n命令格式：\r\n[@SendScrollMsg]\r\n#ACT\r\nSendScrollMsg 5 测试信息%t秒后结束... 151 0 \r\n\r\n[@吆喝]\r\n#say\r\n先放上你要吆喝的物品，放置完成后点击<开始吆喝/@开始吆喝>开始叫卖。\\ \\<返回/@back>   <关闭/@exit>\r\n#act\r\nQueryItemDlg 放上吆喝的物品 @ShowMyItem 0 \r\n\r\n[@ShowMyItem]\r\n#act\r\nGetDlgItemId MShowItemID\r\ninc SShowItems <$ITEM(MShowItemID)>\r\ndelaygoto 100 @吆喝 \r\n\r\n[@开始吆喝]\r\n#if\r\ncompval <$STR(SShowItems)> ! \"\"\r\n#act\r\nSendScrollMsg 【<$USERNAME>】出售物品：<$STR(SShowItems)>(点击物品可暂停漂移) 151 16\r\nmov SShowItems \"\"\r\nclose\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("发送文字信息", "SENDMSG");

                    cd = new CompletionData("发送文字信息(新)", "功能：\r\n    利用NPC发送信息,发送文字信息(新)。 \r\n\r\n格式：\r\n    SENDMSG 信息类型代码 %s信息内容%d 字体颜色(0-255) 背景颜色(0-255) [注意不带颜色按原来的发送] \r\n\r\n信息类型代码：\r\n    1、发送普通红色广播信息。\r\n    2、发送普通红色广播信息,并显示NPC名称。\r\n    3、发送普通红色广播信息,并显示人物名称。\r\n    4、在NPC头顶,显示普通说话信息。\r\n    5、发送红色信息给人物\r\n    6、发送绿色信息给人物\r\n    7、发送蓝色信息给人物 \r\n\r\n    %s 代表人物名称\r\n    %d 代表NPC名称\r\n\r\n命令格式：\r\n\r\n[@smsg]\r\n#ACT\r\nSENDMSG 5 普通文字1<LABEL=@QFLabel&HINT=点击运行脚本功能>[执行脚本]</>普通文字2 255 168 65000 //聊天栏置顶文字+执行脚本\r\nSENDMSG 5 普通文字2<PIC=DscStart0.bmp&LABEL=@QFLabel>普通文字2 255 168 65000 //聊天栏置顶文字+按钮执行脚本,DscStart0.bmp必须存在lui.pkg中\r\nSENDMSG 5 <URL=www.XXXXX.com&HINT=点击打开网站>访问官方网</> 255 168\r\n聊天栏置顶文字需要最新客户端支持。 \r\n\r\n@QFLabel 代表将触发QFunction里对应的脚本名 \r\n\r\n注：不兼容 此类格式的使用 SENDMSG 类型 发送信息 字体颜色(0..255) 背景颜色(0..255) 延迟时间(秒) @label\r\n\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("发送文字信息(新)", "SENDMSG");

                    cd = new CompletionData("回收乾坤玉璧", "功能：\r\n   回收乾坤玉璧 \r\n\r\n格式：\r\nTAKENIMBUSITEM 物品名是否解封(0=未解封,1=已解封) 数量 //拿走背包指定数量玉璧(三个参数)\r\n\r\n-----------------------------------------------------------------\r\n[@UnWrap]\r\n#IF\r\nUNWRAPNIMBUSITEM 乾坤玉璧\r\n#SAY\r\n封乾玉壁解封成功。\r\n#ACT\r\nTAKE 金币 5000 //需要5000金币才给解封\r\nTAKENIMBUSITEM 乾坤玉璧 1 10\r\n#ELSESAY\r\n没有可以解封的物品。 \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("回收乾坤玉璧", "[@UnWrap]\r\n#IF\r\nUNWRAPNIMBUSITEM 乾坤玉璧\r\n#SAY\r\n封乾玉壁解封成功。\r\n#ACT\r\nTAKE 金币 5000 //需要5000金币才给解封\r\nTAKENIMBUSITEM 乾坤玉璧 1 10\r\n#ELSESAY\r\n没有可以解封的物品。");

                    cd = new CompletionData("回收聚灵珠", "功能：\r\n   回收聚灵珠 \r\n\r\n格式：\r\nTAKEDURAITEM 物品名是否聚满(0=未满的,1=已满的) 数量 //拿走背包指定条件物品(三个参数)\r\n\r\n-----------------------------------------------------------------\r\n[@main]\r\n#IF\r\nCHECKITEMDURACOUNT 聚灵珠(大) 1 > 10 \r\n#ACT \r\nTAKEDURAITEM 聚灵珠(大) 1 10\r\n#SAY\r\n回收聚灵珠(大)成功。\r\n#ELSESAY\r\n没有可以回收的聚灵珠(大)。 \r\n\r\n-----------------------------------------------------------------\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("回收聚灵珠", "[@main]\r\n#IF\r\nCHECKITEMDURACOUNT 聚灵珠(大) 1 > 10 \r\n#ACT \r\nTAKEDURAITEM 聚灵珠(大) 1 10\r\n#SAY\r\n回收聚灵珠(大)成功。\r\n#ELSESAY\r\n没有可以回收的聚灵珠(大)。");

                    cd = new CompletionData("在线命令弹出对话框命令", "功能：\r\n玩家在线输入指定命令弹出菜单脚本功能。 \r\n\r\n格式：\r\n命令为command.ini内(命令名称随便定义)：MemberFunc=我是会员\r\n输入@我是会员 命令后将运行登录脚本(Qmanage.txt)内[@Member]段内容脚本\r\n具体脚本内容自己写。 \r\n\r\n;==========================================\r\n;功能\r\n[@Member]\r\n#IF \r\n#SAY\r\n你好,会员玩家。 \r\n;==========================================\r\n\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("在线命令弹出对话框命令", "[@Member]\r\n#IF \r\n#SAY\r\n你好,会员玩家。");

                    cd = new CompletionData("增加搜索摆摊物品命令", "SEARCHONSALEITEM 物品名 出售类型(元宝/金币) 返回最高个数(1-10) //返回信息自动以sendmsg形式发送给玩家\r\n\r\n#IF\r\nCHECKGAMEGOLD ~ 1\r\nSEARCHONSALEITEM 开天 元宝 3\r\n#ACT\r\nGAMEGOLD - 1\r\n#ELSEACT\r\nSENDMSG 5 找不到指定的物品\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("增加搜索摆摊物品命令", "SEARCHONSALEITEM");

                    cd = new CompletionData("重新分配人物附加属性点", "功能：\r\n重新分配人物附加属性点 \r\n\r\n格式：\r\n将人物附加的属性点复位到未分配状态。 \r\n\r\n#IF\r\n#ACT\r\nRESTBONUSPOINT\r\n#SAY\r\n你的属性点已经重新分配；\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("重新分配人物附加属性点", "RESTBONUSPOINT");

                    cd = new CompletionData("将人物IP加入列表", "功能：\r\n将人物IP加入列表 \r\n\r\n格式：\r\n=========================\r\n[@ADDIPLIST]\r\n#IF\r\n#ACT\r\nADDIPLIST IP.TXT\r\n=========================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("将人物IP加入列表", "ADDIPLIST IP.TXT");

                    cd = new CompletionData("删除人物登陆IP", "删除：列表中人物IP\r\n;==========================================\r\n[@DELNAMELIST]\r\n#IF\r\n#ACT\r\nDELIPLIST IP.TXT\r\n;==========================================");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("删除人物登陆IP", "DELIPLIST IP.TXT");

                    cd = new CompletionData("将人物加入某行会", "功能：\r\n将人物加入某行会 \r\n\r\n格式：\r\n\r\n自助加入行会的NPC命令：AFFILIATEGUILD 行会名 \r\n\r\n=========================\r\n[@AFFILIATEGUILD]\r\n#IF\r\nCHECKLEVELEX > 0 \r\nISNEWHUMAN\r\n#ACT\r\nGMEXECUTE 加入门派\r\nAFFILIATEGUILD 剑龙阁\r\n=========================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("将人物加入某行会", "ADDIPLIST IP.TXT");

                    cd = new CompletionData("将人物名称加入列表", "功能：\r\n将人物名称加入列表 \r\n\r\n格式：\r\n=========================\r\n[@ADDNAMELIST]\r\n#IF\r\n#ACT\r\nADDNAMELIST 行会争霸名单.TXT\r\n=========================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("将人物名称加入列表", "ADDNAMELIST 行会争霸名单.TXT");

                    cd = new CompletionData("将人物宝宝全部杀死", "功能：\r\n    将人物宝宝全部杀死 \r\n\r\n格式：\r\n    KILLSLAVE  地图 X Y 范围怪物名称数量死亡(0=消失,1=杀死) //怪物名称=* 杀所有属下；K004=SELF时,检测自己当前地图 \r\n\r\n;==========================================\r\n[@KILLSLAVE]\r\n#IF\r\n#ACT\r\nKILLSLAVE K004 39 38 100 * 6 0\r\n#SAY\r\n你的宝宝清理干净了。\r\n;==========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("将人物宝宝全部杀死", "KILLSLAVE K004 39 38 100 * 6 0");

                    cd = new CompletionData("将人物帐号加入列表", "功能：\r\n将人物帐号加入列表 \r\n\r\n格式：\r\n=========================\r\n[@ADDACCOUNTLIST]\r\n#IF\r\n#ACT\r\nADDACCOUNTLIST 帐号.TXT\r\n=========================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("将人物帐号加入列表", "ADDACCOUNTLIST 帐号.TXT");

                    cd = new CompletionData("将人物技能转换", "功能：\r\n将人物技能转换 \r\n\r\n格式：\r\n\r\nCONVERTSKILL S D //S 技能转换为 D 技能,保留原技能等级,修炼点等... \r\n\r\n（前提是必须修炼了S技能才能进行转换,否则无效）\r\n=========================\r\n[@CONVERTSKILL]\r\n#IF\r\n#ACT\r\nCONVERTSKILL 刺杀剑术 火墙\r\n=========================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("将人物技能转换", "CONVERTSKILL 刺杀剑术 火墙");

                    cd = new CompletionData("将人物指定宝宝杀死", "功能：\r\n    将人物指定宝宝杀死\r\n\r\n格式：\r\nKILLSLAVENAME 名称 //指定宝宝< /FONT> \r\n\r\nKILLSLAVENAME * //所有宝宝\r\n\r\n;==========================================\r\n[@KILLSLAVENAME]\r\n#IF\r\n#ACT\r\nKILLSLAVENAME 蜈蚣 \r\n#SAY\r\n你的宝宝清理干净了。\r\n;========================================== \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("将人物指定宝宝杀死", "[@KILLSLAVENAME]\r\n#IF\r\n#ACT\r\nKILLSLAVENAME 蜈蚣 \r\n#SAY\r\n你的宝宝清理干净了。");
                    cd = new CompletionData("将人物杀死", "功能：\r\n    将人物杀死 \r\n\r\n格式：\r\n    KILL\r\n\r\n;==========================================\r\n;杀死人物\r\nKILL 0 人物死亡,不显示凶手信息\r\nKILL 1 人物死亡不掉物品,不显示凶手信息\r\nKILL 2 人物死亡,显示凶手信息为NPC\r\nKILL 3 人物死亡不掉物品,显示凶手信息为NPC\r\n;==========================================\r\n[@KILL]\r\n#IF\r\n#ACT\r\nKILL 2\r\n;========================================== \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("将人物杀死", "KILL");
                    cd = new CompletionData("将人物踢下线", "功能：\r\n    将人物踢下线 \r\n\r\n格式：\r\n   KICK 1 // 让在线玩家执行小退\r\n\r\n;==========================================\r\n[@KICK]\r\n#IF\r\n#ACT\r\nSENDMSG 5 %s,非法登录游戏\r\nKICK 1\r\n;========================================== \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("将人物踢下线", "KICK");
                    cd = new CompletionData("将变量写入文本命令", "功能：\r\n\r\n将变量写入文本命令：\r\n\r\nADDLINELIST 文件名(默认Envir目录下) 字符 //写入文本行\r\nDELLINELIST 文件名(默认Envir目录下) 字符 //删除文本行 \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("将变量写入文本命令", "#IF\r\n#ACT\r\nCLEARNAMELIST ..\\QuestDiary\\www.BLuem2.vip\\冲级数据\\冲级第一名.txt\r\nCLEARNAMELIST ..\\QuestDiary\\www.BLuem2.vip\\冲级数据\\冲级第二名.txt\r\nCLEARNAMELIST ..\\QuestDiary\\www.BLuem2.vip\\冲级数据\\冲级第三名.txt\r\nCLEARNAMELIST ..\\QuestDiary\\www.BLuem2.vip\\冲级数据\\冲级第四名.txt\r\nCLEARNAMELIST ..\\QuestDiary\\www.BLuem2.vip\\冲级数据\\冲级第五名.txt\r\nCLEARNAMELIST ..\\QuestDiary\\www.BLuem2.vip\\冲级数据\\冲级第六名.txt\r\nCLEARNAMELIST ..\\QuestDiary\\www.BLuem2.vip\\冲级数据\\冲级第七名.txt\r\nCLEARNAMELIST ..\\QuestDiary\\www.BLuem2.vip\\冲级数据\\冲级第八名.txt\r\nCLEARNAMELIST ..\\QuestDiary\\www.BLuem2.vip\\冲级数据\\冲级第九名.txt\r\nCLEARNAMELIST ..\\QuestDiary\\www.BLuem2.vip\\冲级数据\\冲级第十名.txt\r\nADDLINELIST ..\\QuestDiary\\www.BLuem2.vip\\冲级数据\\冲级第一名.txt <$STR(A14)>\r\nADDLINELIST ..\\QuestDiary\\www.BLuem2.vip\\冲级数据\\冲级第二名.txt <$STR(A16)>\r\nADDLINELIST ..\\QuestDiary\\www.BLuem2.vip\\冲级数据\\冲级第三名.txt <$STR(A18)>\r\nADDLINELIST ..\\QuestDiary\\www.BLuem2.vip\\冲级数据\\冲级第四名.txt <$STR(A20)>\r\nADDLINELIST ..\\QuestDiary\\www.BLuem2.vip\\冲级数据\\冲级第五名.txt <$STR(A22)>\r\nADDLINELIST ..\\QuestDiary\\www.BLuem2.vip\\冲级数据\\冲级第六名.txt <$STR(A24)>\r\nADDLINELIST ..\\QuestDiary\\www.BLuem2.vip\\冲级数据\\冲级第七名.txt <$STR(A26)>\r\nADDLINELIST ..\\QuestDiary\\www.BLuem2.vip\\冲级数据\\冲级第八名.txt <$STR(A28)>\r\nADDLINELIST ..\\QuestDiary\\www.BLuem2.vip\\冲级数据\\冲级第九名.txt <$STR(A30)>\r\nADDLINELIST ..\\QuestDiary\\www.BLuem2.vip\\冲级数据\\冲级第十名.txt <$STR(A32)>");

                    cd = new CompletionData("将指定物品刷新在指定范围内", "功能：\r\n将指定物品刷新在指定范围内 \r\n\r\n格式：\r\n\r\nDROPITEMMAP 地图号 坐标 坐标 范围 物品名 数量 （地图号如果使用SELF,将代表人物当前地图） \r\n\r\n\r\n=========================\r\n[@DROPITEMMAP]\r\n#IF\r\n#ACT\r\nDROPITEMMAP 3 330 330 5 金条 1\r\n=========================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("将指定物品刷新在指定范围内", "DROPITEMMAP");

                    cd = new CompletionData("将行会名称加入列表", "功能：\r\n将人物行会名加入列表 \r\n\r\n格式：\r\n=========================\r\n[@ADDGUILDLIST]\r\n#IF\r\n#ACT\r\nADDGUILDLIST 行会争霸.TXT\r\n=========================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("将行会名称加入列表", "ADDNAMELIST 行会争霸名单.TXT");

                    cd = new CompletionData("开始镖车任务", "功能：\r\nSTARTESCORT 镖车名字 //开始任务\r\n\r\n格式：\r\n\r\n[@STARTESCORT]\r\n#IF\r\nRANDOM 10\r\n#ACT \r\nSTARTESCORT 无敌镖车 \r\nSENDMSG 0 玩家%s,接到了无敌镖车.谁也别想抢他的车哦！\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("开始镖车任务", "STARTESCORT");

                    cd = new CompletionData("快速杀死怪物", "功能：\r\n    OpenItemBox ；//快速杀死怪物。 \r\n\r\n使用方法：\r\nOpenItemBox 怪物名 ；//怪物死亡后会按爆率文件来爆出物品。 \r\n\r\n脚本样例：\r\n========================================= \r\n\r\n[@OpenItemBox]\r\n#IF\r\n#ACT\r\nOpenItemBox 黄泉教主 \r\n\r\n========================================= \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("快速杀死怪物", "OPENITEMBOX");

                    cd = new CompletionData("提交镖车任务", "功能：\r\nFINISHESCORT //提交任务\r\n\r\n格式：\r\n\r\n[@FINISHESCORT] \r\n#ACT\r\nFINISHESCORT \r\n\r\n[@FinishEscort_镖车1]\r\n#SAY\r\n你完成押运任务！\\ \\\r\n<关闭/@exit>\r\n#ACT\r\n//奖励 \r\n\r\n[@FinishEscort_Fail_1]\r\n你没有镖车啊,提交什么任务！？\\\r\n<关闭/@exit> \r\n\r\n[@FinishEscort_Fail_2]\r\n镖车距离我太远了,叫我如何核查货物呢？\\\r\n<关闭/@exit>\r\n\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("提交镖车任务", "FINISHESCORT");

                    cd = new CompletionData("播放WAV文件(扩展)", "格式：\r\n\r\n#IF\r\n#ACT\r\nPlaySound 文件名 循环(0不循环1循环) 【参数】\r\nPlaySound Stop //停止当前所有播放的声音\r\n\r\n参数包括：\r\nAll //所有在线人物播放\r\nGuild //执行命令者的同行会人物,如无行会则不播放\r\n地图名字 //使用地图文件名,如：3,则盟重的所有人都播放\r\n参数为空 //自己播放 \r\n\r\n举列：\r\n\r\n#IF\r\n#ACT\r\nPlaySound .\\wav\\log-in-long2.wav 0 all 【所有在线的玩家播放客户端下的该音乐log-in-long2.wav】\r\n\r\nPlaySound .\\wav\\log-in-long2.wav 0 Guild 【执行该命令的行会玩家,同行会的全部播放,如无行会则不播放】\r\n\r\nPlaySound .\\wav\\log-in-long2.wav 0 3 【3,则盟重的所有人都播放】\r\n\r\nPlaySound .\\wav\\log-in-long2.wav 0 【参数为空 //自己播放】\r\n\r\nPlaySound .\\Sound\\123.wav 0 //登陆器会在传奇目录下建立\"Sound\"目录,可以自己更新一些自定义的声音 需要自己用登录器做自动更新\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("播放WAV文件(扩展)", "PLAYSOUND");

                    cd = new CompletionData("放弃镖车任务", "功能：\r\nGIVEUPESCORT //放弃镖车任务\r\n\r\n格式：\r\n\r\n[@FINISHESCORT] \r\n#IF\r\n#ACT \r\nCHANGEEXP + 100000000\r\nCHANGEEXP + 100000000\r\nCHANGEEXP + 100000000\r\nCHANGEEXP + 100000000\r\nCHANGEEXP + 100000000\r\nCHANGEEXP + 100000000\r\nCHANGEEXP + 100000000\r\nCHANGEEXP + 100000000\r\nCHANGEEXP + 100000000\r\nCHANGEEXP + 100000000\r\nGIVEUPESCORT\r\nSENDMSG 0 〖喜讯〗玩家%s成功将【无敌镖车】拉到.完成任务获得【10亿经验奖励】 \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("放弃镖车任务", "GIVEUPESCORT");

                    cd = new CompletionData("脚本召唤宝宝", "功能：\r\n新的召唤属下命令。 \r\n\r\n格式：\r\nRECALLMOBEX 怪物名 X Y 等级 数量 叛变时间(分钟) 是否自动变色(0/1) 身体颜色(默认-1) 是否去掉主人名字(留空显示)\r\n注：其中 数量 表示最多能召唤多少属下,执行 RECALLMOBEX 一次,召唤一属下\r\n\r\n;==========================================\r\n;召唤宝宝\r\n[@RECALLMOB]\r\n#IF\r\n#ACT \r\nRECALLMOBEX 弓箭手 51 44 1 6 30 0 -1\r\nMessagebox 你在51.44坐标召唤了一个1级弓箭手,你还可以召唤6只,30分钟后宝宝叛变\r\n#SAY\r\n你已经召唤了1名弓箭手。\r\n;==========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("脚本召唤宝宝", "RECALLMOBEX");

                    cd = new CompletionData("是否自动减少元宝", "功能：\r\n设置是否自动减少元宝,元宝等于0后将返回安全区！ \r\n\r\n格式：\r\nAUTOSUBGAMEGOLD 控制符(START,STOP) \r\n\r\n;==========================================\r\n;设置自动减少元宝\r\n[@AUTOSUBGAMEGOLD]\r\n#IF\r\n#ACT\r\nPARAM1 1\r\nPARAM2 10\r\nAUTOSUBGAMEGOLD START\r\n#SAY\r\n现进入自动减少元宝模式,每10秒减少一个 \r\n\r\n;==========================================\r\n\r\n;==========================================\r\n;停止自动减少元宝\r\n[@AUTOSUBGAMEGOLD]\r\n#IF\r\n#ACT\r\nAUTOSUBGAMEGOLD STOP\r\n#SAY\r\n现退出自动减少元宝模式 \r\n\r\n;==========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("是否自动减少元宝", "AUTOSUBGAMEGOLD");

                    cd = new CompletionData("是否自动增加元宝", "功能：\r\n设置是否自动增加元宝。 \r\n\r\n格式：\r\nAUTOADDGAMEGOLD 控制符(START,STOP) \r\n\r\n;==========================================\r\n;设置自动增加元宝\r\n[@AUTOADDGAMEGOLD]\r\n#IF\r\n#ACT\r\nPARAM1 1\r\nPARAM2 10\r\nAUTOADDGAMEGOLD START\r\n#SAY\r\n现进入自动增加元宝模式,每10秒增加一个 \r\n\r\n;==========================================\r\n\r\n;==========================================\r\n;停止自动增加元宝\r\n[@AUTOADDGAMEGOLD]\r\n#IF\r\n#ACT\r\nAUTOADDGAMEGOLD STOP\r\n#SAY\r\n现退出自动增加元宝模式 \r\n\r\n;==========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("是否自动增加元宝", "AUTOADDGAMEGOLD");

                    cd = new CompletionData("更改人物名称颜色", "功能：\r\n更改名称颜色 \r\n\r\n格式：\r\n\r\nCHANGENAMECOLOR 颜色代码(0-255) \r\n\r\n=========================\r\n;设置人物的名称颜色\r\n[@CHANGENAMECOLOR]\r\n#IF\r\n#ACT\r\nCHANGENAMECOLOR 2\r\n#SAY\r\n你的颜色已改变。\r\n=========================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("更改人物名称颜色", "CHANGENAMECOLOR");

                    cd = new CompletionData("更改人物头发类型", "功能：\r\n更改头发类型 \r\n\r\n格式：\r\n=========================\r\n[@HAIRSTYLE]\r\n#IF\r\n#ACT\r\nHAIRSTYLE 0\r\n=========================\r\n\r\n[@HAIRSTYLE1]\r\n#IF\r\n#ACT\r\nHAIRSTYLE 1\r\n=========================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("更改人物头发类型", "HAIRSTYLE");

                    cd = new CompletionData("更改人物职业", "功能：\r\n更改人物职业 \r\n\r\n格式：\r\n\r\nCHANGEJOB 职业名称(WARRIOR,WIZARD,TAOIST) \r\n\r\n;==========================================\r\n;转职业为武士\r\n[@CHANGEJOB0]\r\n#IF\r\n#ACT\r\nCHANGEJOB WARRIOR\r\n#SAY\r\n你的职业已经变成武士了。\r\n;==========================================\r\n\r\n;==========================================\r\n;转职业为法师\r\n[@CHANGEJOB1]\r\n#IF\r\n#ACT\r\nCHANGEJOB WIZARD \r\n#SAY\r\n你的职业已经变成法师了。\r\n;==========================================\r\n\r\n;==========================================\r\n;转职业为道士\r\n[@CHANGEJOB2]\r\n#IF\r\n#ACT\r\nCHANGEJOB TAOIST \r\n#SAY\r\n你的职业已经变成道士了。\r\n;==========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("更改人物职业", "CHANGEJOB");

                    cd = new CompletionData("清除人物的仓库密码", "功能：\r\n    清除人物的仓库密码 \r\n\r\n格式：\r\n    CLEARPASSWORD \r\n\r\n;==========================================\r\n;清除人物的仓库密码\r\n[@CLEARPASSWORD]\r\n#IF\r\n#ACT\r\nCLEARPASSWORD \r\n#SAY\r\n你的仓库密码已清除。\r\n;==========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("清除人物的仓库密码", "CLEARPASSWORD");

                    cd = new CompletionData("清除人物转生", "功能：\r\n   清除人物转生数据(即人物转生后恢复到未转生状态) \r\n\r\n格式：\r\n   RESTRENEWLEVEL\r\n\r\n;==========================================\r\n[@RESTRENEWLEVEL]\r\n#IF\r\n#ACT\r\nRESTRENEWLEVEL\r\n#SAY\r\n你的转生等级已经取消。\r\n;==========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("清除人物转生", "RESTRENEWLEVEL");

                    cd = new CompletionData("清除列表所有内容", "功能：\r\n   清除列表内容。 \r\n\r\n格式：\r\n   CLEARNAMELIST 名单.txt \r\n\r\n;==========================================\r\n[@MIAN]\r\n#IF\r\n#ACT\r\nCLEARNAMELIST 名单.txt\r\n#SAY\r\n名单里所有内容全部清除了。\r\n;==========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("清除列表所有内容", "CLEARNAMELIST");

                    cd = new CompletionData("清除地图物品", "功能：\r\n    清除地图物品 \r\n\r\n格式：\r\n    CLEARMAPITEM 地图 X Y 范围 \r\n\r\n;==========================================\r\n;清除盟重安全区内的回城卷\r\n[@main]\r\n#IF\r\n#ACT\r\nCLEARMAPITEM 3 330 330 10\r\n#SAY\r\n盟重安全区内的回城卷都被清除了。\r\n;========================================== \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("清除地图物品", "CLEARMAPITEM");

                    cd = new CompletionData("清除师徒信息", "功能：\r\n    清除师徒信息。 \r\n\r\n格式：\r\n    DELMASTER\r\n;==========================================\r\n[@Main]\r\n#IF\r\n#ACT\r\nDELMASTER\r\n#SAY\r\n你已经和你师傅脱离关系了\r\n;==========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("清除师徒信息", "DELMASTER");

                    cd = new CompletionData("清除地图中的怪物", "功能：\r\n   用于清除指定地图里的怪物,人物的宝宝不在此范围以内,禁止清除怪物列表中的怪物不会被清除。\r\n    此功能一般用于动态刷怪方式,在有人的地图出现怪物,没人时地图上的怪物全部清除,以大大节约机器资源。 \r\n\r\n格式：\r\n   CLEARMAPMON 地图号 \r\n\r\n;==========================================\r\n[@MIAN]\r\n#IF\r\n#ACT\r\nCLEARMAPMON 3 \r\n#SAY\r\n盟重地图内所有的怪物都已经清理干净了。\r\n;==========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("清除地图中的怪物", "CLEARMAPMON");

                    cd = new CompletionData("清除结婚信息", "功能：\r\n   清除结婚信息\r\n\r\n格式：\r\n   DELMARRY\r\n\r\n;==========================================\r\n;清除结婚信息\r\n[@main]\r\n#IF\r\n#ACT\r\nDELMARRY\r\n#SAY\r\n你的结婚信息已清除。\r\n;========================================== \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("清除结婚信息", "DELMARRY");

                    cd = new CompletionData("物品双击触发脚本", "功能：\r\n双击物品可以自动触发指定脚本功能； \r\n\r\n使用方法：\r\n1、物品数据库设置1：STDMODE字段设置为：31\r\n2、物品数据库设置2：AniCount字段设置为：X ,X为指字数字触发脚本(1-999) \r\n3、\\Mir200\\Envir\\market_def\\QFunction-0.txt下设置 \r\n\r\n[@StdModeFuncX]\r\n#IF\r\n#ACT\r\nsendmsg 7 触发成功！！！ \r\n===================================\r\n注：其中X为物品数据库设置2中的X \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("物品双击触发脚本", "[@StdModeFuncX]\r\n#IF\r\n#ACT\r\nsendmsg 7 触发成功！！！");

                    cd = new CompletionData("特修人物所有装备", "功能：\r\n   特修人物所有装备 \r\n\r\n格式：\r\n　　　RepairAll\r\n\r\n;==========================================\r\n[@SuperRepair]\r\n#If\r\nCheckLevelEx > 0 \r\n#Act\r\nRepairAll\r\nSendMsg 5 [提示]:看你那身破烂东西.现在给你全部修好了. \r\n;==========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("特修人物所有装备", "REPAIRALL");

                    cd = new CompletionData("申请多英雄(新)", "功能：\r\n    CREATEHEROEX //申请多英雄(新)。 \r\n\r\n格式：\r\n   CREATEHEROEX ,申请储备英雄,参考脚本附件： 申请英雄.txt\r\n\r\n命令格式：\r\n\r\n[@@buHeroEx]\r\n#ACT\r\nDELAYCALL 10 ~$CREATEHEROEX\r\n[~$CREATEHEROEX]\r\n选择英雄职业：\\ \\\r\n<男战士/@CREATEHERO_WARR_MAN1> <女战士/@CREATEHERO_WARR_WOM1>\\\r\n<男法师/@CREATEHERO_WIZA_MAN1> <女法师/@CREATEHERO_WIZA_WOM1>\\\r\n<男道士/@CREATEHERO_TAOS_MAN1> <女道士/@CREATEHERO_TAOS_WOM1>\\ \\\r\n<关闭/@exit>\r\n[@CREATEHERO_WARR_MAN1]\r\n#ACT\r\nCREATEHEROEX 0 0\r\n[@CREATEHERO_WARR_WOM1]\r\n#ACT\r\nCREATEHEROEX 0 1\r\n[@CREATEHERO_WIZA_MAN1]\r\n#ACT\r\nCREATEHEROEX 1 0\r\n[@CREATEHERO_WIZA_WOM1]\r\n#ACT\r\nCREATEHEROEX 1 1\r\n[@CREATEHERO_TAOS_MAN1]\r\n#ACT\r\nCREATEHEROEX 2 0\r\n[@CREATEHERO_TAOS_WOM1]\r\n#ACT\r\nCREATEHEROEX 2 1\r\n\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("申请多英雄(新)", "CREATEHEROEX");

                    cd = new CompletionData("离线挂机涨经验", "功能：\r\n　　　改进的离线泡点系统.增加参数三\r\n\r\n格式：\r\n　　　OffLine S Exp KickTime\r\n　　　参数一S单位为秒.参数二Exp为得到的经验值.意为每隔S秒将或者Exp的经验值.默认只在安全区有效.参数三KickTime为踢下线的时间.单位为分.\r\n\r\n      参数三省略时则默认15天踢下线..\r\n\r\n      参数三 < = 0时,无限时间脱机挂着,否则为脱机的时间限制(分钟),超过该时间踢下线\r\n\r\n;==========================================\r\n[@OffLinePlay]\r\n#If\r\nCheckLevelEx > 0 \r\n#Act\r\nSendMsg 5 [提示]:每5秒增加300点经验值.只让你挂1个小时. \r\nOffLine 5 300 60\r\n;==========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("离线挂机涨经验", "[@OffLinePlay]\r\n#If\r\nCheckLevelEx > 0 \r\n#Act\r\nSendMsg 5 [提示]:每5秒增加300点经验值.只让你挂1个小时. \r\nOffLine 5 300 60");

                    cd = new CompletionData("离线挂机,自动打怪", "功能：\r\n   OFFLINEPLAYEX 时间(分) //离线挂机,自动打怪；不建议挂太多,只会消耗资源！ \r\n\r\n挂机的人物必须学习了主动技能,是否不会进行攻击怪物 \r\n\r\n格式：\r\n\r\n;==========================================\r\n[@OFFLINEPLAYEX]\r\n#If\r\nCheckLevelEx > 0 \r\n#Act\r\nOFFLINEPLAYEX 5000 \r\n;==========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("离线挂机,自动打怪", "[@OFFLINEPLAYEX]\r\n#If\r\nCheckLevelEx > 0 \r\n#Act\r\nOFFLINEPLAYEX 5000");

                    cd = new CompletionData("组队地图传送(新)", "功能：\r\nGROUPMAPMOVE //编组地图传送(新) \r\n\r\n格式：\r\n\r\nGROUPMAPMOVE(GROUPMOVE) 地图号 X Y lv @lab cap\r\nlv 最少等级, 非空非0数字起作用\r\n@lab 非空触发QFunction对应节\r\ncap 是否组长才可以使用此命令(空字符=不限制, 非空= 组长才可以使用)\r\n\r\n============================\r\n#IF\r\nISGROUPMASTER\r\n#ACT\r\nGROUPMAPMOVE 3 330 330 7 @传送\r\n#ELSESAY\r\n你不是组长 \r\n===========================\r\n\r\nQFunction.txt 内容如下： \r\n\r\n[@传送]\r\n#IF\r\nCHECKLEVELEX > 7\r\n#SAY\r\n你的条件完全达到,可以继续完成任务。 \r\n\r\n===========================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("组队地图传送(新)", "GROUPMAPMOVE");

                    cd = new CompletionData("组队地图随机传送", "功能：\r\nGROUPMOVE //编组地图随机传送。 \r\n\r\n格式：\r\n\r\n只有组长,才可以使用\r\n============================\r\n[@GROUPMOVE]\r\n#IF\r\n#ACT\r\nGROUPMOVE 3 \r\n\r\n===========================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("组队地图随机传送", "GROUPMOVE");

                    cd = new CompletionData("自动召唤英雄", "RecallHero //召唤英雄,参数不为空时回收英雄\r\n\r\n例子： \r\n\r\n[@自动召唤出英雄]\r\n#IF\r\nHAVEHERO\r\n#ELSEACT\r\nBREAK\r\n#IF\r\n#ACT\r\nRecallHero\r\nBREAK \r\n\r\n \r\n\r\n[@自动收回英雄]\r\n#IF\r\nHAVEHERO\r\n#ELSEACT\r\nBREAK\r\n#IF\r\n#ACT\r\nRecallHero 1 \r\nBREAK \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("自动召唤英雄", "RECALLHERO");

                    cd = new CompletionData("自定义变量功能", "功能：自定义变量功能,不再受固定几个变量限制\r\n\r\n使用方法：\r\n\r\n几个步骤如下：\r\n\r\n1、首先要声明变量\r\n\r\nVAR STRING HUMAN www.BLuem2.vip\r\nVAR INTEGER HUMAN 395973 \r\n\r\nVAR STRING GLOBAL 财富第一名\r\nVAR INTEGER GLOBAL 第一财富值 \r\n\r\nSTRING 代表字符变量声明\r\nINTEGER 代表数字变量声明\r\nHUMAN 代表私有个人变量\r\nGLOBAL 代表公共全局变量 \r\n\r\n注：全局变量声明与普通变量不同,引擎启动后,执行QManage NPC [@OnStart]段一次：请将全局变量声明写在该段下。 \r\n\r\n如：\r\n\r\n[@OnStart]\r\n#IF\r\n#ACT\r\nVAR STRING GLOBAL 财富第一名\r\nLOADVAR GLOBAL 财富第一名 ..\\QUESTDIARY\\数据文件\\排名\\自定义全局变量数据.TXT\r\n\r\n2、读取变量\r\n\r\nLOADVAR HUMAN www.BLuem2.vip ..\\QUESTDIARY\\数据文件\\网站\\www.BLuem2.vip.TXT\r\nLOADVAR HUMAN 395973 ..\\QUESTDIARY\\数据文件\\网站\\395973.TXT\r\nLOADVAR GLOBAL 财富第一名 ..\\QUESTDIARY\\数据文件\\排名\\自定义全局变量数据.TXT\r\nLOADVAR GLOBAL 第一财富值 ..\\QUESTDIARY\\数据文件\\排名\\自定义全局变量数据.TXT \r\n\r\n3、保存变量\r\n\r\nCALCVAR HUMAN 395973 + 5\r\nSAVEVAR HUMAN 395973 ..\\QUESTDIARY\\数据文件\\网站\\395973.TXT \r\n\r\nCALCVAR GLOBAL 财富第一名 = <$USERNAME>\r\nSAVEVAR GLOBAL 财富第一名 ..\\QUESTDIARY\\数据文件\\排名\\自定义全局变量数据.TXT \r\n4、检查变量\r\n\r\n#IF\r\nCHECKVAR HUMAN 395973 = 5 ;支持格式( > < = ? ) ?是代表等于或大于\r\n#ACT\r\nsendmsg 7 等于5 \r\n\r\n#IF\r\nCHECKVAR HUMAN 395973 = <$STR(M2)> ;支持变量格式\r\n#ACT\r\nsendmsg 7 等于5 \r\n\r\n5、数字类变量操作\r\n\r\nCALCVAR HUMAN 395973 = 0 ;支持格式( + - * / = )\r\nSAVEVAR HUMAN 395973 ..\\QUESTDIARY\\数据文件\\网站\\395973.TXT \r\n\r\n6、变量显示\r\n\r\n<$HUMAN(395973)> ;私有显示 \r\n\r\n<$GLOBAL(财富第一名)> ;全局显示 \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("自定义变量功能", "VAR STRING HUMAN www.BLuem2.vip");

                    cd = new CompletionData("自定义命令内容功能(改进)", "新功能：\r\n\r\nQUERYVALUE xxx ret len lab npc //向客户端发送请求窗口 \r\n\r\n新介绍：\r\n\r\nxxx获取请求的返回值变量编号,ret=0返回到$STR(Sxxx)ret=1返回到$STR(Mxxx)<br>ret请求的返回值类型,0=文字,1=数字,2=无返回值\r\nlen 客户端允许输入的字符长度,ret=0或1 起作用\r\nlab 执行后跳转的NPC节段lab\r\nnpc 执行后跳转的NPC宿主,QF= QFunction,QM= QManage,其他值则是当前的NPC\r\n\r\n示例：\r\n\r\n[@qv1]\r\n#ACT\r\nQUERYVALUE 45 1 10 @CheckNo 请在下面输入验证码：\r\n[@CheckNo]\r\n你输入了：<$STR(M45)> \r\n\r\n[@qv2]\r\n#ACT\r\nQUERYVALUE 2 0 20 @CheckNo2 请在下面输入验证码： QF //触发：QFunction-0.txt [@CheckNo2] #ACT SENDMSG 5 QueryValue：<$STR(S2)> \r\n\r\n[@qv3]\r\n#ACT\r\nQUERYVALUE 8 0 20 @CheckNo3 请在下面输入验证码： QM //触发：QManage.txt [@CheckNo3] #ACT SENDMSG 5 QueryValue：<$STR(S8)> \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("自定义命令内容功能(改进)", "QUERYVALUE");

                    cd = new CompletionData("自定义命令触发", "功能：\r\nUSERCMD ；//自定义命令触发,是通过调用功能脚本里的相应脚本实现。 \r\n\r\n格式：\r\n配置文件：UserCmd.txt 在Envir下\r\n配置文件格式： \r\n\r\n;========================\r\n;命令名称对应编号\r\n会员 1\r\n挑战 2\r\n仓库 3\r\n;======================= =\r\n\r\n功能脚本格式：QFunction-0.txt\r\n\r\n[@UserCmd1]\r\n#IF\r\n#SAY\r\n你好,会员玩家。 \r\n\r\n[@UserCmd2]\r\n#IF\r\n#SAY\r\n你想挑战吗？ \r\n\r\n[@UserCmd3]\r\n#IF\r\n#SAY\r\n你想找会什么装备。 \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("自定义命令触发", "USERCMD");

                    cd = new CompletionData("自定义脚本字体颜色", "功能：\r\n<COLOR=clSkyBlue 你好！>\\<关闭/@exit>\r\n\r\n<COLOR=$123456 你好！> //$表示16进制 \r\n\r\n格式：\r\n\r\n例：<COLOR=clSkyBlue 你好！>\\<关闭/@exit>\r\nclBlack, clMaroon, clGreen, clOlive, clNavy, clPurple, clTeal, clGray, \r\nclSilver, clRed, clLime, clYellow, clBlue, clFuchsia, clAqua, clLtGray, \r\nclDkGray, clWhite, clMoneyGreen, clSkyBlue, clCream, clMedGray \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("自定义脚本字体颜色", "<COLOR=clSkyBlue 你好！>\\<关闭/@exit>");

                    cd = new CompletionData("获取字符长度", "功能：\r\nGETSTRLENGTH $STR(S1) M1 //获取$STR(S1)字符的长度到M1\r\n\r\n格式：\r\n============================\r\n[@GETSTRLENGTH]\r\n#IF\r\n#ACT\r\nMOV S1 <$USERNAME>\r\nGETSTRLENGTH $STR(S1) M1\r\n===========================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("获取字符长度", "GETSTRLENGTH");

                    cd = new CompletionData("获取对面人物名称", "功能：\r\n获取对面人物名称。\r\n\r\n格式：GetPoseName 变量 \r\n\r\n#IF \r\n#ACT\r\nGetPoseName S0\r\nSendMsg 5 你对面的人物是: <$STR(S0)> \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("获取对面人物名称", "GETPOSENAME");

                    cd = new CompletionData("获取装备属性到变量中", "功能：\r\n   获取装备属性到变量中 \r\n\r\n格式：\r\nGETDLGITEMVALUE 变量M0~99(获取值到变量M0~99) 属性位置(0-41) 检查条件需要配合QUERYITEMDLG命令\r\n\r\n;========================================== \r\n\r\n[@main]\r\n#ACT\r\nDELAYCALL 10 @DELAY_UPGRADEDLGITEM \r\n\r\n[@DELAY_UPGRADEDLGITEM]\r\n#ACT\r\nQUERYITEMDLG 查询装备合成需求 @GETDLGITEMVALUE 0 \r\n\r\n[@GETDLGITEMVALUE] \r\n#IF\r\nCHECKDLGITEMTYPE WEAPON\r\nCHECKDLGITEMNAME 木剑 \r\nCHECKDLGITEMADDVALUE 3 ? 10\r\n#ACT\r\nGETDLGITEMVALUE M3 3\r\n#SAY\r\n你的<$DLGITEMNAME>目前幸运+<$STR(M3)>\r\n#ELSESAY\r\n你提交的是什么物品？我要的可是木剑,是武器啊！\\\r\n;==========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("获取装备属性到变量中", "GETDLGITEMVALUE");

                    cd = new CompletionData("行会地图传送", "功能：\r\n　　　行会传送NPC命令\r\n\r\n格式：\r\n　　　GuildMapMove M X Y \r\n　　　参数一M为地图代码.参数二X为坐标X.参数三Y为坐标Y.\r\n\r\n;==========================================\r\n[@GuildMapMove]\r\n#If\r\nCheckLevelEx > 0 \r\n#Act\r\nGuildMapMove 3 333 333 \r\nSendMsg 5 [提示]:大家都回城了吧.哈哈 \r\n;==========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("行会地图传送", "GUILDMAPMOVE");

                    cd = new CompletionData("装备属性升级(一)", "功能：\r\n装备升级功能可以指定升级物品及属性,按指定机率得到结果。\r\n需要升级的装备物品必须放在身上。 UPGRADEITEMEX 的第7参数是1时,点数几率参数将不随机,直接赋予点数 \r\n\r\n格式：\r\n\r\nUPGRADEITEMEX 物品位置(0-12) 连击引擎属性位置(0-22) 成功机率(0-100) 点数机率(0-255) 是否破碎或还原(0,1,2)(2代表还原) 第六参数,非空时,不显示成功、失败、破碎等信息 \r\n\r\n;=================================\r\n\r\n[@MAIN]\r\n#IF\r\n#ACT\r\nUPGRADEITEMEX 1 0 1 1 2 0\r\nUPGRADEITEMEX 1 1 1 1 2 0\r\nUPGRADEITEMEX 1 2 1 1 2 0\r\nUPGRADEITEMEX 1 3 1 1 2 0\r\n;=================================\r\n\r\n物品位置:\r\n0 盔甲\r\n1 武器\r\n2 照明物(蜡烛,此物品属性升级无效)\r\n3 项链\r\n4 头盔\r\n5 右手镯\r\n6 左手镯\r\n7 右戒指\r\n8 右戒指\r\n9 无(放护身符位置)\r\n10 腰带\r\n11 鞋子\r\n12 宝石 \r\n\r\n成功机率:\r\n升级成功机率,数字越大机率越小。\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("装备属性升级(一)", "UPGRADEITEMEX 1 0 1 1 2 0");

                    cd = new CompletionData("装备属性升级(二)", "功能：\r\n装备升级功能可以指定升级物品及属性,按指定机率得到结果。\r\n需要升级的装备物品必须放在物品框内。 UPGRADEITEMEX 的第7参数是1时,点数几率参数将不随机,直接赋予点数 \r\n\r\n格式：\r\nUPGRADEDLGITEM 属性位置(0-23) 成功机率(0-100) 点数机率(0-255、随机) 是否破碎或还原(0,1,2)(2代表还原) 第五参数,非空时,不显示成功、失败、破碎等信息 \r\n\r\n;=================================\r\n\r\n[@MAIN]\r\n这里提供武器升级,请放上你的需要升级的武器。\\ \\\r\n<返回/@main> <关闭/@exit>\r\n#ACT\r\nDELAYCALL 10 @DELAY_UPGRADEDLGITEM\r\n[@DELAY_UPGRADEDLGITEM]\r\n#ACT\r\nQUERYITEMDLG 放入需要升级的武器 @QUREY_UPGRADEDLGITEM 0\r\n[@QUREY_UPGRADEDLGITEM]\r\n#IF\r\n//武器\r\nCHECKDLGITEMTYPE WEAPON\r\n#ACT\r\nDELAYCALL 10 @START_UPGRADEDLGITEM\r\n#ELSESAY\r\n你提交的是什么物品？我要的可是武器啊！\\ \r\n\r\n[@START_UPGRADEDLGITEM]\r\n#IF\r\n#ACT\r\nUPGRADEDLGITEM 0 100 5 2 0\r\nUPGRADEDLGITEM 1 100 5 2 0\r\nUPGRADEDLGITEM 2 100 5 2 0\r\n......");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("装备属性升级(二)", "UPGRADEDLGITEM 0 100 5 2 0");

                    cd = new CompletionData("装备属性升级(三)", "装备属性升级(三)\r\n功能：\r\n物品名 数量 属性位置 属性值 (普通鉴定属性|神秘鉴定属性|特殊属性|神技|几鉴)   注意：GIVE  给予鉴定属性, 必须在最后加入几鉴才可生效,1 2 3  //分别代表1鉴 2鉴 3鉴 \r\n\r\n格式：\r\n\r\n#IF\r\n#ACT\r\nGIVE 屠龙 1 0 0 1=11,2=12,3=13,9=15|24=2,25=1,26=3,27=6|67|127|1\r\n\r\n解释：\r\n普通鉴定属性：最多4项属性(“,”号分隔),每个属性：属性位置=属性值\r\n神秘鉴定属性：(同上) \r\n\r\n属性值范围1~15,属性位置范围1~30,对应如下属性：\r\n数字 类型 起作用的StdMode\r\n----------------------------------------\r\n1 攻击 通用\r\n2 魔法 通用\r\n3 道术 通用\r\n4 物防 非 5, 6, 19, 20, 21, 23, 24\r\n5 魔防 非 5, 6, 19, 20, 21, 23, 24\r\n6 准确 5, 6, 20, 24\r\n7 敏捷 20, 24\r\n8 魔法躲避 19, 20, 21, 23, 24\r\n9 幸运 5, 6, 19, 20, 21, 23, 24\r\n10 诅咒 5, 6\r\n......");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("装备属性升级(三)", "GIVEEX");

                    cd = new CompletionData("装备强化、发光属性", "功能：\r\n\r\nStditemConvert.exe 放入DB目录下,点击自动转换StdItems.DB后将会生成出新的StdItem_new.DB,将原来的数据改名备份或删除,StdItem_new.DB改为默认的StdItem.DB名称即可 \r\n\r\n属性：\r\n\r\n7个装备属性：\r\n忽视目标防御 //位置17,数值0~15\r\n增加伤害 //位置18,数值0~15\r\n伤害反射 //位置19,数值0~15\r\n物理伤害减少 //位置20,数值0~15\r\n魔法伤害减少 //位置21,数值0~15\r\n经验吸收增加 //位置22,数值0~15,暂时未起用\r\n装备发光效果 //位置23,数值0~2\r\n可以使用@Supermake,UpgradeItems,UpgradeItemsEx,Give命令调整以下装备属性 \r\n\r\n也可以在StdItem.DB数据库中填写：\r\nSmite //暴击\r\nDropRate //PK目标爆率\r\nIgnDef //忽视目标防御\r\nDamAdd //增加伤害\r\nDamReb //伤害反射\r\nDcRedu //物理伤害减少\r\nMcRedu //魔法伤害减少\r\nExpAdd //经验吸收增加 \r\n\r\nBind //装备绑定设置,配合[拾取后绑定]和[装备后绑定]使用\r\n//Bind=0 拾取,穿戴装备都不绑定\r\n//Bind=1 拾取后绑定\r\n//Bind=2 装备后绑定\r\n//Bind= 3 拾取后绑定 + 装备后绑定 \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("装备强化、发光属性", "BIND");

                    cd = new CompletionData("装备邦定帐号功能", "功能：\r\n绑定装备的控制在 功能设置->全局设置 \r\n\r\n使用方法：\r\n\r\nQUERYBINDITEM 0 //弹出绑定窗口\r\nQUERYBINDITEM 1 //弹出解绑窗口\r\nRESUMEBINDITEM //允许继续执行绑定\r\nRESUMEUNBINDITEM //允许继续执行解绑 \r\n\r\n[@ItemBinding] //进行绑定之前,做检测用\r\n[@ItemUnBinding] //进行解绑之前,做检测用\r\n[@ItemBinded] //绑定成功后\r\n[@ItemUnBinded] //解绑成功后 \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("装备邦定帐号功能", "QUERYBINDITEM");

                    cd = new CompletionData("解封乾坤玉璧(未启用)", "功能：\r\n   解封乾坤玉璧 \r\n\r\n格式：\r\nUNWRAPNIMBUSITEM 名称 //检测第一参数,物品名\r\n\r\n增加解封乾坤玉壁物品NPC条件命令：\r\n-----------------------------------------------------------------\r\n[@UnWrap]\r\n#IF\r\nUNWRAPNIMBUSITEM 乾坤玉璧\r\n#SAY\r\n封乾玉壁解封成功。\r\n#ACT\r\nTAKE 金币 5000 //需要5000金币才给解封 \r\n#ELSESAY\r\n没有可以解封的物品。 \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("解封乾坤玉璧(未启用)", "UNWRAPNIMBUSITEM");

                    cd = new CompletionData("设置NPC刷怪", "功能：\r\n设置NPC刷怪功能 \r\n\r\nPARAM1 地图号\r\nPARAM2 Y坐标 \r\nPARAM3 X坐标 \r\nMONGEN 怪物名 数量 范围 \r\n\r\n示例：\r\n\r\n#IF\r\nCHECKMONMAP G005 150\r\n#ACT\r\nGIVE 回城卷 1\r\nMAPMOVE G005 67 38\r\n#ELSEACT\r\nPARAM1 G005\r\nPARAM2 50\r\nPARAM3 50\r\nMONGEN 巨型多角虫 50 100\r\nMONGEN 狼 50 100\r\nMONGEN 虎蛇 50 100\r\nMONGEN 红蛇 50 100\r\nGIVE 回城卷 1\r\nMAPMOVE G005 67 38 \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("设置NPC刷怪", "MONGEN");

                    cd = new CompletionData("设置人物下线是否自动安全区挂机", "功能：\r\n增加NPC命令控制人物下线是否自动脱机,如下：\r\n\r\n示例：\r\n\r\n此命令只为NPC命令！可以放在任何脚本里使用！建议放在登陆脚本里进行执行！\r\n\r\n比如：\r\n\r\nD:\\Mirserver\\Mir200\\Envir\\MapQuest_def\\QManage.txt\r\n\r\n一开始就执行！\r\n\r\n[@Login]\r\n#IF\r\nCHECKLEVELEX > 0\r\n#ACT\r\nSETOFFLINEPLAY ON\r\nSENDMSG 7 你现在已经开启下线安全区自动脱机功能！\r\n\r\n[@Login]\r\n#IF\r\nCHECKLEVELEX > 0\r\n#ACT\r\nSETOFFLINEPLAY OFF\r\nSENDMSG 7 你现在没有开启下线安全区自动脱机功能！\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("设置人物下线是否自动安全区挂机", "[@Login]\r\n#IF\r\nCHECKLEVELEX > 0\r\n#ACT\r\nSETOFFLINEPLAY ON\r\nSENDMSG 7 你现在已经开启下线安全区自动脱机功能！\r\n\r\n[@Login]\r\n#IF\r\nCHECKLEVELEX > 0\r\n#ACT\r\nSETOFFLINEPLAY OFF\r\nSENDMSG 7 你现在没有开启下线安全区自动脱机功能！\r\n");

                    cd = new CompletionData("设置人物下线触发脚本", "功能：\r\n　　　设置人物下线触发脚本\r\n\r\n格式：\r\n　　　SetOffLineFunc @Label\r\n　　　参数一@Label为QFunction.txt文本中指定的脚本段落.设置后下线将触发[@Label]如参数一留空则为取消下线触发.\r\n\r\n;==========================================\r\n注意：QManage.txt人物上线执行此功能：\r\n[@Login]\r\n#If\r\n#Act \r\nSetOffLineFunc @OffLine \r\n;==========================================\r\n\r\n;==========================================\r\nQFunction.txt文本中\r\n[@OffLine]\r\n#IF\r\n#ACT\r\nOFFLINE 60 3000000 1440\r\nBreak\r\n\r\n;参数三 < = 0时,无限时间脱机挂着,否则为脱机的时间限制(分钟),超过该时间踢下\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("设置人物下线触发脚本", ";注意：QManage.txt人物上线执行此功能：\r\n[@Login]\r\n#If\r\n#Act \r\nSetOffLineFunc @OffLine \r\n;==========================================\r\n\r\n;==========================================\r\n;QFunction.txt文本中\r\n[@OffLine]\r\n#IF\r\n#ACT\r\nOFFLINE 60 3000000 1440\r\nBreak\r\n");

                    cd = new CompletionData("设置人物任务进度", "功能：\r\n   任务进度 \r\n\r\n格式：\r\nSETMISSION +/-/^ ID(1~65535) 步骤(1~65535) \r\nSETMISSION + 12 2 //增加ID=12的任务 //若当前人物还没有接ID=12的任务,则相当于SETMISSION + 12 1 //若当前人物已经接了ID= 12的任务,则相当于SETMISSION ^12 2\r\n\r\nSETMISSION - 23 //删除ID= 23的任务,触发QFunction的 @CancelMission \r\n\r\nSETMISSION ^ 12 //更新ID=12任务到当前步骤+1\r\n\r\nSETMISSION ^ 34 5 //更新ID=34任务到指定步骤5,\r\n//ID=34的任务存在\r\n//必要条件：ID=34任务步骤>= 5\r\n\r\nCLEARMISSION //清理所有任务 \r\n\r\n;========================================== \r\n[@main] \r\n#IF\r\nCHECKMISSION 1 > 1 \r\n#ACT\r\nSETMISSION ^ 34 5\r\n#SAY\r\n你已经做到了第34步第5小节的任务上了。\r\n#ELSESAY\r\n你都还没开始任务。\\\r\n;==========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("设置人物任务进度", "[@main] \r\n#IF\r\nCHECKMISSION 1 > 1 \r\n#ACT\r\nSETMISSION ^ 34 5\r\n#SAY\r\n你已经做到了第34步第5小节的任务上了。\r\n#ELSESAY\r\n你都还没开始任务。\\");

                    cd = new CompletionData("清理所有任务 ", "清理所有任务");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("清理所有任务 ", "CLEARMISSION");

                    cd = new CompletionData("加入会员人物及时间", "加入会员人物及时间\r\n\r\n加入会员人物及时间： ADDUSERDATE 会员.txt\r\n\r\n相关命令：\r\nCHECKUSERDATE   会员.txt   <       30    p0        p1\r\n    检查命令    会员名单  控制符  天数  使用天数  剩余天数(可用<$STR(p1)>在脚本中显示)\r\n    注：如果要检查忽略人物名字就在p1 后面加个参数 1\r\n\r\n删除会员人物及时间： DELUSERDATE 会员.txt");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("加入会员人物及时间", "ADDUSERDATE");

                    cd = new CompletionData("设置人物在线泡经验", "功能：\r\n在线泡经验(直接得到经验)。 \r\n\r\n格式：\r\n\r\n==================================================\r\n#IF\r\nCheckLevelex > 30\r\n#ACT\r\nsetautogetexp 1 10 1 3\r\n命令 时间 经验 是否安全区(0为任何地方)地图号(任何地图请不用填)\r\n==================================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("设置人物在线泡经验", "CHECKLEVELEX");

                    cd = new CompletionData("设置人物练习技能", "功能：\r\n1.脚本增加技能\r\n\r\nADDSKILL 技能名称 \r\n\r\n2.脚本调整技能等级\r\n\r\nSkillLevel 控制符(=,+,-) 等级数\r\n\r\n;==========================================\r\n#IF\r\n#ACT\r\nADDSKILL 雷电术\r\n#SAY\r\n你已经练习雷电术了。\r\n;==========================================\r\n#If\r\n#Act\r\nSkillLevel 雷电术 = 3\r\n#Say\r\n你的雷电术等级已经为3级\r\n;==========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("设置人物练习技能", ";==========================================\r\n#IF\r\n#ACT\r\nADDSKILL 雷电术\r\n#SAY\r\n你已经练习雷电术了。\r\n;==========================================\r\n#If\r\n#Act\r\nSkillLevel 雷电术 = 3\r\n#Say\r\n你的雷电术等级已经为3级\r\n;==========================================");

                    cd = new CompletionData("设置人物定时系统", "功能：\r\n增加个人定时功能类似机器人！ \r\n\r\n个人定时器系统,格式：SETSCTIMER ID(0-9) 定时间隔(秒)\r\n说明：定时器脚本位于QManage.txt里,定时器对应的为[@OnTimer0] [@OnTimer1] [@OnTimer2] ... [@OnTimer9] \r\n\r\n用于停止指定的定时器,格式：KILLSCTIMER ID(0-9) \r\n\r\n示例：\r\n\r\n开启定时时器\r\n\r\n#IF\r\n#ACT\r\nSetScTimer 0 20\r\n#SAY\r\n你已经开始定时器系统每20秒触发一次QManage.txt里[@OnTimer0]段 \r\n\r\n[@OnTimer0]\r\n#ACT\r\nSendMsg 5 你目前位于%m的(%x：%y) \r\n\r\n停止定时器\r\n\r\n#IF\r\n#ACT\r\nKillScTimer 0\r\n#SAY\r\n0号定时器已经停止 \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("设置人物定时系统", ";开启定时时器\r\n\r\n#IF\r\n#ACT\r\nSetScTimer 0 20\r\n#SAY\r\n你已经开始定时器系统每20秒触发一次QManage.txt里[@OnTimer0]段 \r\n\r\n[@OnTimer0]\r\n#ACT\r\nSendMsg 5 你目前位于%m的(%x：%y) \r\n\r\n;停止定时器\r\n\r\n#IF\r\n#ACT\r\nKillScTimer 0\r\n#SAY\r\n0号定时器已经停止 \r\n");

                    cd = new CompletionData("设置人物属性(一)", "功能：\r\n    设置人物属性翻倍。(0:防御力 1：魔御力 2: 攻击力 3：魔法力 4：道术) \r\n\r\n格式：\r\n    STATUSRATE 类别 倍率 时间 (倍率为整数,时间的单位为秒) \r\n\r\n;==========================================\r\n[@MAIN]\r\n#IF\r\n#ACT\r\nSTATUSRATE 0 2 60\r\n#SAY\r\n恭喜,你获得了60秒,防御属性翻倍的奖励。 \r\n;==========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("设置人物属性(一)", "STATUSRATE");

                    cd = new CompletionData("设置人物属性(二)", "功能：\r\n    设置人物属性附加。(0=HP, 1= MP, 2=防御, 3=魔御, 4=攻击, 5= 魔法, 6= 道术)\r\n\r\n格式：\r\n    ABILITYADD 属性(0~6) 点数(0~65535) 时间(秒) \r\n\r\n;========================================== \r\n例1：\r\n--------------------------------------------\r\n比如原来人物防御：5-10\r\n使用：AbilityAdd 2 123 20,后人物防御为：5+(123/2)-10+123=66-133 //这里下限附加一半,上限全部附加(HP,MP除外)\r\n\r\n例2：\r\n--------------------------------------------\r\n#ACT\r\nMOV M1 $ABILITYADDPOINT0 \r\n\r\n#IF\r\nLARGE M1 0\r\n#ACT\r\nMOV M0 $ABILITYADDTIME0\r\nINC M0 100\r\nAbilityAdd 0 $ABILITYADDPOINT0 $STR(M0)\r\nSENDMSG XX HP附加增加了100秒\r\n;========================================= =\r\n\r\n配套变量：\r\n\r\n<$ABILITYADDPOINT0>~<$ABILITYADDPOINT6> //附加点数\r\n<$ABILITYADDTIME0>~<$ABILITYADDTIME6> //附加时间(秒) \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("设置人物属性(二)", "ABILITYADD");

                    cd = new CompletionData("设置人物延迟进入地图", "说明： \r\nBATCHDELAY //先设置秒延迟,作为ADDBATCH延迟多少的参数\r\nADDBATCH   //就会按前一条BATCHDELAY延迟多久后飞地图\r\nBATCHMOVE  //延迟+随机飞上面增加的地图 \r\n\r\nBATCHMOVE \r\n\r\n示例： \r\n\r\n#IF\r\nDAYTIME NIGHT\r\n#ACT\r\nSendMsg 7 晚上进入祖玛地图,小心怪物异常凶猛！\r\nBATCHDELAY 111\r\nADDBATCH  D5071\r\nBATCHDELAY 222\r\nADDBATCH  D5072\r\nBATCHDELAY 333\r\nADDBATCH  D5073\r\nBATCHMOVE \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("设置人物延迟进入地图", "#IF\r\nDAYTIME NIGHT\r\n#ACT\r\nSendMsg 7 晚上进入祖玛地图,小心怪物异常凶猛！\r\nBATCHDELAY 111\r\nADDBATCH  D5071\r\nBATCHDELAY 222\r\nADDBATCH  D5072\r\nBATCHDELAY 333\r\nADDBATCH  D5073\r\nBATCHMOVE");

                    cd = new CompletionData("设置人物当前权限", "功能：\r\n    设置人物当前权限(0-10) \r\n\r\n格式：(提升到超级GM)\r\n\r\n;==========================================\r\n[@MAIN]\r\n#IF\r\n#ACT\r\nCHANGEPERMISSION 10 \r\n;==========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("设置人物当前权限", "CHANGEPERMISSION");

                    cd = new CompletionData("设置人物当前模式", "功能：\r\n    设置人物当前模式。(1管理模式、2无敌模式、3隐身模式) \r\n\r\n格式：\r\n    CHANGEMODE 模式类型 开关(1为开,0为关)\r\n\r\n;=======================================\r\n[@MAIN]\r\n#IF\r\n#ACT\r\nCHANGEMODE 1 1\r\nCHANGEMODE 2 1\r\nCHANGEMODE 3 1\r\n;=======================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("设置人物当前模式", "CHANGEMODE");

                    cd = new CompletionData("设置人物攻击模式", "功能：\r\n    切换攻击模式。 \r\n\r\n格式：\r\nCHANGEATTACKMODE 数字(0~6) //切换攻击模式 0=全体 1=和平 2=夫妻 3=师徒 4=编组 5=行会 6=善恶 \r\n\r\n0 全体模式\r\n1 和平模式\r\n2 夫妻模式\r\n3 师徒模式\r\n4 编组模式\r\n5 行会模式\r\n6 善恶模式 \r\n\r\n#IF\r\nCHECKATTACKMODE > 0\r\n#ACT\r\nCHANGEATTACKMODE 1\r\n#SAY\r\n你现在的攻击模式是和平模式。 \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("设置人物攻击模式", "CHANGEATTACKMODE");

                    cd = new CompletionData("设置人物称号系统", "功能：\r\n新称号系统 \r\n\r\n增加减少称号物品DB时,请注意Shape的连续性(递增),\r\nShape 编号\r\nweight = 1,图片和名字分为上下显示\r\nSource颜色,0~5(0=白色、1=绿色、2=蓝色、3=紫色、4=红色、5=金色)\r\nReserved 显示DB中的名字(有部分图自带了名字,所以可以写上1)\r\nLooks 图片在 ui1.wzl 中的开始位置\r\nDuraMax 可使用时间,单位小时\r\n其他就等同于装备属性。 \r\n\r\n玩家改变使用称号或刚上线有使用到称号,触发：QFunction 的\r\n人物：[@TitleChanged_XX]\r\n英雄：[@HeroTitleChanged_XX]\r\nXX代表物品DB中的Shape \r\n\r\n格式：\r\nCONFERTITLE 传奇之星 \r\n\r\n;==========================================\r\n[@NAME]\r\n#IF \r\nCHECKTITLE 传奇之星 < 1 //检测称号\r\n#ACT\r\nCONFERTITLE 传奇之星 //授予称号\r\n;==========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("设置人物称号系统", "CONFERTITLE");

                    cd = new CompletionData("设置人物等级", "功能：\r\n   调整当前人物的等级。 \r\n\r\n格式：\r\nCHANGELEVEL (=,+,-) 等级数< /FONT> \r\n\r\n#IF\r\nCHECKLEVELEX ? 70\r\n#ACT\r\nCHANGELEVEL = 70\r\nSENDMSG 5 系统提示：本服目前封顶级别是70级,请不要再进行冲级,否则后果自负。 \r\n\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("设置人物等级", "CHANGELEVEL");

                    cd = new CompletionData("设置人物自动寻路", "功能：\r\n@_automove；//自动寻路坐标\r\n\r\n格式：\r\n\r\n脚本写法：\r\n之前：<自动寻路/@_automove 300:300> //“@_automove”复制此字符,修改后面坐标即可\r\n现在：<自动寻路/@_automove 300:300:盟重省> //“@_automove”检测当前地图是否符合,兼容之前写法 \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("设置人物自动寻路", "@_automove");

                    cd = new CompletionData("设置人物自动穿装备", "功能：\r\n自动穿背包中的装备,TAKEON 物品名称 装备位置\r\n装备位置：\r\n0 = 衣服 \r\n1 = 武器 \r\n2 = 蜡烛 \r\n3 = 项链 \r\n4 = 头盔 \r\n5 = 左手镯 \r\n6 = 右手镯 \r\n7 = 左戒指 \r\n8 = 右戒指 \r\n9 = 护符 \r\n10 = 腰带 \r\n11 = 鞋子 \r\n12 = 宝石 \r\n\r\n格式：\r\n\r\nTAKEON 物品名称 装备位置 \r\n\r\n=========================\r\n[@TAKEON]\r\n#IF\r\n#ACT\r\nGIVE 屠龙 1\r\nGIVE 圣战宝甲 1\r\nTAKEON 圣战宝甲 0\r\nTAKEON 屠龙 1 \r\n=========================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("设置人物自动穿装备", "TAKEON");

                    cd = new CompletionData("设置人物转生", "功能：\r\n    设置人物转生。 \r\n\r\n格式：\r\n    RENEWLEVEL 转次数 转后等级 分配点数\r\n    转次数 代表一次转多少级(数值范围为 1 - 255)\r\n    转后等级 代表转生后人物的等级,0为不改变人物当前等级。\r\n    分配点数 代表转生后可以得到的点数,此点数可能按比例换成人物属性点(数值范围 1 - 20000)。 \r\n\r\n;==========================================\r\n;将人物转生一次,后等级设为 28级,分配100点属性\r\n[@RenewLevel]\r\n#IF\r\n#ACT\r\nRENEWLEVEL 1 28 100\r\n#SAY\r\n转生成功。\r\n;========================================== \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("设置人物转生", "RENEWLEVEL");

                    cd = new CompletionData("设置人物骰子数量", "功能：\r\n设置人物骰子数量,跳转指定脚本\r\n\r\n例子：\r\n\r\n[@MAIN]\r\n#IF\r\n#ACT\r\nplaydice 3 @xxx ;玩3颗骰子,跳转到标签[@xxx]");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("设置人物骰子数量", "PLAYDICE");

                    cd = new CompletionData("设置所有行会攻城", "功能：\r\n\r\n设置所有行会攻城\r\n\r\n格式：\r\n\r\nADDTOCASTLEWARLIST 城堡号 * \r\n\r\n;===================================\r\n[@MAIN]\r\n#IF\r\n#ACT\r\nGMEXECUTE ADDTOCASTLEWARLIST 0 *\r\n#SAY\r\n城堡0今天晚上所有行会参加攻城 \r\n;===================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("设置所有行会攻城", "ADDTOCASTLEWARLIST");

                    cd = new CompletionData("设置人物攻击力倍数", "功能：\r\n设置攻击力倍数 \r\n\r\n格式：\r\nPOWERRATE 倍率 有效时间\r\n倍率 为杀攻击力倍数,倍数除以100为真正的倍率(200 为 2 倍经验,150 为1.5倍) 支持变量操作\r\n\r\n;==========================================\r\n[@POWERRATE]\r\n#IF\r\n#ACT\r\nPOWERRATE 1000 600\r\n#SAY\r\n您当前攻击力倍数为 10倍,有效时间 600秒。 \r\n;==========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("设置人物攻击力倍数", "POWERRATE");

                    cd = new CompletionData("设置人物杀怪经验倍数", "功能：\r\n设置杀怪经验倍数. \r\n\r\n格式：\r\nKILLMONEXPRATE 倍率 有效时间\r\n倍率 为杀怪经验倍数,倍数除以100为真正的倍率(200 为 2 倍经验,150 为1.5倍) \r\n\r\n;==========================================\r\n[@KILLMONEXPRATE]\r\n#IF\r\n#ACT\r\nKILLMONEXPRATE 1000 600\r\n#SAY\r\n您当前杀怪经验倍数为 10倍,有效时间 600秒。\r\n;==========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("设置人物杀怪经验倍数", "KILLMONEXPRATE");

                    cd = new CompletionData("设置人物穿人穿怪", "功能：\r\n设置穿人穿怪。 \r\n\r\n格式：\r\nTHROUGHHUMMS参数一M为模式[-1=恢复(;注意-1没空格)/0=穿人穿怪/1=穿怪/2=穿人].参数二S为时间.单位秒\r\n\r\n;==========================================\r\n例：\r\n#IF \r\n#ACT\r\nTHROUGHHUM 0 1800\r\n#Say\r\n1800秒内你可以穿人穿怪了\r\n;==========================================\r\n例：\r\n#IF \r\n#ACT\r\nTHROUGHHUM 1 1200\r\n#Say\r\n1200秒内你可以穿怪了\r\n;==========================================\r\n例：\r\n#IF \r\n#ACT\r\nTHROUGHHUM 2 1200\r\n#Say\r\n1200秒内你可以穿人了\r\n;==========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("设置人物穿人穿怪", "THROUGHHUM");

                    cd = new CompletionData("调整人物PK值", "功能：\r\n调整人物PK值。 \r\n\r\n格式：\r\n\r\nCHANGEPKPOINT 控制符(= ,+,-) 点数 \r\n\r\n;==========================================\r\n;设置人物PK值\r\n[@CHANGEPKPOINT0]\r\n#IF\r\n#ACT\r\nCHANGEPKPOINT = 8\r\n#SAY\r\n你目前的PK值等于8点。 \r\n;==========================================\r\n\r\n;==========================================\r\n;增加人物PK值\r\n[@CHANGEPKPOINT1]\r\n#IF\r\n#ACT\r\nCHANGEPKPOINT + 1\r\n#SAY\r\n你的PK值增加了1点。 \r\n;==========================================\r\n\r\n;==========================================\r\n;减少人物PK值\r\n[@CHANGEPKPOINT2]\r\n#IF\r\n#ACT\r\nCHANGEPKPOINT - 1\r\n#SAY\r\n你的PK值已经减少了1点。 \r\n;==========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("调整人物PK值", "CHANGEPKPOINT");

                    cd = new CompletionData("调整人物会员等级", "功能：\r\n    调整人物会员等级。 \r\n\r\n格式：\r\n    SETMEMBERLEVEL 控制符(=,+,-)  等级数(1-65535) 支持变量操作\r\n\r\n;==========================================\r\n;设置人物会员等级\r\n[@SETMEMBERLEVEL0]\r\n#IF\r\n#ACT\r\nSETMEMBERLEVEL = 5\r\n#SAY\r\n你的会员等级为5。 \r\n;==========================================\r\n\r\n;==========================================\r\n;增加人物会员等级\r\n[@SETMEMBERLEVEL1]\r\n#IF\r\n#ACT\r\nSETMEMBERLEVEL + 1\r\n#SAY\r\n你的会员等级已经改变。 \r\n;==========================================\r\n\r\n;==========================================\r\n;减少人物会员等级\r\n[@SETMEMBERLEVEL2]\r\n#IF\r\n#ACT\r\nSETMEMBERLEVEL - 1\r\n#SAY\r\n你的会员等级已经改变。 \r\n;==========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("调整人物会员等级", "SETMEMBERLEVEL");

                    cd = new CompletionData("调整人物会员类型", "功能：\r\n    调整人物会员类型。 \r\n\r\n格式：\r\n    SETMEMBERTYPE 控制符(=,+,-)  类型数(1-65535) 支持变量操作\r\n\r\n;==========================================\r\n;设置人物会员类型\r\n[@SETMEMBERTYPE0]\r\n#IF\r\n#ACT\r\nSETMEMBERTYPE = 5\r\n#SAY\r\n你的会员类型为5。 \r\n;==========================================\r\n\r\n;==========================================\r\n;增加人物会员类型\r\n[@SETMEMBERTYPE1]\r\n#IF\r\n#ACT\r\nSETMEMBERTYPE + 1\r\n#SAY\r\n你的会员类型已经改变。 \r\n;==========================================\r\n\r\n;==========================================\r\n;减少人物会员类型\r\n[@SETMEMBERTYPE2]\r\n#IF\r\n#ACT\r\nSETMEMBERTYPE - 1\r\n#SAY\r\n你的会员类型已经改变。 \r\n;==========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("调整人物会员类型", "SETMEMBERTYPE");

                    cd = new CompletionData("调整人物保存型变量", "功能：INTS 0~9 + - = 数量\r\n\r\n变量显示：<$INTS0>~<$INTS9> \r\n\r\n#IF\r\nCHECKINTS 0~9 >/</= 数量\r\n#ACT\r\nINTS 0 + 1\r\nINTS 0 = 1\r\nINTS 0 - 1\r\n\r\n注：\r\n\r\n提供对十个整数操作,记录日志在元宝类型中,搜索日志以关键字“整数”进行过滤,\r\n可以使用<$INTS0~9>获取和显示,英雄以<$H.INTS0~9>获取和显示\r\nMYSQL版本需要更新数据库的TBL_ADDON表和DBServer,以读取和保存新数据,最新数据库结构参考legendofmir.sql\r\n如果需要把当前版本的普通数据转换到MYSQL数据,同样需要使用本次更新的DB2MySQL.exe\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("调整人物保存型变量", "INTS");

                    cd = new CompletionData("调整人物元宝", "功能：\r\n调整人物元宝。 \r\n\r\n格式：\r\n\r\nGAMEGOLD 控制符(= ,+,-)(1-2100000000)\r\n\r\n;==========================================\r\n;设置人物元宝\r\n[@GAMEGOLD0]\r\n#IF\r\n#ACT\r\nGAMEGOLD = 8\r\n#SAY\r\n你已经拥有8颗元宝了。 \r\n;==========================================\r\n\r\n;==========================================\r\n;增加人物元宝\r\n[@GAMEGOLD1]\r\n#IF\r\n#ACT\r\nGAMEGOLD + 1\r\n#SAY\r\n你的元宝已经加了1颗。 \r\n;==========================================\r\n\r\n;==========================================\r\n;减少人物元宝\r\n[@GAMEGOLD2]\r\n#IF\r\n#ACT\r\nGAMEGOLD - 1\r\n#SAY\r\n你的元宝已经扣除了1颗。 \r\n;==========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("调整人物元宝", "GAMEGOLD");

                    cd = new CompletionData("调整人物内功等级", "功能：\r\nCHANGEIPLEVEL 操作符(+ - =) 数值 //修改内功等级,CHANGEIPLEVEL = XX 学习内功\r\n\r\n格式：\r\n\r\n[@CHANGEIPLEVEL]\r\n#IF\r\n#ACT\r\nCHANGEIPLEVEL = 255 \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("调整人物内功等级", "CHANGEIPLEVEL");

                    cd = new CompletionData("调整人物内功经验", "功能：\r\nCHANGEIPEXP 操作符(+ - =) 数值 //修改内功经验\r\n\r\n格式：\r\n\r\n[@CHANGEIPEXP]\r\n#IF\r\n#ACT\r\nCHANGEIPEXP + 5000000 \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("调整人物内功经验", "CHANGEIPEXP");

                    cd = new CompletionData("调整人物声望", "功能：\r\n调整人物声望。 \r\n\r\n格式：\r\n\r\nCREDITPOINT 控制符(= ,+,-)(1-255)\r\n\r\n;==========================================\r\n;设置人物声望\r\n[@CREDITPOINT0]\r\n#IF\r\n#ACT\r\nCREDITPOINT = 8\r\n#SAY\r\n你已经拥有8点声望。\r\n;==========================================\r\n\r\n;==========================================\r\n;增加人物声望\r\n[@CREDITPOINT1]\r\n#IF\r\n#ACT\r\nCREDITPOINT+ 1\r\n#SAY\r\n你的声望已经加了1点。 \r\n;==========================================\r\n\r\n;==========================================\r\n;减少人物声望\r\n[@CREDITPOINT2]\r\n#IF\r\n#ACT\r\nCREDITPOINT- 1\r\n#SAY\r\n你的声望已经减少了1点。 \r\n;==========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("调整人物声望", "CREDITPOINT");

                    cd = new CompletionData("调整人物属性点数", "功能：\r\n    调整人物属性点。 \r\n\r\n格式：\r\n    BONUSPOINT 控制符(= ,+)  点数(0-1000) \r\n\r\n;==========================================\r\n;设置属性点\r\n[@BONUSPOINT0]\r\n#IF\r\n#ACT\r\n  BONUSPOINT = 0\r\n#SAY\r\n你的属性点点已全部清0了。 \r\n;==========================================\r\n\r\n;==========================================\r\n;增加属性点\r\n[@BONUSPOINT1]\r\n#IF\r\n#ACT\r\n  BONUSPOINT + 1\r\n#SAY\r\n你的属性点已经加了1点。 \r\n;==========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("调整人物属性点数", "BONUSPOINT");

                    cd = new CompletionData("调整人物技能修炼点", "功能：\r\nCHANGETRANPOINT 技能名 操作符(+ - =) 数值 //修改技能经验点\r\n\r\n格式：\r\n\r\n[@CHANGETRANPOINT]\r\n#IF\r\n#ACT\r\nCHANGETRANPOINT 烈火剑法 + 500 \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("调整人物技能修炼点", "CHANGETRANPOINT");

                    cd = new CompletionData("调整人物泡点", "功能：\r\n调整人物泡点。\r\n\r\n格式：\r\n\r\nGAMEPOINT 控制符(=,+,-)(1-2100000000)\r\n\r\n;==========================================\r\n;设置人物泡点 \r\n[@GAMEPOINT] \r\n#IF \r\n#ACT \r\nGAMEPOINT = 8 \r\n#SAY \r\n你已经拥有8点泡点了。 \r\n;==========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("调整人物泡点", "GAMEPOINT");

                    cd = new CompletionData("调整人物灵气值", "功能：\r\n    调整人物灵气值。 \r\n\r\n格式：\r\nNIMBUS (+、-、=) 数量//调整人物灵气数量 \r\n\r\n#IF \r\n#ACT\r\nNIMBUS + 200");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("调整人物灵气值", "NIMBUS");

                    cd = new CompletionData("调整人物灵符", "功能：\r\n调整人物灵符。 \r\n\r\n格式：\r\n\r\nGAMEGIRD 控制符(= ,+,-)(1-65535) \r\n\r\n;==========================================\r\n;设置人物灵符\r\n[@GAMEGIRD0]\r\n#IF\r\n#ACT\r\nGAMEGIRD = 8\r\n#SAY\r\n你已经拥有8张灵符。 \r\n\r\n;==========================================\r\n;增加人物灵符\r\n[@GAMEGIRD1]\r\n#IF\r\n#ACT\r\nGAMEGIRD + 1\r\n#SAY\r\n你的灵符已经加了1张。 \r\n\r\n;==========================================\r\n;减少人物灵符\r\n[@GAMEGIRD2]\r\n#IF\r\n#ACT\r\nGAMEGIRD - 1\r\n#SAY\r\n你的灵符已经扣除了1张。\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("调整人物灵符", "GAMEGIR");

                    cd = new CompletionData("调整人物的HP或MP", "功能：\r\n调整人物的HP或MP \r\n\r\n格式：\r\n\r\nHUMANHP(+ -= )数字 \r\n\r\nHUMANMP(+ -= )数字 \r\n=========================\r\n[@HUMANHP0]\r\n#IF\r\n#ACT\r\nHUMANHP + 100 \r\n\r\n[@HUMANHP1]\r\n#IF\r\n#ACT\r\nHUMANHP - 100 \r\n\r\n[@HUMANHP2]\r\n#IF\r\n#ACT\r\nHUMANHP = 100 \r\n=========================\r\n\r\n[@HUMANMP0]\r\n#IF\r\n#ACT\r\nHUMANMP + 100 \r\n\r\n[@HUMANMP1]\r\n#IF\r\n#ACT\r\nHUMANMP - 100 \r\n\r\n[@HUMANMP2]\r\n#IF\r\n#ACT\r\nHUMANMP = 100 \r\n=========================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("调整人物的HP或MP", "HUMANHP（+ -= ）数字 \r\n\r\nHUMANMP（+ -= ）数字 \r\n");

                    cd = new CompletionData("调整人物经验", "功能：\r\n调整人物经验。 \r\n\r\n格式：\r\n\r\nCHANGEEXP (+,-,= ) 经验数 支持变量操作 \r\n\r\n;==========================================\r\n;设置人物经验\r\n[@CHANGEEXP0]\r\n#IF\r\n#ACT\r\nCHANGEEXP = 5000 \r\n#SAY\r\n你当前的经验等于5000点。 \r\n;==========================================\r\n\r\n;==========================================\r\n;增加人物经验\r\n[@CHANGEEXP1]\r\n#IF\r\n#ACT\r\nCHANGEEXP + 5000 \r\n#SAY\r\n你当前的经验增加了5000点。 \r\n;==========================================\r\n\r\n;==========================================\r\n;减少人物经验\r\n[@CHANGEEXP2]\r\n#IF\r\n#ACT\r\nCHANGEEXP - 5000 \r\n#SAY\r\n你当前的经验减少了5000点。 \r\n;==========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("调整人物经验", "CHANGEEXP");

                    cd = new CompletionData("调整人物行会建设", "控制：\r\nGUILDBUILDPOINT 控制符(+ -)数字 //建筑度\r\nGUILDAURAEPOINT控制符(+ -)数字 //人气度\r\nGUILDSTABILITYPOINT控制符(+ -)数字 //安定度\r\nGUILDFLOURISHPOINT控制符(+ -)数字 //繁荣度 \r\n\r\n检测：\r\nCHECKGUILDBUILDPOINT 控制符(<>=)数字 //建筑度\r\nCHECKGUILDAURAEPOINT 控制符(<>=)数字 //人气度\r\nCHECKGUILDSTABILITYPOINT 控制符(<>=)数字 //安定度\r\nCHECKGUILDFLOURISHPOINT 控制符(<>= )数字 //繁荣度 \r\n\r\n变量：\r\n<$GUILDBUILDPOINT> //建筑度\r\n<$GUILDAURAEPOINT> //人气度\r\n<$GUILDSTABILITYPOINT> //安定度\r\n<$GUILDFLOURISHPOINT> //繁荣度 \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("调整人物行会建设", "GUILDBUILDPOINT");

                    cd = new CompletionData("调整人物转生经验", "功能：\r\n    调整人物转生经验。 \r\n\r\n格式：\r\n    CHANGEREEXP 控制符(= ,+,-)\r\n\r\n;==========================================\r\n;清理人物转生经验\r\n[@CHANGEREEXP]\r\n#IF\r\n#ACT\r\nCHANGEREEXP = 0\r\n#SAY< BR> 你的转生经验已全部清0了。\r\n\r\n;==========================================\r\n;增加人物转生经验\r\n[@CHANGEREEXP0]\r\n#IF\r\n#ACT\r\nCHANGEREEXP + 10000 \r\n#SAY< BR> 你的转生经验已增加了10000点。\r\n\r\n;==========================================\r\n;减少人物转生经验\r\n[@CHANGEREEXP1]\r\n#IF\r\n#ACT\r\nCHANGEREEXP - 10000 \r\n#SAY\r\n你的转生经验已减少了10000点。\r\n;==========================================\r\n\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("调整人物转生经验", "CHANGEREEXP");

                    cd = new CompletionData("调整人物金刚石", "功能：\r\n调整人物金刚石。 \r\n\r\n格式：\r\n\r\nGAMEDIAMOND 控制符(= ,+,-)(1-65535)\r\n\r\n;==========================================\r\n;设置人物金刚石\r\n[@GAMEDIAMOND0]\r\n#IF\r\n#ACT\r\nGAMEDIAMOND = 8\r\n#SAY\r\n你已经拥有8颗金刚石了。 \r\n;==========================================\r\n\r\n;==========================================\r\n;增加人物金刚石\r\n[@GAMEDIAMOND1]\r\n#IF\r\n#ACT\r\nGAMEDIAMOND + 1\r\n#SAY\r\n你的金刚石已经加了1颗。 \r\n;==========================================\r\n\r\n;==========================================\r\n;减少人物金刚石\r\n[@GAMEDIAMOND2]\r\n#IF\r\n#ACT\r\nGAMEDIAMOND - 1\r\n#SAY\r\n你的金刚石已经扣除了1颗。 \r\n;==========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("调整人物金刚石", "GAMEDIAMOND");

                    cd = new CompletionData("调整人物金币", "功能：\r\n调整人物金币。 \r\n\r\n格式：\r\n\r\nGIVE 金币 数字 (给予人物金币,最高支持金币200000000) \r\n\r\nTAKE 金币 数字 (扣除人物金币) \r\n\r\n;==========================================\r\n;给予人物金币\r\n[@GIVE]\r\n#IF\r\n#ACT\r\nGIVE 金币 50000 \r\n#SAY\r\n你已经获得50000金币。 \r\n;==========================================\r\n\r\n;==========================================\r\n;删除人物金币\r\n[@TAKE]\r\n#IF\r\n#ACT\r\nTAKE 金币 50000 \r\n#SAY\r\n你的50000金币已经被扣除了。 \r\n;==========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("调整人物金币", "GIVE 金币 数字 （给予人物金币,最高支持金币200000000） \r\n\r\nTAKE 金币 数字 （扣除人物金币） \r\n");

                    cd = new CompletionData("跳转,延迟执行脚本", "功能：\r\n跳转,延时执行脚本。 \r\n\r\n格式\r\n\r\nDELAYGOTO S Label \r\n\r\nDELAYCALL S Label \r\n\r\n参数一S单位为毫秒.参数二Label为跳转的脚本段,以上两个命令都可以进行跳转和延时执行脚本。\r\n;=========================================================\r\n\r\n[@MAIN]\r\n<延时执行脚本/@DELAYCALL> <清除延迟/@CLEARDELAYGOTO> \\ \\\r\n<返回/@main> \r\n[@DELAYCALL]\r\n#IF\r\n#ACT\r\nDELAYGOTO 3 @DELAYCALL1\r\n#SAY\r\n请稍候.... \r\n\r\n[@DELAYCALL1]\r\n#ACT\r\nGIVE 金币 100 \r\n\r\n[@CLEARDELAYGOTO]\r\n#IF\r\n#ACT\r\nCLEARDELAYGOTO\r\n#SAY\r\n延迟跳转已清除\\\r\n<返回/@MAIN> \r\n;=========================================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("跳转,延迟执行脚本", "DELAYGOTO S Label \r\n\r\nDELAYCALL S Label \r\n");

                    cd = new CompletionData("跳转标签", "功能：\r\n   跳转标签 \r\n\r\n格式：\r\nGETDLGITEMVALUE 变量M0~99(获取值到变量M0~99) 属性位置(0-41) 检查条件需要配合 QUERYITEMDLG 命令 \r\n\r\n;========================================== \r\n\r\n[@main]\r\n#ACT\r\nDELAYCALL 10 @DELAY_UPGRADEDLGITEM \r\n\r\n[@DELAY_UPGRADEDLGITEM]\r\n#ACT\r\nQUERYITEMDLG 查询装备合成需求 @QUERYITEMDLG1 0 \r\n\r\n[@QUERYITEMDLG1] \r\n#IF\r\nCHECKDLGITEMTYPE WEAPON\r\nCHECKDLGITEMNAME 木剑 \r\nCHECKDLGITEMADDVALUE 3 ? 10\r\n#ACT\r\nGETDLGITEMVALUE M3 3\r\n#SAY\r\n你的<$DLGITEMNAME>目前幸运+<$STR(M3)>\r\n#ELSESAY\r\n你提交的是什么物品？我要的可是木剑,是武器啊！\\\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("跳转标签", "GETDLGITEMVALUE");

                    cd = new CompletionData("随机读取文档中一行内容到变量中", "功能：\r\n    从指定文件内随机读取一行到变量中。 \r\n\r\n格式：\r\n   1概率型. READRANDOMSTR .\\QuestDiary\\变量\\颜色.txt S15 \r\n\r\n   2随机型. ReadRandomLine .\\QuestDiary\\变量\\颜色.txt S15 \r\n\r\n;========================================== \r\n[@delskill]\r\n#IF\r\n#ACT\r\nREADRANDOMSTR .\\QuestDiary\\变量\\颜色.txt S15\r\n#SAY\r\n你所获得的颜色是：$STR(S15) \r\n\r\n;颜色.txt 文本内字符 获取几率(数字越大,获取该行字符几率越小)\r\n字符1 11\r\n文字2 88\r\n;==========================================\r\n[@delskill]\r\n#IF\r\n#ACT\r\nReadRandomLine .\\QuestDiary\\变量\\颜色.txt S15\r\n#SAY\r\n你所获得的颜色是：$STR(S15) \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("随机读取文档中一行内容到变量中", "1概率型. READRANDOMSTR .\\QuestDiary\\变量\\颜色.txt S15 \r\n\r\n   2随机型. ReadRandomLine .\\QuestDiary\\变量\\颜色.txt S15 \r\n");

                    cd = new CompletionData("随机验证码系统", "功能：\r\n　　　系统随机给一个验证码<$RANDOMNO>.可用于防挂机等等功能噢. \r\n\r\n格式：\r\n　　　SetRandomNo\r\n　　　让系统随机给一个验证码.\r\n　　　CheckRandomNo\r\n　　　检测输入的字符是否为系统给的随机验证码. \r\n\r\n;========================================== \r\n[@Main]\r\n#If\r\nCheckLevelEx > 0 \r\n#Act\r\nSetRandomNo\r\n#Say\r\n<输入验证码/@@CheckNo>：<$RANDOMNO> \r\n\r\n[@@CheckNo]\r\n#If\r\nCheckRandomNo\r\n#Act\r\nSendMsg 5 [提示]:验证码输入正确.\r\nClose\r\n#ElseAct\r\nSendMsg 5 [提示]:验证码输入错误.\r\nClose \r\n;==========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("随机验证码系统", "SETRANDOMNO");

                    cd = new CompletionData("佣兵脚本", "获取佣兵示例脚本");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("佣兵脚本", "(@@inputstring @@CreateMercenary)\r\n\r\n[@main]\r\n#say\r\n佣兵交易向导：\\\r\n1、制作为佣兵的人物角色不小于5级。\\\r\n2、每次制作,背包需要放一张空白的【佣兵契约纸】。\\\r\n3、制作为契约的角色将损失当前等级经验值,但技能、物品、英雄等数据将会保留。\\\r\n4、已经签约的【佣兵契约纸】可以用于创建新角色。\\\r\n5、制作佣兵契约的角色,需要取消金币交易行的所有订单！\\ \\\r\n<制作佣兵契约/@MakeMercenary>   <使用契约创建角色/@@CreateMercenary>\\\r\n<制作佣兵契约(需两步验证)/@MakeMercenaryWithTotpAuth>\\\r\n<制作佣兵契约(需密保验证)/@MakeMercenarymibao>\\\r\n\r\n; ------------------- 制作契约,需密保验证 -------------------\r\n\r\n[@MakeMercenarymibao]\r\n请输入以下正确信息进行验证\\\r\n输入连续错误3次将30分钟内禁止在次输入\\\r\n问题1：【<$STR(S1)>】--------------------<点击输入/@验证1>\\\r\n答案1：【<$STR(S2)>】--------------------<点击输入/@验证2>\\\r\n问题2：【<$STR(S3)>】--------------------<点击输入/@验证3>\\\r\n答案2：【<$STR(S4)>】--------------------<点击输入/@验证4>\\\r\n\r\n \r\n\r\n[@验证1]\r\n#if\r\n!CompVal <$QUIZ> = <$STR(S1)>\r\n#act\r\nQueryValue 1 0 20 ~MMBB1 请输入你的【密保问题1】：\r\n#elseact\r\nMESSAGEBOX 错误提示：当前输入的密保是正确的无需在次输入\r\nBREAK\r\n\r\n\r\n[~MMBB1]\r\n#if\r\ncompval $INTS0 < 3300\r\ncompval $INTS0 > 1500\r\n#act\r\nMOV D1 <$UNIXTIME>\r\nINC D1 <$INTS0>\r\nints 0 = <$STR(D1)>\r\n\r\n#if\r\ncompval $INTS0 > 3300\r\ncompval $INTS0 > $UNIXTIME\r\n#act\r\ncalcval $INTS0 - $UNIXTIME\r\nsendmsg 7 将在<$CALCRESULT>秒后,才可以再次输入\r\ncalcval $INTS0 ~ 0\r\nsendmsg 7 将在<$CALCRESULT>过期,才可以再次输入\r\nMOV S1 \r\nBREAK\r\n\r\n#if\r\ncompval $INTS0 > 3300\r\ncompval $INTS0 < $UNIXTIME\r\n#act\r\nints 0 = 0\r\n\r\n#IF\r\nCompVal <$QUIZ> = <$STR(S1)>\r\n#ACT\r\nDelayGoto 1 @MakeMercenarymibao\r\nBREAK\r\n#elseact\r\nMESSAGEBOX 错误提示：你输入的【密保问题1】错误.连续错误3次将30分钟内禁止在次输入\r\nMOV S1 \r\nints 0 + 600\r\nBREAK\r\n\r\n[@验证2]\r\n#if\r\nEQUAL S1 \r\n#act\r\nMESSAGEBOX 错误提示：请先输入【密保问题1】\r\nBREAK\r\n\r\n#if\r\n!CompVal <$ANSWER> = <$STR(S2)>\r\n#act\r\nQueryValue 2 0 20 ~MMBB2 请输入你的【密保答案1】：\r\n#elseact\r\nMESSAGEBOX 错误提示：当前输入的密保是正确的无需在次输入\r\nBREAK\r\n\r\n\r\n[~MMBB2]\r\n#if\r\ncompval $INTS0 < 3300\r\ncompval $INTS0 > 1500\r\n#act\r\nMOV D1 <$UNIXTIME>\r\nINC D1 <$INTS0>\r\nints 0 = <$STR(D1)>\r\n\r\n#if\r\ncompval $INTS0 > 3300\r\ncompval $INTS0 > $UNIXTIME\r\n#act\r\ncalcval $INTS0 - $UNIXTIME\r\nsendmsg 7 将在<$CALCRESULT>秒后,才可以再次输入\r\ncalcval $INTS0 ~ 0\r\nsendmsg 7 将在<$CALCRESULT>过期,才可以再次输入\r\nMOV S2 \r\nBREAK\r\n\r\n#if\r\ncompval $INTS0 > 3300\r\ncompval $INTS0 < $UNIXTIME\r\n#act\r\nints 0 = 0\r\n\r\n#IF\r\nCompVal <$ANSWER> = <$STR(S2)>\r\n#ACT\r\nDelayGoto 1 @MakeMercenarymibao\r\nBREAK\r\n#elseact\r\nMESSAGEBOX 错误提示：你输入的【密保答案1】错误.连续错误3次将30分钟内禁止在次输入\r\nMOV S2 \r\nints 0 + 600\r\nBREAK\r\n\r\n\r\n[@验证3]\r\n#if\r\nEQUAL S2 \r\n#act\r\nMESSAGEBOX 错误提示：请先输入【密保答案1】\r\nBREAK\r\n\r\n#if\r\n!CompVal <$QUIZ2> = <$STR(S3)>\r\n#act\r\nQueryValue 3 0 20 ~MMBB3 请输入你的【密保问题2】：\r\n#elseact\r\nMESSAGEBOX 错误提示：当前输入的密保是正确的无需在次输入\r\nBREAK\r\n\r\n\r\n[~MMBB3]\r\n#if\r\ncompval $INTS0 < 3300\r\ncompval $INTS0 > 1500\r\n#act\r\nMOV D1 <$UNIXTIME>\r\nINC D1 <$INTS0>\r\nints 0 = <$STR(D1)>\r\n\r\n#if\r\ncompval $INTS0 > 3300\r\ncompval $INTS0 > $UNIXTIME\r\n#act\r\ncalcval $INTS0 - $UNIXTIME\r\nsendmsg 7 将在<$CALCRESULT>秒后,才可以再次输入\r\ncalcval $INTS0 ~ 0\r\nsendmsg 7 将在<$CALCRESULT>过期,才可以再次输入\r\nMOV S3 \r\nBREAK\r\n\r\n#if\r\ncompval $INTS0 > 3300\r\ncompval $INTS0 < $UNIXTIME\r\n#act\r\nints 0 = 0\r\n\r\n#IF\r\nCompVal <$QUIZ2> = <$STR(S3)>\r\n#ACT\r\nDelayGoto 1 @MakeMercenarymibao\r\nBREAK\r\n#elseact\r\nMESSAGEBOX 错误提示：你输入的【密保问题2】错误.连续错误3次将30分钟内禁止在次输入\r\nMOV S3 \r\nints 0 + 600\r\nBREAK\r\n\r\n\r\n[@验证4]\r\n#if\r\nEQUAL S3 \r\n#act\r\nMESSAGEBOX 错误提示：请先输入【密保问题2】\r\nBREAK\r\n#elseact\r\nQueryValue 4 0 20 ~MMBB4 请输入你的【密保答案2】：\r\nBREAK\r\n\r\n\r\n[~MMBB4]\r\n#if\r\ncompval $INTS0 < 3300\r\ncompval $INTS0 > 1500\r\n#act\r\nMOV D1 <$UNIXTIME>\r\nINC D1 <$INTS0>\r\nints 0 = <$STR(D1)>\r\n\r\n#if\r\ncompval $INTS0 > 3300\r\ncompval $INTS0 > $UNIXTIME\r\n#act\r\ncalcval $INTS0 - $UNIXTIME\r\nsendmsg 7 将在<$CALCRESULT>秒后,才可以再次输入\r\ncalcval $INTS0 ~ 0\r\nsendmsg 7 将在<$CALCRESULT>过期,才可以再次输入\r\nMOV S4 \r\nBREAK\r\n\r\n#if\r\ncompval $INTS0 > 3300\r\ncompval $INTS0 < $UNIXTIME\r\n#act\r\nints 0 = 0\r\n\r\n#IF\r\nCompVal <$QUIZ> = <$STR(S1)>\r\nCompVal <$ANSWER> = <$STR(S2)>\r\nCompVal <$QUIZ2> = <$STR(S3)>\r\nCompVal <$ANSWER2> = <$STR(S4)>\r\n#ACT\r\nDelayGoto 1 @MakeMercenarymibao\r\n;参数1表示不小于此等级的角色可以被压卡,\r\n;参数2表示制作为佣兵将损失15%的经验值\r\n;参数3为1时表示过滤掉隐藏角色\r\nQUERYMERCENARIES 5 1 0\r\n#elseact\r\nMESSAGEBOX 错误提示：你输入的【密保答案2】错误.连续错误3次将30分钟内禁止在次输入\r\nMOV S4 \r\nints 0 + 600\r\nBREAK\r\n\r\n \r\n\r\n \r\n\r\n \r\n\r\n \r\n\r\n; ------------------- 制作契约,需两步验证 -------------------\r\n[@MakeMercenaryWithTotpAuth]\r\n#if\r\ntrue\r\n#act\r\n; 请求两步验证的Key($TOTPKEY),成功后将回调@OnGetTotpKey标签\r\nGetTotpKey\r\n\r\n[@OnGetTotpKey]\r\n#if\r\nCompVal <$TOTPKEY>  = \"\"\r\n#say\r\n你的账号未绑定两步验证,暂不能制作佣兵契约！\\ \\\r\n<绑定两步验证/@@totp>   <返回/@main>\\ \\\r\n<关闭/@exit>\r\n#elseact\r\nQueryValue 5 1 6 ~VerifyKeyCode 请输入绑定当前账号的两步验证码：\r\n\r\n[~VerifyKeyCode]\r\n#if\r\n; 验证两步验证码是否正确\r\nVerifyKeyCode <$TOTPKEY> <$STR(M5)>\r\n#act\r\nQUERYMERCENARIES 5 1 0\r\n#elsesay\r\n输入的两步验证码不正确,不能制作佣兵契约！\\ \\\r\n<关闭/@exit>\r\n\r\n; ------------------- 制作契约,无需验证 -------------------\r\n[@MakeMercenary]\r\n#if\r\ntrue\r\n#act\r\n;参数1表示不小于此等级的角色可以被压卡,\r\n;参数2表示制作为佣兵将损失15%的经验值\r\n;参数3为1时表示过滤掉隐藏角色\r\nQUERYMERCENARIES 5 1 0\r\n\r\n\r\n; ------------------- 契约创建角色 -------------------\r\n[@@CreateMercenary]\r\n请放上已签约的佣兵契约纸。\\ \\\r\n<返回/@main>   <关闭/@exit>\r\n\r\n\r\n; ------------------- 压卡成功后执行的字段 -------------------\r\n[@MakeMercenarySuccess]\r\n#if\r\ntrue\r\n#act\r\nsendmsg 7 压卡成功：正在制作契约的玩家：<$PARAMSTR(0)>,被制作为契约的原角色名：<$PARAMSTR(1)>,佣兵契约上的角色名：<$PARAMSTR(2)>\r\n;StringsReplace 需要重命名的角色文本.txt <$PARAMSTR(1)> <$PARAMSTR(2)> 0 HardDisk\r\n\r\n;使用契约纸成功创建角色后执行的字段\r\n[@NewCharByContractSuccess]\r\n#if\r\ntrue\r\n#act\r\nsendmsg 7 创角成功：正在使用契约创角的玩家：<$PARAMSTR(0)>,佣兵契约上的角色名：<$PARAMSTR(1)>,创建的新角色名：<$PARAMSTR(2)>\r\n;StringsReplace 需要重命名的角色文本.txt <$PARAMSTR(1)> <$PARAMSTR(2)> 0 HardDisk\r\n");

                    cd = new CompletionData("假人系统脚本", "获取假人系统脚本");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("假人系统脚本", "假人的移动攻击速度其实就是英雄的速度和智能AI\r\n改进英雄组合速度,开启可以细调英雄各种组合动作 让假人更加真实智能\r\n\r\n相关命令：\r\n设置!setup SearchTargetWhenIdle=1自动打怪空闲时随机搜寻,否则停留原地等待刷怪\r\n\r\n设置随机释放连击NPC命令：RANDSERIESSKILL\r\n\r\n添加NPC命令：PetPickupItemRange 宠物名字 范围(0-15)  \r\n      宠物自动拾取范围,宠物名字为“H”时指英雄,宠物名字为“Self”时指自己（挂机用）,“*”指非英雄的所有宠物,范围0时关闭自动拾取,可结合上条更新进行定时。\r\n\r\n添加NPC命令：Lockdown 1  // 锁定,不可攻击、移动、魔法等,Lockdown 0是解锁\r\n\r\n添加NPC命令：FakePlayer 0/1 // 0踢假人,1加载假人\r\n\r\n添加NPC命令：GuardPos x y 范围  // 设置假人的守护点和范围（不小于Setup中的maxViewRangeX）,x为-1时取消守护\r\n   例子：\r\n   ;随机地图点守护\r\n   MAP $CURRENTMAP\r\n   GuardPos $CURRENTX $CURRENTY 30\r\n\r\n增加BotNoAttackList.txt,设置挂机假人不攻击列表,格式：怪物名 1 // 1挂机假人不攻击\r\n假人可以触发：[@PickupItem]、[@HeroLogin]\r\n\r\nM2 BotAutoWearEquipments.txt // 简单的假人拾取后自动穿戴列表,首次运行后查看文本说明\r\n;假人、假人的英雄拾取后自动穿戴列表,格式：职业(0战士 1法师 2道士) 装备名 权重\r\n;其中的权重为非0数字,拾取的装备穿戴权重比身上的大时,会自动穿上,例子：\r\n0 新手铁剑 1\r\n2 新手半月 1\r\n1 新手海魂 1\r\n0 战技之刃 2\r\n1 魔法之刃 2\r\n2 道术之刃 2\r\n0 凝霜 3\r\n2 咒术降魔 3\r\n1 聚法偃月 3\r\n0 精钢斩马刀 4\r\n1 魔杖 4\r\n2 银蛇 4\r\n0 附灵魔杖 5\r\n1 精良银蛇 5\r\n2 敏锐魔杖 5\r\n0 井中月 6\r\n2 无极棍 6\r\n1 骨玉权杖 6\r\n");

                    cd = new CompletionData("元宝转账脚本", "获取元宝转账脚本");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("元宝转账脚本", "(@@dealgold)\r\n[@main]\r\n<进行元宝转帐/@dealgamegold>\\\r\n<关闭/@exit> \r\n\r\n[@dealgamegold]\r\n#IF\r\n#ACT\r\nSTARTTAKEGOLD \r\n\r\n[@startdealgold]\r\n#IF\r\n#SAY\r\n<$DEALGOLDPLAY>与你交易,请<输入转帐/@@dealgold>数量\\\r\n<返回/@main> \r\n\r\n[@dealgoldpost]\r\n对不起,需要交易的人没站好位置！\\\r\n<返回/@main> \r\n\r\n[@dealgoldFail]\r\n对不起,你的元宝数量没有这么多！\\\r\n<返回/@exit> \r\n\r\n[@dealgoldInputFail]\r\n非法操作,交易失败！ \r\n\r\n[@dealgoldPlayError]\r\n转帐失败！ \r\n");

                    cd = new CompletionData("创建英雄脚本", "获取创建英雄脚本");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("创建英雄脚本", "(@@BuHero)\r\n[@main]\r\n创建英雄：<英雄取名/@@buHero>\\ \\\r\n删除英雄：<删除英雄/@DelMyHero>\\ \\\r\n<退出/@exit> \r\n\r\n[@@buHero]\r\n#ACT\r\nDELAYCALL 10 ~$CREATEHERO \r\n\r\n[~$CREATEHERO]\r\n//CREATEHERO 职业 性别\r\n请创建英雄：\\ \\\r\n<男战士/@CREATEHERO_WARR_MAN> <女战士/@CREATEHERO_WARR_WOM>\\\r\n<男法师/@CREATEHERO_WIZA_MAN> <女法师/@CREATEHERO_WIZA_WOM>\\\r\n<男道士/@CREATEHERO_TAOS_MAN> <女道士/@CREATEHERO_TAOS_WOM>\\ \\\r\n<关闭/@exit>\r\n[@CREATEHERO_WARR_MAN]\r\n#ACT\r\nCREATEHERO 0 0\r\n[@CREATEHERO_WARR_WOM]\r\n#ACT\r\nCREATEHERO 0 1\r\n[@CREATEHERO_WIZA_MAN]\r\n#ACT\r\nCREATEHERO 1 0\r\n[@CREATEHERO_WIZA_WOM]\r\n#ACT\r\nCREATEHERO 1 1\r\n[@CREATEHERO_TAOS_MAN]\r\n#ACT\r\nCREATEHERO 2 0\r\n[@CREATEHERO_TAOS_WOM]\r\n#ACT\r\nCREATEHERO 2 1 \r\n\r\n//返回消息\r\n[@CreateingHero]\r\n系统正在接受申请,请稍候……\\ \\\r\n<关闭/@exit> \r\n\r\n[@HaveHero]\r\n您已经有英雄了。\\ \\\r\n<关闭/@exit> \r\n\r\n[@SetHeroName]\r\n请先给您的英雄取名字。\\ \\\r\n<关闭/@exit> \r\n\r\n[@HaveHero]\r\n您已经有英雄了。\\ \\\r\n<关闭/@exit> \r\n\r\n[@DelMyHero]\r\n#ACT\r\nDELETEHERO\r\n//返回消息\r\n[@NotHaveHero]\r\n你没有英雄。\\ \\\r\n<关闭/@exit>\r\n[@LogOutHeroFirst]\r\n请将英雄设置下线！\\ \\\r\n<关闭/@exit>\r\n[@DeleteHeroOK]\r\n删除英雄成功。\\ \\\r\n<关闭/@exit \r\n\r\n注意：QFunction 添加如下设置\r\n\r\n\r\n[@@RECALLPLAYER]\r\n#IF\r\nCHECKHEROONLINE\r\n#ACT\r\nCLOSE\r\nMESSAGEBOX 请先设置您的英雄下线！\r\n#ELSEACT\r\nCLOSE\r\nRECALLPLAYER \r\n\r\n[@HERONAMEFILTER]\r\n英雄名字中包含禁用字符\\<关闭/@EXIT> \r\n\r\n[@CREATEHEROOK]\r\n#IF\r\n#ACT\r\nGIVE 火龙之心 1\r\nSENDMSG 0 恭喜:玩家「<$USERNAME>」成功带领英雄。\r\n#SAY\r\n创建英雄成功\\<关闭/@EXIT>\r\n[@HERONAMEEXISTS]\r\n英雄名字已经存在\\<关闭/@EXIT>\r\n[@HEROOVERCHRCOUNT]\r\n你的帐号角色过多\\<关闭/@EXIT>\r\n[@CREATEHEROFAIL]\r\n创建英雄失败\\<关闭/@EXIT>\r\n[@CREATEHEROFAILEX]\r\n创建英雄失败,请稍候重试\\<关闭/@EXIT> \r\n");

                    cd = new CompletionData("坐标记录脚本", "获取坐标记录脚本");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("坐标记录脚本", "[@TagMapInfo] \r\n当前记录地图信息：\\\r\n一： <记录当前坐标/@TAGMAPINFO1> <移动到/@PMOVE1> -> <$TAGMAPNAME1> <$TAGX1> <$TAGY1>\\\r\n二： <记录当前坐标/@TAGMAPINFO2> <移动到/@PMOVE2> -> <$TAGMAPNAME2> <$TAGX2> <$TAGY2>\\\r\n三： <记录当前坐标/@TAGMAPINFO3> <移动到/@PMOVE3> -> <$TAGMAPNAME3> <$TAGX3> <$TAGY3>\\\r\n四： <记录当前坐标/@TAGMAPINFO4> <移动到/@PMOVE4> -> <$TAGMAPNAME4> <$TAGX4> <$TAGY4>\\\r\n五： <记录当前坐标/@TAGMAPINFO5> <移动到/@PMOVE5> -> <$TAGMAPNAME5> <$TAGX5> <$TAGY5>\\\r\n六： <记录当前坐标/@TAGMAPINFO6> <移动到/@PMOVE6> -> <$TAGMAPNAME6> <$TAGX6> <$TAGY6>\\\r\n\u007f <关闭/@exit>\r\n[@PMOVE1]\r\n#ACT\r\nTAGMAPMOVE 1\r\n[@PMOVE2]\r\n#ACT\r\nTAGMAPMOVE 2\r\n[@PMOVE3]\r\n#ACT\r\nTAGMAPMOVE 3\r\n[@PMOVE4]\r\n#ACT\r\nTAGMAPMOVE 4\r\n[@PMOVE5]\r\n#ACT\r\nTAGMAPMOVE 5\r\n[@PMOVE6]\r\n#ACT\r\nTAGMAPMOVE 6\r\n[@TAGMAPINFO1]\r\n#ACT\r\nTAGMAPINFO 1\r\nDELAYGOTO 100 @TagMapInfo\r\n[@TAGMAPINFO2]\r\n#ACT\r\nTAGMAPINFO 2\r\nDELAYGOTO 100 @TagMapInfo\r\n[@TAGMAPINFO3]\r\n#ACT\r\nTAGMAPINFO 3\r\nDELAYGOTO 100 @TagMapInfo\r\n[@TAGMAPINFO4]\r\n#ACT\r\nTAGMAPINFO 4\r\nDELAYGOTO 100 @TagMapInfo\r\n[@TAGMAPINFO5]\r\n#ACT\r\nTAGMAPINFO 5\r\nDELAYGOTO 100 @TagMapInfo\r\n[@TAGMAPINFO6]\r\n#ACT\r\nTAGMAPINFO 6\r\nDELAYGOTO 100 @TagMapInfo");

                    cd = new CompletionData("拜师收徒脚本", "获取拜师收徒脚本");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("拜师收徒脚本", "(@s_repair)\r\n%100\r\n+30\r\n+25\r\n[@main]\r\n你想做什么？\\\r\n<我要拜师/@拜师> <了解拜师系统相关知识/@了解>\\\r\n<查询声望点数/@查询声望>\\\r\n<解除师徒关系/@解除师徒关系>\\\r\n<领取荣誉勋章/@领取荣誉勋章>\\\r\n<修理荣誉勋章/@s_repair>\\\r\n<离开/@exit>\r\n\r\n\r\n[@查询声望]\r\n<$USERNAME>您好！您现在的<声望点数>是：<$CREDITPOINT>\\ \\\r\n<返回/@main>\\\r\n\r\n\r\n[@领取荣誉勋章]\r\n荣誉勋章是表彰着勇士们的荣誉,只要你的声望足够多\\\r\n就可以到我这里来兑换荣誉勋章,我这里分为两种方式\\\\\r\n<领取随机勋章/@随机兑换> 随机领取将消耗一定数量的声望值\\\r\n<领取指定勋章/@指定兑换> 指定领取将消耗一倍数量的声望值\\\\\r\n<返回/@main>\\ \r\n\r\n[@随机兑换]\r\n勋章是随机配的,请考虑清楚\\ \\\r\n<领取一级荣誉勋章/@Random1> 需要花费10点声望,佩戴需要5点声望\\\r\n<领取二级荣誉勋章/@Random2> 需要花费20点声望,佩戴需要10点声望\\\r\n<领取三级荣誉勋章/@Random3> 需要花费30点声望,佩戴需要15点声望\\\r\n<领取四级荣誉勋章/@Random4> 需要花费40点声望,佩戴需要20点声望\\ \\\r\n<返回/@领取荣誉勋章> \r\n\r\n[@Random1]\r\n#IF\r\nCHECKCREDITPOINT ? 10\r\n#ACT\r\nCREDITPOINT - 10\r\nReadRandomLine .\\QuestDiary\\荣誉勋章\\1级荣誉勋章.txt S15\r\nGIVE <$STR(S15)> 1\r\n#SAY\r\n你已获得了荣誉的象征！快走吧！\\\r\n<返回/@随机兑换>\\\r\n#ELSESAY\r\n请检查领取荣誉勋章所需要的声望值！\\\r\n<返回/@随机兑换>\\ \r\n\r\n[@Random2]\r\n#IF\r\nCHECKCREDITPOINT ? 20\r\n#ACT\r\nCREDITPOINT - 20\r\nReadRandomLine .\\QuestDiary\\荣誉勋章\\2级荣誉勋章.txt S15\r\nGIVE <$STR(S15)> 1\r\n#SAY\r\n你已获得了荣誉的象征！快走吧！\\\r\n<返回/@随机兑换>\\\r\n#ELSESAY\r\n请检查领取荣誉勋章所需要的声望值！\\\r\n<返回/@随机兑换>\\ \r\n\r\n[@Random3]\r\n#IF\r\nCHECKCREDITPOINT ? 30\r\n#ACT\r\nCREDITPOINT - 30\r\nReadRandomLine .\\QuestDiary\\荣誉勋章\\3级荣誉勋章.txt S15\r\nGIVE <$STR(S15)> 1\r\n#SAY\r\n你已获得了荣誉的象征！快走吧！\\\r\n<返回/@随机兑换>\\\r\n#ELSESAY\r\n请检查领取荣誉勋章所需要的声望值！\\\r\n<返回/@随机兑换>\\ \r\n\r\n[@Random4]\r\n#IF\r\nCHECKCREDITPOINT ? 40\r\n#ACT\r\nCREDITPOINT - 40\r\nReadRandomLine .\\QuestDiary\\荣誉勋章\\4级荣誉勋章.txt S15\r\nGIVE <$STR(S15)> 1\r\n#SAY\r\n你已获得了荣誉的象征！快走吧！\\\r\n<返回/@随机兑换>\\\r\n#ELSESAY\r\n请检查领取荣誉勋章所需要的声望值！\\\r\n<返回/@随机兑换>\\\r\n\r\n\r\n[@指定兑换]\r\n你可以指定所需要的勋章,但是会花费更多的声望值\\ \\\r\n<领取一级荣誉勋章/@荣誉勋章10> 需要花费20点声望,佩戴需要5点声望\\\r\n<领取二级荣誉勋章/@荣誉勋章20> 需要花费40点声望,佩戴需要10点声望\\\r\n<领取三级荣誉勋章/@荣誉勋章30> 需要花费60点声望,佩戴需要15点声望\\\r\n<领取四级荣誉勋章/@荣誉勋章40> 需要花费80点声望,佩戴需要20点声望\\ \\\r\n<返回/@领取荣誉勋章> \r\n\r\n[@荣誉勋章10]\r\n<领取11号荣誉勋章/@荣誉勋章11>\\\r\n<领取12号荣誉勋章/@荣誉勋章12>\\\r\n<领取13号荣誉勋章/@荣誉勋章13>\\\r\n<领取14号荣誉勋章/@荣誉勋章14>\\\r\n<领取15号荣誉勋章/@荣誉勋章15>\\\r\n<返回/@领取荣誉勋章> \r\n\r\n[@荣誉勋章20]\r\n<领取21号荣誉勋章/@荣誉勋章21>\\\r\n<领取22号荣誉勋章/@荣誉勋章22>\\\r\n<领取23号荣誉勋章/@荣誉勋章23>\\\r\n<领取24号荣誉勋章/@荣誉勋章24>\\\r\n<领取25号荣誉勋章/@荣誉勋章25>\\\r\n<返回/@领取荣誉勋章> \r\n\r\n[@荣誉勋章30]\r\n<领取31号荣誉勋章/@荣誉勋章31>\\\r\n<领取32号荣誉勋章/@荣誉勋章32>\\\r\n<领取33号荣誉勋章/@荣誉勋章33>\\\r\n<领取34号荣誉勋章/@荣誉勋章34>\\\r\n<领取35号荣誉勋章/@荣誉勋章35>\\\r\n<返回/@领取荣誉勋章> \r\n\r\n[@荣誉勋章40]\r\n<领取41号荣誉勋章/@荣誉勋章41>\\\r\n<领取42号荣誉勋章/@荣誉勋章42>\\\r\n<领取43号荣誉勋章/@荣誉勋章43>\\\r\n<领取44号荣誉勋章/@荣誉勋章44>\\\r\n<领取45号荣誉勋章/@荣誉勋章45>\\\r\n<返回/@领取荣誉勋章> \r\n\r\n[@荣誉勋章11]\r\n#IF\r\nCHECKCREDITPOINT ? 20\r\n#ACT\r\nCREDITPOINT - 20\r\ngive 荣誉勋章11号\r\n#SAY\r\n你已获得了荣誉的象征！快走吧！\\\r\n<返回/@指定兑换>\r\n#ELSESAY\r\n请检查领取荣誉勋章所需要的声望值！\\\r\n<返回/@指定兑换>\\ \r\n\r\n[@荣誉勋章12]\r\n#IF\r\nCHECKCREDITPOINT ? 20\r\n#ACT\r\nCREDITPOINT - 20\r\ngive 荣誉勋章12号 1\r\n#SAY\r\n你已获得了荣誉的象征！快走吧！\\\r\n<返回/@指定兑换>\r\n#ELSESAY\r\n请检查领取荣誉勋章所需要的声望值！\\\r\n<返回/@指定兑换>\\ \r\n\r\n[@荣誉勋章13]\r\n#IF\r\nCHECKCREDITPOINT ? 20\r\n#ACT\r\nCREDITPOINT - 20\r\ngive 荣誉勋章13号 1\r\n#SAY\r\n你已获得了荣誉的象征！快走吧！\\\r\n<返回/@指定兑换>\r\n#ELSESAY\r\n请检查领取荣誉勋章所需要的声望值！\\\r\n<返回/@指定兑换>\\ \r\n\r\n[@荣誉勋章14]\r\n#IF\r\nCHECKCREDITPOINT ? 20\r\n#ACT\r\nCREDITPOINT - 20\r\ngive 荣誉勋章14号 1\r\n#SAY\r\n你已获得了荣誉的象征！快走吧！\\\r\n<返回/@指定兑换>\r\n#ELSESAY\r\n请检查领取荣誉勋章所需要的声望值！\\\r\n<返回/@指定兑换>\\ \r\n\r\n[@荣誉勋章15]\r\n#IF\r\nCHECKCREDITPOINT ? 20\r\n#ACT\r\nCREDITPOINT - 20\r\ngive 荣誉勋章15号 1\r\n#SAY\r\n你已获得了荣誉的象征！快走吧！\\\r\n<返回/@指定兑换>\r\n#ELSESAY\r\n请检查领取荣誉勋章所需要的声望值！\\\r\n<返回/@指定兑换>\\ \r\n\r\n[@荣誉勋章21]\r\n#IF\r\nCHECKCREDITPOINT ? 40\r\n#ACT\r\nCREDITPOINT - 40\r\ngive 荣誉勋章21号 1\r\n#SAY\r\n你已获得了荣誉的象征！快走吧！\\\r\n<返回/@指定兑换>\r\n#ELSESAY\r\n请检查领取荣誉勋章所需要的声望值！\\\r\n<返回/@指定兑换>\\ \r\n\r\n[@荣誉勋章22]\r\n#IF\r\nCHECKCREDITPOINT ? 40\r\n#ACT\r\nCREDITPOINT - 40\r\ngive 荣誉勋章22号 1\r\n#SAY\r\n你已获得了荣誉的象征！快走吧！\\\r\n<返回/@指定兑换>\r\n#ELSESAY\r\n请检查领取荣誉勋章所需要的声望值！\\\r\n<返回/@指定兑换>\\ \r\n\r\n[@荣誉勋章23]\r\n#IF\r\nCHECKCREDITPOINT ? 40\r\n#ACT\r\nCREDITPOINT - 40\r\ngive 荣誉勋章23号 1\r\n#SAY\r\n你已获得了荣誉的象征！快走吧！\\\r\n<返回/@指定兑换>\r\n#ELSESAY\r\n请检查领取荣誉勋章所需要的声望值！\\\r\n<返回/@指定兑换>\\ \r\n\r\n[@荣誉勋章24]\r\n#IF\r\nCHECKCREDITPOINT ? 40\r\n#ACT\r\nCREDITPOINT - 40\r\ngive 荣誉勋章24号 1\r\n#SAY\r\n你已获得了荣誉的象征！快走吧！\\\r\n<返回/@指定兑换>\r\n#ELSESAY\r\n请检查领取荣誉勋章所需要的声望值！\\\r\n<返回/@指定兑换>\\ \r\n\r\n[@荣誉勋章25]\r\n#IF\r\nCHECKCREDITPOINT ? 40\r\n#ACT\r\nCREDITPOINT - 40\r\ngive 荣誉勋章25号 1\r\n#SAY\r\n你已获得了荣誉的象征！快走吧！\\\r\n<返回/@指定兑换>\r\n#ELSESAY\r\n请检查领取荣誉勋章所需要的声望值！\\\r\n<返回/@指定兑换>\\ \r\n\r\n[@荣誉勋章31]\r\n#IF\r\nCHECKCREDITPOINT ? 60\r\n#ACT\r\nCREDITPOINT - 60\r\ngive 荣誉勋章31号 1\r\n#SAY\r\n你已获得了荣誉的象征！快走吧！\\\r\n<返回/@指定兑换>\r\n#ELSESAY\r\n请检查领取荣誉勋章所需要的声望值！\\\r\n<返回/@指定兑换>\\ \r\n\r\n[@荣誉勋章32]\r\n#IF\r\nCHECKCREDITPOINT ? 60\r\n#ACT\r\nCREDITPOINT - 60\r\ngive 荣誉勋章32号 1\r\n#SAY\r\n你已获得了荣誉的象征！快走吧！\\\r\n<返回/@指定兑换>\r\n#ELSESAY\r\n请检查领取荣誉勋章所需要的声望值！\\\r\n<返回/@指定兑换>\\ \r\n\r\n[@荣誉勋章33]\r\n#IF\r\nCHECKCREDITPOINT ? 60\r\n#ACT\r\nCREDITPOINT - 60\r\ngive 荣誉勋章33号 1\r\n#SAY\r\n你已获得了荣誉的象征！快走吧！\\\r\n<返回/@指定兑换>\r\n#ELSESAY\r\n请检查领取荣誉勋章所需要的声望值！\\\r\n<返回/@指定兑换>\\ \r\n\r\n[@荣誉勋章34]\r\n#IF\r\nCHECKCREDITPOINT ? 60\r\n#ACT\r\nCREDITPOINT - 60\r\ngive 荣誉勋章34号 1\r\n#SAY\r\n你已获得了荣誉的象征！快走吧！\\\r\n<返回/@指定兑换>\r\n#ELSESAY\r\n请检查领取荣誉勋章所需要的声望值！\\\r\n<返回/@指定兑换>\\ \r\n\r\n[@荣誉勋章35]\r\n#IF\r\nCHECKCREDITPOINT ? 60\r\n#ACT\r\nCREDITPOINT - 60\r\ngive 荣誉勋章35号 1\r\n#SAY\r\n你已获得了荣誉的象征！快走吧！\\\r\n<返回/@指定兑换>\r\n#ELSESAY\r\n请检查领取荣誉勋章所需要的声望值！\\\r\n<返回/@指定兑换>\\ \r\n\r\n[@荣誉勋章41]\r\n#IF\r\nCHECKCREDITPOINT ? 80\r\n#ACT\r\nCREDITPOINT - 80\r\ngive 荣誉勋章41号 1\r\n#SAY\r\n你已获得了荣誉的象征！快走吧！\\\r\n<返回/@指定兑换>\r\n#ELSESAY\r\n请检查领取荣誉勋章所需要的声望值！\\\r\n<返回/@指定兑换>\\ \r\n\r\n[@荣誉勋章42]\r\n#IF\r\nCHECKCREDITPOINT ? 80\r\n#ACT\r\nCREDITPOINT - 80\r\ngive 荣誉勋章42号 1\r\n#SAY\r\n你已获得了荣誉的象征！快走吧！\\\r\n<返回/@指定兑换>\r\n#ELSESAY\r\n请检查领取荣誉勋章所需要的声望值！\\\r\n<返回/@指定兑换>\\ \r\n\r\n[@荣誉勋章43]\r\n#IF\r\nCHECKCREDITPOINT ? 80\r\n#ACT\r\nCREDITPOINT - 80\r\ngive 荣誉勋章43号 1\r\n#SAY\r\n你已获得了荣誉的象征！快走吧！\\\r\n<返回/@指定兑换>\r\n#ELSESAY\r\n请检查领取荣誉勋章所需要的声望值！\\\r\n<返回/@指定兑换>\\ \r\n\r\n[@荣誉勋章44]\r\n#IF\r\nCHECKCREDITPOINT ? 80\r\n#ACT\r\nCREDITPOINT - 80\r\ngive 荣誉勋章44号 1\r\n#SAY\r\n你已获得了荣誉的象征！快走吧！\\\r\n<返回/@指定兑换>\r\n#ELSESAY\r\n请检查领取荣誉勋章所需要的声望值！\\\r\n<返回/@指定兑换>\\ \r\n\r\n[@荣誉勋章45]\r\n#IF\r\nCHECKCREDITPOINT ? 80\r\n#ACT\r\nCREDITPOINT - 80\r\ngive 荣誉勋章45号 1\r\n#SAY\r\n你已获得了荣誉的象征！快走吧！\\\r\n<返回/@指定兑换>\r\n#ELSESAY\r\n请检查领取荣誉勋章所需要的声望值！\\\r\n<返回/@指定兑换>\\ \r\n\r\n[@s_repair]\r\n你可以在这里修理勋章！\\ \\ \\\r\n<返 回/@Main>\r\n\r\n\r\n[@了解]\r\n师父 --人物等级到达35级,就可以收徒弟了！\\\r\n徒弟 --人物等级必须在20级以下才能做别人的徒弟！\\\r\n出师 --徒弟等级到达35级,徒弟自动出师！\\\r\n收徒 --双方必须面对面！由徒弟点我要拜师即可\\\r\n好处 --徒弟出师后师父会自动获得5点声望！\\\r\n声望 --声望可以兑换荣誉勋章,声望上限255点。\\\r\n命令 ：@允许师徒传送 @师徒传送\\\r\n<返回/@MAIN>\\ \r\n\r\n[@拜师]\r\n#IF\r\n#ACT\r\nQUERYVALUE 2 0 14 @检测师傅 请输入你拜师对象角色名称： \r\n\r\n[@检测师傅]\r\n#IF\r\nEQUAL S2 <$USERNAME>\r\n#ACT\r\nMessageBox 不能对自己发送拜师请求。\r\nBREAK \r\n\r\n#IF\r\nCHECKMASTER\r\n#ACT\r\nMessageBox 你都已经拜了别人为师,怎么还拜师。\r\nBREAK \r\n\r\n#IF\r\nCHECKISMASTER\r\n#ACT\r\nMessageBox 你都已经是别人的师傅了,没事别来这玩。\r\nBREAK \r\n\r\n#IF\r\n<$STR(S2)>.CHECKMASTER\r\n#ACT\r\nMessageBox 你找了个什么人做师父,怎么现在还是别人的徒弟？\r\nBREAK \r\n\r\n#IF\r\nCHECKLEVELEX ? 20\r\n#ACT\r\nMessageBox 你都<$LEVEL>级了还要找师父？\r\nBREAK \r\n\r\n#IF\r\n<$STR(S2)>.CHECKLEVELEX < 35\r\n#ACT\r\nMessageBox 你输入的拜师对象等级太低,怎么当你的师傅？\r\nBREAK \r\n\r\n#IF\r\nCHECKONLINE <$STR(S2)>\r\n#ELSEACT\r\nMessageBox <$STR(S2)>\\不在线,你不能拜师！\r\nBREAK \r\n\r\n#IF\r\n<$STR(S2)>.ISONMAP 3\r\n#ACT\r\nMASTER\r\n#ELSEACT\r\nMessageBox 你输入的拜师对象不在当前地图,怎么拜师？\r\nBREAK \r\n\r\n[@STARTGETMASTER]\r\n拜师仪式正式开始。\\ \\\r\n你是否确认拜师？\\ \\\r\n<确认/@REQUESTMASTER> \r\n\r\n[@STARTMASTER]\r\n拜师仪式正式开始。\\ \\\r\n对方已经向你提出拜师请求。\\ \\ \r\n\r\n[@REQUESTMASTER]\r\n#IF\r\n#ACT\r\nMASTER REQUESTMASTER\r\nCLOSE \r\n\r\n[@WATEMASTER]\r\n#IF\r\n#ACT\r\nSENDMSG 2 %S,面对着自己仰慕已久的英雄：<$STR(S2)>,深深鞠了一躬,请求对方收自己为徒! \r\n\r\n[@REVMASTER]\r\n#IF\r\n#ACT\r\nSENDMSG 2 %S,对方想拜你为师,你是否想收此人为徒？\r\n#SAY\r\n对方想拜你为师,你是否想收此人为徒？ \\ \\\r\n<同意/@RESPOSEMASTER>\\\r\n<不同意/@RESPOSEMASTERFAIL> \r\n\r\n[@RESPOSEMASTER]\r\n#IF\r\n#ACT\r\nMASTER RESPONSEMASTER OK\r\nCLOSE\r\n[@RESPOSEMASTERFAIL]\r\n#IF\r\n#ACT\r\nMASTER RESPONSEMASTER FAIL \r\n\r\n[@ENDMASTER]\r\n#IF\r\n#ACT\r\nSENDMSG 0 %S,完成了拜师收徒仪式！ \r\n\r\n[@ENDMASTERFAIL]\r\n拜师失败！\\ \\\r\n<关闭/@EXIT> \r\n\r\n[@MASTERDIRERR]\r\n对方没站好位置 \r\n\r\n[@MASTERCHECKDIR]\r\n请面对面站好位置\\\\ \r\n\r\n[@HUMANTYPEERR]\r\n此人不可以做你的师父。\r\n\r\n\r\n[@解除师徒关系] \r\n#if \r\nhavemaster \r\n#act \r\nunmaster \r\n#ELSESAY \r\n你都没师父,跑来做什么？？\\ \\ \r\n<返回/@main> \r\n\r\n\r\n;======================================================= \r\n\r\n[@UnMasterCheckDir] \r\n按正常出师步骤,必须二个人对面对站好位置,\\ \r\n如果人来不了你只能选择强行出师了。\\ \\ \r\n<我要强行出师/@强行出师>\\ \r\n<返回/@main> \r\n;======================================================= \r\n;对面位置不是人物时显示的信息 \r\n[@UnMasterTypeErr] \r\n你对面站了个什么东西,怎么不太象人来的。\\ \\ \r\n<返回/@main> \r\n[@UnIsMaster] \r\n必须由徒弟发出请求！！！\\ \\ \r\n<返回/@main> \r\n[@UnMasterError] \r\n不要来捣乱！！！\\ \\ \r\n<返回/@main> \r\n;======================================================= \r\n;开始程序后,双方显示的信息 \r\n[@StartUnMaster] \r\n解除师徒仪式现在开始！！！\\ \\ \r\n是否确定真的要脱离师徒关系？\\ \\ \r\n<确定/@RequestUnMaster> \r\n[@WateUnMaster] \r\n解除师徒仪式现在开始！！！\\ \\ \r\n\r\n;======================================================= \r\n;发出请求 \r\n[@RequestUnMaster] \r\n#if \r\nhavemaster \r\n#act \r\nunmaster requestunmaster \r\n;======================================================= \r\n;回应请求 \r\n[@ResposeUnMaster] \r\n#if \r\nhavemaster \r\n#act \r\nunmaster responseunmaster \r\n\r\n;=============================================== \r\n;请求后显示的信息 \r\n[@WateUnMaster] \r\n你已向对方发出请求,请耐心等待对方的答复。 \r\n\r\n;=============================================== \r\n;请求后对方显示的信息 \r\n[@RevUnMaster] \r\n对方向你请求解除师徒关系,你是否答应？ \\ \\ \r\n<我愿意/@RequestUnMaster>\\\r\n<返回/@main> \r\n\r\n;=============================================== \r\n\r\n[@ExeMasterFail] \r\n你都没师父,跑来做什么？ \\ \\ \r\n[返回/@main] \r\n\r\n;============================================== \r\n\r\n[@强行出师]\r\n#IF\r\ncheckitem 金条 1\r\ncheckismaster\r\n#ACT\r\nTAKE 金条 1\r\nunmaster requestunmaster force \r\nSENDMSG 2 :%s,已经和他的『徒弟』强行脱离师徒关系！\r\nBREAK\r\n#IF\r\ncheckitem 金条 1\r\ncheckmaster\r\n#ACT\r\nTAKE 金条 1\r\nunmaster requestunmaster force \r\nSENDMSG 2 :%s,已经和他的『师父』强行脱离师徒关系！\r\n#ELSESAY \r\n要收一根金条的手续费,你没有金条。\\ \r\n<确定/@exit> \r\n\r\n[@UnMasterEnd] \r\n呵呵,你已经脱离师徒关系了！ \r\n");

                    cd = new CompletionData("寻宝人脚本", "获取寻宝人脚本");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("寻宝人脚本", "[@main]\r\n宝物之间有一定的灵犀存在,鉴宝师可以帮你分辨你的宝物到底是\\\r\n不是灵媒,如果是灵媒,你可以把它放在灵媒探索位,对,就是界\\\r\n面左下角那个框,用快截键<COLOR=clRed CTRL+X>就可以探寻宝物的存在。\\\r\n<购买铲子/@购买铲子> <如何寻宝/@如何寻宝>\\\r\n<如何挖宝/@如何挖宝> <关于灵媒/@关于灵媒>\\\r\n<回收除魔灵媒/@回收除魔灵媒>\\ \r\n\r\n[@回收除魔灵媒]\r\n你要是除魔灵媒太多,可以交给我,我免费回收。\\\r\n<COLOR=clRed 点击下面的按钮将回收包裹中所有的除魔灵媒,请确认。>\\\r\n<回收包裹中的所有除魔灵媒/@回收>\\ \r\n\r\n[@回收]\r\n你确定要回收掉<COLOR=clRed 包裹中所有的除魔灵媒吗？>\\ \\\r\n<确定/@确定>\\ \r\n\r\n[@确定]\r\n#IF\r\nCHECKITEM 除魔灵媒 1\r\n#ACT\r\nTAKE 除魔灵媒 999\r\nclose\r\n#ELSESAY\r\n您的背包中没有除魔灵媒啊！\\ \\ \r\n\r\n[@关于灵媒]\r\n通过鉴宝师的鉴定,任何宝物都有可能成为灵媒。每个灵媒都有自\\\r\n己的品质,品质越高的灵媒,越有可能寻找到好的宝物,使用最好\\\r\n的灵媒甚至可以挖到天龙装备和神戒！不过每次挖到一件宝物,灵\\\r\n媒的品质都会有所降低。每次使用灵媒会扣除灵媒一定的灵气值,\\\r\n灵媒的灵气值可以使用灵气神水来补充,灵气神水可以在商城购买\\\r\n得到。\\\r\n<返回/@MAIN>\\ \r\n\r\n[@如何挖宝]\r\n只有用我特制的新手铲、铁铲、金刚铲、乌金铲才能挖掘到宝物,\\\r\n用的铲子越好,挖掘范围则越大,挖掘速度也越快。\\ \\\r\n<购买铲子/@购买铲子>\\\r\n<返回/@MAIN>\\ \r\n\r\n[@如何寻宝]\r\n　　 要想寻找宝物,首先你必须有一个宝物灵媒,任何可鉴定的物\\\r\n品都可能是宝物灵媒,你要去鉴定师那里进行鉴定,如果你的某件\\\r\n装备被鉴定为宝物灵媒,你可以把它放在灵媒探索位,放好之后,\\\r\n按下<COLOR=clRed CTRL+X>快截键就可以使用灵媒了。\\\r\n　　在某些地图,例如 <COLOR=clRed 祖玛七层大厅、雷炎二层、宫殿迷宫、霸者>\\\r\n<COLOR=clRed 大厅、连接通道、石墓七层、尸魔洞三层、骨魔洞五层、沃玛寺庙>\\\r\n<COLOR=clRed 、行会遗迹地图>等都有大量宝物的存在,在这些地图中,灵媒更容\\\r\n易感应到宝物的存在。在以上地图中,当你使用灵媒的时候,灵媒\\\r\n<下一页/@下一页> <返回/@MAIN>\\ \r\n\r\n[@下一页]\r\n会给你指示宝物所在的方位,当你离很近的时候,灵媒会给你指示\\\r\n宝物所在的范围,你离宝物越近,灵媒给你的提示也会不同,你需\\\r\n要<COLOR=clRed 细心琢磨>,快速掌握挖宝的诀窍哦！\\\r\n<上一页/@如何寻宝> <返回/@MAIN>\\\r\n\r\n\r\n \r\n\r\n[@购买铲子]\r\n只有用我特制的新手铲、铁铲、金刚铲、乌金铲才能挖掘到宝物,\\\r\n用的铲子越好,挖掘范围则越大,挖掘速度也越快。\\\r\n<使用5000金币购买新手铲/@新手铲>\\\r\n<使用1元宝购买铁铲/@铁铲>\\\r\n<使用10元宝购买金刚铲/@金刚铲>\\\r\n<使用50元宝购买乌金铲/@乌金铲>\\\r\n<返回/@MAIN>\\ \r\n\r\n[@乌金铲]\r\n#IF\r\nCHECKGAMEGOLD > 49\r\n#ACT\r\nGAMEGOLD - 50\r\nGIVE 乌金铲 1\r\nclose\r\n#ELSESAY\r\n[失败]：您的元宝不足\\ \\\r\n<返回/@购买铲子>\\\r\n\r\n\r\n[@金刚铲]\r\n#IF\r\nCHECKGAMEGOLD > 9\r\n#ACT\r\nGAMEGOLD - 10\r\nGIVE 金刚铲 1\r\nclose\r\n#ELSESAY\r\n[失败]：您的元宝不足\\ \\\r\n<返回/@购买铲子>\\ \r\n\r\n[@铁铲]\r\n#IF\r\nCHECKGAMEGOLD > 0\r\n#ACT\r\nGAMEGOLD - 1\r\nGIVE 铁铲 1\r\nclose\r\n#ELSESAY\r\n[失败]：您的元宝不足\\ \\\r\n<返回/@购买铲子>\\ \r\n\r\n[@新手铲]\r\n#IF\r\nCHECKGOLD 5000\r\n#ACT\r\nTAKE 金币 5000\r\nGIVE 新手铲 1\r\nclose\r\n#ELSESAY\r\n购买新手铲需要5000金币,你没有足够的金币。\\ \\\r\n<返回/@购买铲子>\\ \r\n");

                    cd = new CompletionData("鉴定师脚本", "获取鉴定师脚本");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("鉴定师脚本", "[@main]\r\n你别看我满头苍髯,想当年,咱也是英姿飒爽,一剑光寒玛法动、\\\r\n万军之中自往来,现在毕竟年纪大了,我自从追随庄主走南闯北以\\\r\n来,对玛法大陆上的各种珍奇古玩产生了浓厚的兴趣。但愿我能帮\\\r\n到你。给我一件装备我可以帮你鉴定它是否还具有更大的潜能,另\\\r\n外,带有神秘属性的装备,也可以在我这里进行解读和激活；你还\\\r\n可以在我这里用羊皮卷制作神秘卷轴来卖钱。\\\r\n<鉴定宝物/@鉴定宝物> <关于神秘属性/@神秘属性> <关于卷轴碎片/@卷轴碎片> <宝物品评/@宝物品评>\\\r\n<宝物任务：鉴定师成长之路/@宝物任务>\\ \r\n\r\n[@宝物任务]\r\n欢迎来到风云变幻的玛法大陆,开始您的勇士征程,在您征战\\\r\n玛法大陆的同时,我们还为您开启了神秘的宝物宝藏等待着您\\\r\n的探索。\\ \\\r\n<初入师门/@初入师门> <我有其它事情找你/@main>\\ \r\n\r\n[@初入师门]\r\n想要入门学习我的宝物鉴定绝学必须要接受我的考验。\\ \\\r\n<接受考验/@接受考验>\\\r\n<我再考虑考虑/@EXIT> \r\n\r\n[@接受考验]\r\n自行修改。\\ \\ \r\n\r\n[@宝物品评]\r\n通过对宝物各项属性的评价,我可以帮你品评一下你的宝物得分和\\\r\n星级,宝物得分越高,星级越高,经过我品评的装备可以拿到\\\r\n收藏家那里去进行展示,让玛法大陆的勇士们都对你的神兵利器\\\r\n顶礼膜拜。\\ \\\r\n<品评宝物/@品评宝物>\\ \\\r\n<返回/@main>\\ \r\n\r\n[@品评宝物]\r\n未开放。\\ \\\r\n<返回/@main>\\ \r\n\r\n[@卷轴碎片]\r\n使用卷轴碎片,可以兑换鉴定卷轴。\\\r\n要获得卷轴碎片,你可以拿你不用的闲置装备跟我换,星王以上的装\\\r\n备都可以换成卷轴碎片,<COLOR=clRed 越好的装备换得的卷轴碎片就越多。>\\\r\n当然,有些装备看上去级别比较低,但品质却是很高,如果你能给我\\\r\n这样的装备,我会给你<COLOR=clRed 非常多的卷轴碎片。>\\\r\n兑换一级卷轴需要<COLOR=clRed 5>个卷轴碎片,兑换二级卷轴需要<COLOR=clRed 10>个卷轴碎片,\\\r\n兑换三级卷轴需要<COLOR=clRed 30>个卷轴碎片。\\\r\n<使用装备换取卷轴碎片/@@ExchangeBook> <参加活动获得卷轴碎片/@活动卷轴碎片>\\\r\n<使用卷轴碎片兑换鉴定卷轴/@碎片换卷轴> <返回/@main>\\ \r\n\r\n[@碎片换卷轴]\r\n请问你要兑换哪种鉴定卷轴？一级卷轴需要5颗卷轴碎片,二级卷轴\\\r\n需要10颗卷轴碎片,三级卷轴需要30颗卷轴碎片。注：所有兑换的精\\\r\n鉴卷轴都是绑定的。\\ \\\r\n<兑换一级卷轴/@一级卷轴>\\\r\n<兑换二级卷轴/@二级卷轴>\\\r\n<兑换三级卷轴/@三级卷轴>\\\r\n<返回/@卷轴碎片>\\ \r\n\r\n[@三级卷轴]\r\n#IF\r\nCHECKITEM 卷轴碎片 30\r\n#ACT\r\nTAKE 卷轴碎片 30\r\nGIVE 三级鉴定卷轴 1\r\nCLOSE\r\n#ELSESAY\r\n兑换需要30个卷轴碎片,你没有足够的碎片。\\ \\\r\n<返回/@碎片换卷轴> \r\n\r\n[@二级卷轴]\r\n#IF\r\nCHECKITEM 卷轴碎片 10\r\n#ACT\r\nTAKE 卷轴碎片 10\r\nGIVE 二级鉴定卷轴 1\r\nCLOSE\r\n#ELSESAY\r\n兑换需要10个卷轴碎片,你没有足够的碎片。\\ \\\r\n<返回/@碎片换卷轴> \r\n\r\n[@一级卷轴]\r\n#IF\r\nCHECKITEM 卷轴碎片 5\r\n#ACT\r\nTAKE 卷轴碎片 5\r\nGIVE 一级鉴定卷轴 1\r\nCLOSE\r\n#ELSESAY\r\n兑换需要5个卷轴碎片,你没有足够的碎片。\\ \\\r\n<返回/@碎片换卷轴> \r\n\r\n[@活动卷轴碎片]\r\n请问你要使用什么东西兑换卷轴碎片,卷轴碎片可以合成各级鉴定\\\r\n卷轴。\\ \\\r\n<用一个珍珑碎片兑换15个卷轴碎片/@一珍珑兑换>\\\r\n<用六个珍珑碎片兑换100个卷轴碎片/@六珍珑兑换>\\\r\n<返回/@卷轴碎片>\\ \r\n\r\n[@六珍珑兑换]\r\n#IF\r\nCHECKITEM 珍珑碎片 6\r\n#ACT\r\nTAKE 珍珑碎片 6\r\nGIVE 卷轴碎片 100\r\nCLOSE\r\n#ELSESAY\r\n你的背包中没有足够的珍珑碎片,无法兑换。\\ \\\r\n<返回/@活动卷轴碎片> \r\n\r\n[@一珍珑兑换]\r\n#IF\r\nCHECKITEM 珍珑碎片 1\r\n#ACT\r\nTAKE 珍珑碎片 1\r\nGIVE 卷轴碎片 15\r\nCLOSE\r\n#ELSESAY\r\n你的背包中没有珍珑碎片,无法兑换。\\ \\\r\n<返回/@活动卷轴碎片> \r\n\r\n[@神秘属性]\r\n有些鉴定出来的宝物带有神秘属性,并不是我不能解读这些神秘的\\\r\n功能,因为解读宝物的属性需要大量的经验和历史知道,我也不是\\\r\n万能的,需要通过阅读更多的神秘卷轴来帮你揭密神秘属性。\\\r\n<制作与解读/@@SecretProperty>\\\r\n<关于神秘卷轴/@神秘卷轴>\\\r\n<返回/@main>\\ \r\n\r\n[@神秘卷轴]\r\n神秘卷轴是由玛法勇士们根据自己的阅历和经验写成的鉴宝经验总\\\r\n结,根据每个人神秘解读的技能等级和熟练度,卷轴也会对应一定\\\r\n的熟练度和等级,等级越高熟练度越高的卷轴,解读神秘属性成功\\\r\n的概率就越高。\\\r\n<返回/@神秘属性>\\\r\n\r\n\r\n[@鉴定宝物]\r\n鉴宝是一门技术,更是一门艺术。宝物和普通装备的区别,在于细微\\\r\n之间,有人说靠眼力,我觉得,应该是靠心去感受。\\\r\n<COLOR=clRed 高级鉴定有机会将你正在鉴定的装备鉴定为主宰装备,届时你正在鉴>\\\r\n<COLOR=clRed 定的装备将消失,如果你不想将某件装备鉴定为主宰装备,你可以使>\\\r\n<COLOR=clRed 用普通鉴定。>\\\r\n<鉴定宝物/@@TreasureIdentify> <宝物的用途/@宝物的用途>\\\r\n<如何获得鉴定卷轴/@鉴定卷轴> <关于高级鉴定/@高级鉴定>\\ \r\n\r\n[@高级鉴定]\r\n我这里共有两种鉴定方式,一个是普通鉴定,一个是高级鉴定,两种\\\r\n方式使用的鉴定卷轴和卷轴碎片是一样的,唯一的不同是,使用高级\\\r\n鉴定方式,将有概率将你正在鉴定的装备替换为对应的古董装备,古\\\r\n董装备属性非常高,只有非常幸运的勇士才能鉴定得到。\\\r\n<COLOR=clRed 当然,如果你不想要自己的装备变成主宰装备,你可以使用普通鉴定>\\\r\n<COLOR=clRed 方式鉴定。>\\\r\n<查看主宰装备属性/@主宰属性>\\\r\n<返回/@鉴定宝物>\\ \r\n\r\n[@主宰属性]\r\n主宰神甲(男) 防御20-26 魔御15-22 攻击10-14 魔法10-16 道术10-16\\\r\n主宰神甲(女) 防御20-26 魔御15-22 攻击10-14 魔法10-16 道术10-16\\\r\n主宰神剑 幸运+7 攻击10-70 魔法10-33 道术12-32\\\r\n　　　　 暴击等级+15 准确+2\\ \r\n主宰项链 攻击5-13 魔法5-13 道术5-13 幸运+2\\\r\n主宰护腕 防御0-2 魔御0-2 攻击5-15 魔法5-15 道术5-15\\\r\n主宰之戒 防御0-2 攻击5-21 魔法5-21 道术5-21\\\r\n主宰之冠 防御6-8 魔御6-8 攻击2-14 魔法2-14 道术2-14\\\r\n<下一页/@下一页> <返回/@鉴定宝物>\\ \r\n\r\n[@下一页]\r\n主宰腰带 防御6-8 魔御6-8 攻击2-14 魔法2-14 道术2-14\\\r\n主宰之靴 防御6-8 魔御6-8 攻击2-14 魔法2-14 道术2-14\\\r\n主宰斗笠 防御3-3 魔御3-3 攻击4-8 魔法4-8 道术4-8\\\r\n主宰面巾 防御3-3 魔御3-3 攻击4-8 魔法4-8 道术4-8\\\r\n主宰勋章 攻击15-18 魔法15-18 道术15-18\\ \\ \\ \\\r\n<上一页/@主宰属性> <返回/@鉴定宝物>\\ \r\n\r\n[@鉴定卷轴]\r\n在日常的活动中可以获得卷轴碎片,卷轴碎片可以在我这里合成各级\\\r\n鉴定卷轴。可以获得的卷轴碎片的活动包括：\\\r\n(活动介绍自己写)\\ \r\n\r\n[@宝物的用途]\r\n宝物带有非常强大的附加属性,高级宝物最高可以附加<COLOR=clRed 攻魔道>\\\r\n<COLOR=clRed 上限+10>的属性,有些甚至带有<COLOR=clRed 护身特技、八卦护身特技、>\\\r\n<COLOR=clRed 战意麻痹特技,麻痹特技,魔道麻痹特技、探测特技、传送特技>\\\r\n等超强属性！\\\r\n若同时穿戴带有同类属性特技的宝物和神戒,优先计算神戒的属性。\\\r\n例如,同时穿戴麻痹戒指和带有战意麻痹特技的装备,则优先按照\\\r\n麻痹戒指的特效麻痹对手。\\ \\\r\n<返回/@鉴定宝物> \r\n");

                    cd = new CompletionData("摆摊市集脚本", "获取摆摊市集脚本");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("摆摊市集脚本", "(@@StallMarket)\r\n[@main]\r\n#say\r\n<打开摆摊集市/@@StallMarket>\\\r\n\r\n \r\n\r\n[@@StallMarket]\r\n欢迎使用摆摊集市！\\ \\<返回/@main>   <关闭/@exit>\r\n");

                    cd = new CompletionData("新INI功能示范脚本", "新INI功能示范脚本\r\n高效率的Ini文件NPC操作命令说明：\r\n\r\n全服自定义排行榜.ini\r\n\r\nINI内容\r\n\r\n[鲜花]  ;全服自定义排行榜.ini=文件;[鲜花]=节 ;bluem2 =键 ;100=值\r\nbluem2=100\r\n网易=100\r\n百度=100\r\n\r\n如果手动修改ini内容后,请使用命令,INI_RELOAD  文件     //重新加载文件,可以用于手动修改了文本文件的情况下。\r\n\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("新INI功能示范脚本", "全服自定义排行榜.ini\r\n\r\nINI内容\r\n\r\n[鲜花]  ;全服自定义排行榜.ini=文件;[鲜花]=节 ;bluem2 =键 ;100=值\r\nbluem2=100\r\n网易=100\r\n百度=100\r\n\r\n如果手动修改ini内容后,请使用命令,INI_RELOAD  文件     //重新加载文件,可以用于手动修改了文本文件的情况下。\r\n\r\n\r\n示例脚本：以下脚本只是演示,请GM自行完善\r\n\r\n注：脚本使用的是S1-S20 M1-M20的个人变量,如果你的版本里有,请自行修改。\r\n\r\n[@main]\r\n<排序鲜花的排名(名字+数值)/@Get>   <排序鲜花的排名(名字)/@GetEx>\\\\\r\n\r\n<检查鲜花的排名(名字)/@Get1>\\\\\r\n\r\n<清理鲜花数据/@INI_CLEAR_SECTION>\\\\ \r\n\r\n<关闭/@exit >\r\n\r\n[@Get1]\r\n#IF\r\nEQUAL S1 <$USERNAME>\r\n#ACT\r\nMessageBox 目前你排名第一。\r\nBREAK\r\n#IF\r\nEQUAL S2 <$USERNAME>\r\n#ACT\r\nMessageBox 目前你排名第二。\r\nBREAK\r\n#IF\r\nEQUAL S3 <$USERNAME>\r\n#ACT\r\nMessageBox 目前你排名第三。\r\nBREAK\r\n#IF\r\nEQUAL S4 <$USERNAME>\r\n#ACT\r\nMessageBox 目前你排名第四。\r\nBREAK\r\n#IF\r\nEQUAL S5 <$USERNAME>\r\n#ACT\r\nMessageBox 目前你排名第五。\r\nBREAK\r\n#IF\r\nEQUAL S6 <$USERNAME>\r\n#ACT\r\nMessageBox 目前你排名第六。\r\nBREAK\r\n#IF\r\nEQUAL S7 <$USERNAME>\r\n#ACT\r\nMessageBox 目前你排名第七。\r\nBREAK\r\n#IF\r\nEQUAL S8 <$USERNAME>\r\n#ACT\r\nMessageBox 目前你排名第八。\r\nBREAK\r\n#IF\r\nEQUAL S9 <$USERNAME>\r\n#ACT\r\nMessageBox 目前你排名第九。\r\nBREAK\r\n#IF\r\nEQUAL S10 <$USERNAME>\r\n#ACT\r\nMessageBox 目前你排名第十。\r\nBREAK\r\n#ELSEACT\r\nMessageBox 榜上无名,请再接再厉。\r\nBREAK\r\n\r\n[@INI_CLEAR_SECTION]\r\n#IF\r\nCHECKLEVELEX > 0\r\n#ACT\r\nINI_CLEAR_SECTION ..\\QuestDiary\\功能脚本\\全服自定义排行榜.ini 鲜花\r\nINI_SAVE ..\\QuestDiary\\功能脚本\\全服自定义排行榜.ini\r\nINI_SORT_DELETE INI_排名__鲜花_IV\r\nSENDMSG 5 清楚鲜花数据成功。\r\n\r\n[@Get]\r\n#IF\r\nCHECKLEVELEX > 0\r\n#ACT\r\nINI_SORT_READ_SECTION_VALUES ..\\QuestDiary\\功能脚本\\全服自定义排行榜.ini 鲜花 1 1 1 INI_排名__鲜花_IV 0\r\nINI_SORT_SECTION_VALUES_COUNT INI_排名__鲜花_IV D0\r\n;SENDMSG 5 获取到排行数据：<$STR(D0)>条\r\n\r\n#IF\r\nLARGE D0 1\r\n#ACT\r\nINI_SORT_GET_SECTION_STRING INI_排名__鲜花_IV S0\r\n;;P0用于限制最多的排名次数\r\nMOV P0 1\r\n;;S92用于累计排名,#ELSESAY的显示调用\r\nMOV S92\r\nSENDMSG 5 正在计算排名,请稍后。。。\r\nDELAYGOTO 1 @GetIdentValueLoop\r\n#ELSEACT\r\nMessageBox 目前没有数据。\r\nBREAK\r\n\r\n\r\n;按顺序获取排行字符串(IDENT_VALUE)到S1~S20\r\n[@GetIdentValueLoop]\r\n#IF\r\nLARGE D0 1\r\nSMALL P0 10\r\n#ACT\r\nMOV S90 S\r\nINC S90 <$STR(P0)>\r\nINC S92 <COLOR=clYellow>\r\nINC S92  \r\nINC S92 <$STR(P0)>、\r\nINC S92 <COLOR=$F7FFFF>\r\nINI_SORT_GET_IDENT_VALUE_STRING INI_排名__鲜花_IV <$STR(P0)> <$STR(S90)>\r\n\r\nMOV S91 <$STR(S\r\nINC S91 <$STR(P0)>\r\nINC S91 )>\r\n\r\nINC S92 <$STR(S91)>\r\nINC S92 \\\r\n;SENDMSG 5 排名<$STR(P0)>：<$STR(S91)>\r\n\r\nDEC D0 1\r\nINC P0 1\r\n\r\nDELAYGOTO 1 @GetIdentValueLoop\r\n#SAY\r\n<$STR(S0)>的当前排名是：\\\r\n<$STR(S92)>\\\r\n<关闭/@exit>\r\n#ACT\r\nBREAK\r\n\r\n#IF\r\nLARGE D0 1\r\nLARGE P0 9\r\nSMALL P0 21\r\n#ACT\r\nMOV S90 S\r\nINC S90 <$STR(P0)>\r\nINC S92 <COLOR=clYellow>\r\nINC S92 <$STR(P0)>、\r\nINC S92 <COLOR=$F7FFFF>\r\nINI_SORT_GET_IDENT_VALUE_STRING INI_排名__鲜花_IV <$STR(P0)> <$STR(S90)>\r\n\r\nMOV S91 <$STR(S\r\nINC S91 <$STR(P0)>\r\nINC S91 )>\r\n\r\nINC S92 <$STR(S91)>\r\nINC S92 \\\r\n;SENDMSG 5 排名<$STR(P0)>：<$STR(S91)>\r\n\r\nDEC D0 1\r\nINC P0 1\r\n\r\nDELAYGOTO 1 @GetIdentValueLoop\r\n#SAY\r\n<$STR(S0)>的当前排名是：\\\r\n<$STR(S92)>\\\r\n<关闭/@exit>\r\n#ELSESAY\r\n<$STR(S0)>的当前排名是：\\\r\n<$STR(S92)>\\\r\n<关闭/@exit>\r\n\r\n#ELSEACT\r\nSENDMSG 5 鲜花排名完毕。\r\nBREAK\r\n\r\n \r\n\r\n[@GetEx]\r\n#IF\r\nCHECKLEVELEX > 0\r\n#ACT\r\nINI_RELOAD ..\\QuestDiary\\功能脚本\\全服自定义排行榜.ini\r\nINI_SORT_READ_SECTION_VALUES ..\\QuestDiary\\功能脚本\\全服自定义排行榜.ini 鲜花 1 1 1 INI_排名__鲜花_IVEX 0\r\nINI_SORT_SECTION_VALUES_COUNT INI_排名__鲜花_IVEX D0\r\n;SENDMSG 5 获取鲜花排行数据：<$STR(D0)>条\r\n\r\n#IF\r\nLARGE D0 1\r\n#ACT\r\nINI_SORT_GET_SECTION_STRING INI_排名__鲜花_IVEX S0\r\n;;P0用于限制最多的排名次数\r\nMOV P0 1\r\n;;S92用于累计排名,#ELSESAY的显示调用\r\nMOV S92\r\nSENDMSG 5 正在计算排名,请稍后。。。\r\nDELAYGOTO 1 @GetIdentValueLoopEx\r\nBREAK\r\n#ELSEACT\r\nMessageBox 目前没有数据。\r\nBREAK\r\n\r\n;按顺序获取排行,分别获取IDENT和VALUE到S1~S20\r\n[@GetIdentValueLoopEx]\r\n#IF\r\nLARGE D0 1\r\nSMALL P0 10\r\n#ACT\r\nMOV S90 S\r\nINC S90 <$STR(P0)>\r\nMOV S95 M\r\nINC S95 <$STR(P0)>\r\nINC S92 <COLOR=clYellow>\r\nINC S92  \r\nINC S92 <$STR(P0)>、\r\nINC S92 <COLOR=$F7FFFF>\r\nINI_SORT_GET_IDENT_STRING INI_排名__鲜花_IVEX <$STR(P0)> <$STR(S90)>\r\nINI_SORT_GET_VALUE_INTEGER INI_排名__鲜花_IVEX <$STR(P0)> <$STR(S95)>\r\n\r\nMOV S91 <$STR(S\r\nINC S91 <$STR(P0)>\r\nINC S91 )>\r\n\r\nMOV S93 <$STR(M\r\nINC S93 <$STR(P0)>\r\nINC S93 )>\r\n\r\nINC S92 <$STR(S91)>\r\nINC S92 \"  \"\r\nINC S92 \"  鲜花数：\"\r\nINC S92 <$STR(S93)>\r\nINC S92 \" \"\r\nINC S92 \\\r\n;;SENDMSG 5 排名<$STR(P0)>：<$STR(S91)>\r\nDEC D0 1\r\nINC P0 1\r\nDELAYGOTO 1 @GetIdentValueLoopEx\r\n#SAY\r\n<$STR(S0)>的当前排名是：\\\r\n<$STR(S92)>\\\r\n<关闭/@exit>\r\n#ACT\r\nBREAK\r\n\r\n#IF\r\nLARGE D0 1\r\nLARGE P0 9\r\nSMALL P0 23\r\n#ACT\r\nMOV S90 S\r\nINC S90 <$STR(P0)>\r\nMOV S95 M\r\nINC S95 <$STR(P0)>\r\nINC S92 <COLOR=clYellow>\r\nINC S92 <$STR(P0)>、\r\nINC S92 <COLOR=$F7FFFF>\r\n\r\nINI_SORT_GET_IDENT_STRING INI_排名__鲜花_IVEX <$STR(P0)> <$STR(S90)>\r\nINI_SORT_GET_VALUE_INTEGER INI_排名__鲜花_IVEX <$STR(P0)> <$STR(S95)>\r\n\r\nMOV S91 <$STR(S\r\nINC S91 <$STR(P0)>\r\nINC S91 )>\r\n\r\nMOV S93 <$STR(M\r\nINC S93 <$STR(P0)>\r\nINC S93 )>\r\n\r\nINC S92 <$STR(S91)>\r\nINC S92 \"  \"\r\nINC S92 \"  鲜花数：\"\r\nINC S92 <$STR(S93)>\r\nINC S92 \" \"\r\nINC S92 \\\r\n;;SENDMSG 5 排名<$STR(P0)>：<$STR(S91)>\r\nDEC D0 1\r\nINC P0 1\r\nDELAYGOTO 1 @GetIdentValueLoopEx\r\n#SAY\r\n<$STR(S0)>的当前排名是：\\\r\n<$STR(S92)>\\\r\n<关闭/@exit>\r\n#ELSESAY\r\n<$STR(S0)>的当前排名是：\\\r\n<$STR(S92)>\\\r\n<关闭/@exit>\r\n#ELSEACT\r\nSENDMSG 5 鲜花排名完毕。\r\nBREAK\r\n");

                    cd = new CompletionData("武器升级脚本", "脚本：\r\n---------------------------------------------------------------------------\r\n\r\n新的武器升级：(可自定升级需要的物品和条件)升级后的武器也带*号标识\r\n@supermake 1 10 参数3  (@supermake 为GM命令.可配合GMEXECUTE 执行GM命令.使用前注意把命令在M2配套修改)\r\n如：\r\nGMEXECUTE supermake 1 10 参数3\r\n\r\n参数3详解：当参数3为1时 是必定失败破碎\r\n攻击  10-13   (必定成功+1~+4)比如写12就是攻击加3必定成功\r\n魔法  20-23\r\n道术  30-33\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("武器升级脚本", "新的武器升级：（可自定升级需要的物品和条件）升级后的武器也带*号标识\r\n@supermake 1 10 参数3  (@supermake 为GM命令.可配合GMEXECUTE 执行GM命令.使用前注意把命令在M2配套修改)\r\n如：\r\nGMEXECUTE supermake 1 10 参数3\r\n\r\n参数3详解：当参数3为1时 是必定失败破碎\r\n攻击  10-13   （必定成功+1~+4）比如写12就是攻击加3必定成功\r\n魔法  20-23\r\n道术  30-33\r\n");

                    cd = new CompletionData("灵符仙子脚本", "获取灵符仙子脚本");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("灵符仙子脚本", "(@@INPUTINTEGER)\r\n[@MAIN]\r\n在进入天关通道前的瞬间,使用1张灵符将可以隐身遁形。\\\r\n此时,天关通道内的怪物将无法看到你,\\\r\n你就可以直接穿越通道,闯入天关！\\\r\n通过我,我可以将您的元宝兑换成灵符\\\r\n进入庄园后,等待一分钟后就可兑换灵符\\\r\n<前往天庭闯天关/@CHUANG>\\\r\n<查询灵符数量/@CHKLINFU>\\\r\n<了解闯天关的规则/@XIJIE> <使用元宝兑换灵符/@YBBUYLINGFU>\\\r\n<去周围看看/@RANDOMGA0> <离开/@EXIT>\\\r\n\r\n\r\n[@CHUANG]\r\n#IF\r\n#ACT\r\nMAPMOVE TT \r\n\r\n[@XIJIE]\r\n<同时拥有月卡和秒卡,如何兑换灵符/@DUIHUAN>\\\r\n<为什么提示我“天关通道已经满员,请稍后再试”/@WHY>\\\r\n<兑换灵符时,为什么系统提示:“你要等一会儿,才能兑换！”/@TISHI>\\\r\n<在天关中获得的金刚石如何显示/@XIANSHI>\\\r\n<天关通道是否就是天关/@SHIFOU>\\ \\\r\n<返回/@MAIN> \r\n\r\n[@SHIFOU]\r\n天关通道并不是天关,只是天庭通往天关的通道\\\r\n天关通道里没有宝藏,真正的宝藏是隐藏在天关里的\\ \\\r\n<返回上一页/@XIJIE> \r\n\r\n[@XIANSHI]\r\n如果您在天关中获得了金刚石,那么在同一天关中是无法显示的\\\r\n如果您进入下一天关,或返回天庭,就可以看到增加的金刚石了\\ \\\r\n<返回上一页/@XIJIE> \r\n\r\n[@TISHI]\r\n系统规定每次兑换灵符后,必须等一分钟后才能再次兑换\\\\\r\n<返回上一页/@XIJIE> \r\n\r\n[@WHY]\r\n如果玩家对自己的实力有信心,可以[选择消灭怪物]\\\r\n在没有灵符的保护下进入天关通道\\玩家必须只身消灭完天关通道里的所有怪物,才能进入天关夺宝\\\r\n这样的通道一共有10条\\如果满员就会提示：“天关通道已经满员,请稍后再试”\\ \\\r\n<返回上一页/@XIJIE> \r\n\r\n[@DUIHUAN]\r\n兑换灵符时,如帐号同时存在月卡、秒卡,系统将先扣月卡再扣秒卡\\\r\n如玩家兑换数量大于等于当前月卡剩余天数\\\r\n系统将只兑换月卡当前余额减去1天后所对应的灵符数量\\\r\n如果玩家还要继续兑换,则需等待一段时间\\\r\n这段时间间隔根据玩家申请时选择兑换灵符的数量而定\\\r\n这段时间以后,玩家可使用秒卡进行灵符的兑换\\\r\n秒卡能兑换的最大额度为秒卡的当前余额减去1小时所对应的灵符数量\\\r\n<“减去 1天”、“减去 1小时”是什么意思/@YISI>\\\r\n<返回上一页/@XIJIE> \r\n\r\n[@YISI]\r\n月卡用户在兑换灵符时,系统会保留当前月卡的最后一天\\\r\n以尽量保证玩家下次能登陆游戏\\\r\n当前月卡所能兑换灵符的最大额度等于月卡的当前余额减去1天\\\r\n保留的这1天依然遵守月卡“随时间自然消耗”的计费方式\\\r\n秒卡用户在兑换灵符时,系统也将保留当前秒卡的最后一小时\\\r\n当前秒卡所能兑换灵符的最大额度等于秒卡的当前余额减去1小时\\\r\n保留的这1小时也依然遵守秒卡“随着游戏而消耗”的计费方式\\\r\n<返回/@XIJIE> \r\n\r\n[@RANDOMGA0]\r\n#IF\r\n#ACT\r\nMAPMOVE GA0 \r\n\r\n[@YBBUYLINGFU]\r\n#IF\r\nCHECKGAMEGOLD > 0\r\n#SAY\r\n兑换标准：1个元宝可以用以兑换1张灵符,\\\r\n灵符的数量请自己决定、每次只能兑换100张以下数量的灵符,\\\r\n如您进行灵符的兑换,则视您已经同意以上兑换规则。\\\r\n<COLOR=clYellow>元宝总数为：<$GAMEGOLD>颗\\\r\n你拥有灵符：<$GAMEGIRD>张\\ \\\r\n<确定使用元宝兑换/@兑换灵符> <离开/@EXIT>\\\r\n#ELSESAY\r\n兑换标准：1个元宝可以用以兑换1张灵符,\\\r\n您没有足够的元宝,请充值！\\\r\n<离开/@EXIT>\\ \r\n\r\n[@兑换灵符]\r\n#ACT\r\nMOV M1 0\r\nQUERYVALUE 1 1 3 @兑换 请在下面输入需要兑换灵符的数量：1-100之间的数值！ \r\n\r\n[@兑换]\r\n#IF\r\nSMALL M1 1\r\n#SAY\r\n请输入正确1-100之间的数字！\\ \\<关闭/@exit>\r\n#ACT\r\nBREAK \r\n\r\n#IF\r\nLARGE M1 100\r\n#SAY\r\n请输入正确1-100之间的数字！\\ \\<关闭/@exit>\r\n#ACT\r\nBREAK \r\n\r\n#IF\r\nCHECKGAMEGOLD ? <$STR(M1)>\r\n#ACT\r\nGAMEGIRD + <$STR(M1)>\r\nGAMEGOLD - <$STR(M1)>\r\n#SAY\r\n兑换标准：1个元宝可以用以兑换1张灵符,\\\r\n灵符的数量请自己决定、每次只能兑换100张以下数量的灵符,\\\r\n如您进行灵符的兑换,则视您已经同意以上兑换规则。\\\r\n<COLOR=clYellow>元宝总数为：<$GAMEGOLD>颗\\\r\n你拥有灵符：<$GAMEGIRD>张\\ \\\r\n<确定使用元宝兑换/@兑换灵符>\\\r\n<离开/@EXIT>\\\r\n#ELSESAY\r\n兑换标准：1个元宝可以用以兑换1张灵符,\\\r\n您没有足够的元宝,请充值！\\\r\n<离开/@EXIT> \r\n");

                    cd = new CompletionData("盛大元宝寄售脚本", "获取盛大元宝寄售脚本");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("盛大元宝寄售脚本", "(@YBDEAL)\r\n[@MAIN]\r\n您好,有什么可以效劳的？我可以为您提供关于元宝的各类服务 \\\r\n现在已经开通了金刚石的元宝交易功能,\\\r\n点击交易栏的第十格可以放置金刚石进行交易,\\\r\n如果不更新客户端,<交易栏的第十格将不能使用>。\\\r\n<查询元宝交易协议/@RULE1> <开通元宝交易/@RULE>\\\r\n<查询元宝数/@ASKYBNUM> <出售物品/@@DEALYBME>\\\r\n<查询正在出售的物品/@ASKYBSELL> <购买物品/@ASKYBDEAL>\\\r\n<查询交易信息/@ASKYBDEALLOG> <关闭对话/@EXIT>\\\r\n<去周围看看/@RANDOMGA0>\\ \r\n\r\n[@RANDOMGA0]\r\n#ACT\r\nMAPMOVE GA0 \r\n\r\n[@ASKYBSELL]\r\n我只能保证在我这里出售的物品3天内是不会有差池\\\r\n如果超过3天交易还没有成功,我可不能保证物品不会丢失\\\r\n并且就算不丢失,取回时也需要另外交给我1个元宝\\\r\n要知道,我做这行是很辛苦的,如果不想找麻烦\\\r\n那就在3天之内保证交易成功\\\r\n<返回/@MAIN>\r\n#ACT\r\nQUERYYBSELL \r\n\r\n[@ASKYBDEAL]\r\n我只能保证在我这里出售的物品3天内是不会有差池\\\r\n如果超过3天交易还没有成功,我可不能保证物品不会丢失\\\r\n并且就算不丢失,取回时也需要另外交给我1个元宝\\\r\n要知道,我做这行是很辛苦的,如果不想找麻烦\\\r\n那就在3天之内保证交易成功\\\r\n<返回/@MAIN>\r\n#ACT\r\nQUERYYBDEAL \r\n\r\n[@ASKYBDEALLOG]\r\n<$QUERYYBDEALLOG> \r\n\r\n[@ASKYBNUM]\r\n你的帐号里元宝的总数是：<$GAMEGOLD>颗。\\ \\ \\\r\n<返回/@MAIN> \r\n\r\n[@RULE]\r\n人人都知道,我是玛法大陆最公正的人\\\r\n您要开通以元宝出售道具,必须向我支付1个元宝\\\r\n我将为您提供永久、公正的服务\\ \\\r\n<我已阅读并同意元宝交易协议,支付一个元宝开通元宝交易/@OPENYBSELL>\\\r\n<不同意/@EXIT> \r\n\r\n[@OPENYBSELL]\r\n#ACT\r\n//参数1 表示开通需要元宝\r\nOPENYBDEAL 1 \r\n\r\n[@RULE1]\r\n您确定已仔细阅读了《元宝交易协议》并接受协议内的所有条款\\\r\n1、第一次使用元宝交易,必须开通元宝交易功能并向NPC支付1个元宝\\\r\n2、充值元宝是针对帐号进行的,\\\r\n该帐号下相同服务器的角色均可以使用这些元宝\\\r\n3、如果物品放在NPC处出售超过3天,交易将被终止,\\\r\n同时卖方取回物品时需额外再支付1个元宝。\\\r\n4、卖方在输入买方角色和元宝数量时,请注意输入正确的角色名\\\r\n因自己本身操作失误造成的损失系统将不进行补偿。\\\r\n<下一页/@RULE2> \r\n\r\n[@RULE2]\r\n5、在一笔交易未完成的情况下,交易双方均不可以再次进行元宝交易\\\r\n6、帐号在单组服务器携带元宝上限:9999个,\\\r\n元宝单次交易额最高不超过9999个,\\\r\n交易成功如果角色携带元宝数量超过上限,\\\r\n超过上限的元宝数量将被自动转换成游戏时间（天卡）\\\r\n7、交易成功,卖方将自动获得相应元宝\\\r\n8、玩家可在我这里查询最近一次的元宝交易记录\\\r\n在取消交易或接受交易物品时,若包裹栏无足够空间,则物品将不能取回\\\r\n<返回/@MAIN> \r\n\r\n[@ASKYBSELLFAIL]\r\n没有查询到指定的记录\r\n[@ASKYBDEALFAIL]\r\n没有查询到指定的记录\r\n[@YBDEALOK]\r\n收购物品成功\r\n");

                    cd = new CompletionData("礼盒赠送脚本", "获取礼盒赠送脚本");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("礼盒赠送脚本", "[@main]\r\n你想做什么？\\\r\n<我要赠送/@赠送> <获取装备/@装备>\\\r\n<离开/@exit> \r\n\r\n[@装备]\r\n#ACT\r\nGIVEEX 屠龙10=1,1=2,2=3,3=4,4=5,5=6,6=7,7=8,14=9,15=10\r\nGIVEEX 嗜魂法杖10=1,1=2,2=3,3=4,4=5,5=6,6=7,7=8,14=9,15=10\r\nGIVEEX 道德戒指10=1,1=2,2=3,3=4,4=5,5=6,6=7,7=8,14=9,15=10\r\nGIVEEX 珊瑚戒指10=1,1=2,2=3,3=4,4=5,5=6,6=7,7=8,14=9,15=10\r\nGIVEEX 死神手套10=1,1=2,2=3,3=4,4=5,5=6,6=7,7=8,14=9,15=10\r\nGIVEEX 铁手镯10=1,1=2,2=3,3=4,4=5,5=6,6=7,7=8,14=9,15=10\r\nGIVEEX 灯笼项链10=1,1=2,2=3,3=4,4=5,5=6,6=7,7=8,14=9,15=10\r\nGIVEEX 蓝翡翠项链10=1,1=2,2=3,3=4,4=5,5=6,6=7,7=8,14=9,15=10\r\n\r\n[@赠送]\r\n#IF\r\n#ACT\r\nQUERYVALUE 2 0 14 @检测 朋友越多,快乐越多,千金易得,挚友难求啊！请输入赠送对象名称： QF \r\n\r\n[@检测]\r\n#IF\r\nEQUAL S2 <$USERNAME>\r\n#ACT\r\nMessageBox 不能对自己发送赠送。\r\nBREAK\r\n#IF\r\nCHECKONLINE <$STR(S2)>\r\n#ACT\r\nDELAYCALL 10 @DELAY_UPGRADEDLGITEM\r\n#ELSEACT\r\nMessageBox <$STR(S2)>\\不在线,你不能赠送！\r\n\r\n\r\n[@DELAY_UPGRADEDLGITEM]\r\n#ACT\r\nQUERYITEMDLG 请放入赠送物品 @CHECKDLGITEMTYPE 0\r\n\r\n\r\n[@CHECKDLGITEMTYPE]\r\n#IF\r\nCHECKDLGITEMTYPE WEAPON\r\nCHECKONLINE <$STR(S2)>\r\n#ACT\r\nSENDMSG 0 <$STR(S2)>收到了友人:<$USERNAME>,赠送的：<$DLGITEMNAME> 246 245\r\nGETDLGITEMVALUE M1 0\r\nGETDLGITEMVALUE M2 1\r\nGETDLGITEMVALUE M3 2\r\nGETDLGITEMVALUE M4 3\r\nGETDLGITEMVALUE M5 4\r\nGETDLGITEMVALUE M6 5\r\nGETDLGITEMVALUE M7 6\r\nGETDLGITEMVALUE M8 7\r\nGETDLGITEMVALUE M9 14\r\nGETDLGITEMVALUE M1015\r\n<$STR(S2)>.GIVEEX<$DLGITEMNAME>10=<$STR(M1)>,1=<$STR(M2)>,2=<$STR(M3)>,3=<$STR(M4)>,4=<$STR(M5)>,5=<$STR(M6)>,6=<$STR(M7)>,7=<$STR(M8)>,14=<$STR(M9)>,15=<$STR(M10)>\r\n<$STR(S2)>.SENDMSG 5 你收到了友人赠送的：<$DLGITEMNAME>\r\nMOV M1 0\r\nMOV M2 0\r\nMOV M3 0\r\nMOV M4 0\r\nMOV M5 0\r\nMOV M6 0\r\nMOV M7 0\r\nMOV M8 0\r\nMOV M9 0\r\nMOV M10 0\r\nTAKEDLGITEM\r\nCLOSE\r\nBREAK\r\n#IF\r\nCHECKDLGITEMTYPE DRESS\r\nCHECKONLINE <$STR(S2)>\r\n#ACT\r\nSENDMSG 0 <$STR(S2)>收到了友人:<$USERNAME>,赠送的：<$DLGITEMNAME> 246 245\r\nGETDLGITEMVALUE M1 0\r\nGETDLGITEMVALUE M2 1\r\nGETDLGITEMVALUE M3 2\r\nGETDLGITEMVALUE M4 3\r\nGETDLGITEMVALUE M5 4\r\nGETDLGITEMVALUE M9 14\r\nGETDLGITEMVALUE M1015\r\n<$STR(S2)>.GIVEEX<$DLGITEMNAME>10=<$STR(M1)>,1=<$STR(M2)>,2=<$STR(M3)>,3=<$STR(M4)>,4=<$STR(M5)>,14=<$STR(M9)>,15=<$STR(M10)>\r\n<$STR(S2)>.SENDMSG 5 你收到了友人赠送的：<$DLGITEMNAME>\r\nMOV M1 0\r\nMOV M2 0\r\nMOV M3 0\r\nMOV M4 0\r\nMOV M5 0\r\nMOV M9 0\r\nMOV M10 0\r\nTAKEDLGITEM\r\nCLOSE\r\nBREAK\r\n#IF\r\nCHECKDLGITEMTYPE MEDAL\r\nCHECKONLINE <$STR(S2)>\r\n#ACT\r\nSENDMSG 0 <$STR(S2)>收到了友人:<$USERNAME>,赠送的：<$DLGITEMNAME> 246 245\r\nGETDLGITEMVALUE M1 0\r\nGETDLGITEMVALUE M2 1\r\nGETDLGITEMVALUE M3 2\r\nGETDLGITEMVALUE M4 3\r\nGETDLGITEMVALUE M5 4\r\nGETDLGITEMVALUE M9 14\r\nGETDLGITEMVALUE M1015\r\n<$STR(S2)>.GIVEEX<$DLGITEMNAME>10=<$STR(M1)>,1=<$STR(M2)>,2=<$STR(M3)>,3=<$STR(M4)>,4=<$STR(M5)>,14=<$STR(M9)>,15=<$STR(M10)>\r\n<$STR(S2)>.SENDMSG 5 你收到了友人赠送的：<$DLGITEMNAME>\r\nMOV M1 0\r\nMOV M2 0\r\nMOV M3 0\r\nMOV M4 0\r\nMOV M5 0\r\nMOV M9 0\r\nMOV M10 0\r\nTAKEDLGITEM\r\nCLOSE\r\nBREAK\r\n#IF\r\nCHECKDLGITEMTYPE NECKLACE\r\nCHECKONLINE <$STR(S2)>\r\n#ACT\r\nSENDMSG 0 <$STR(S2)>收到了友人:<$USERNAME>,赠送的：<$DLGITEMNAME> 246 245\r\nGETDLGITEMVALUE M1 0\r\nGETDLGITEMVALUE M2 1\r\nGETDLGITEMVALUE M3 2\r\nGETDLGITEMVALUE M4 3\r\nGETDLGITEMVALUE M5 4\r\nGETDLGITEMVALUE M9 14\r\nGETDLGITEMVALUE M1015\r\n<$STR(S2)>.GIVEEX<$DLGITEMNAME>10=<$STR(M1)>,1=<$STR(M2)>,2=<$STR(M3)>,3=<$STR(M4)>,4=<$STR(M5)>,14=<$STR(M9)>,15=<$STR(M10)>\r\n<$STR(S2)>.SENDMSG 5 你收到了友人赠送的：<$DLGITEMNAME>\r\nMOV M1 0\r\nMOV M2 0\r\nMOV M3 0\r\nMOV M4 0\r\nMOV M5 0\r\nMOV M9 0\r\nMOV M10 0\r\nTAKEDLGITEM\r\nCLOSE\r\nBREAK\r\n#IF\r\nCHECKDLGITEMTYPE HELMET\r\nCHECKONLINE <$STR(S2)>\r\n#ACT\r\nSENDMSG 0 <$STR(S2)>收到了友人:<$USERNAME>,赠送的：<$DLGITEMNAME> 246 245\r\nGETDLGITEMVALUE M1 0\r\nGETDLGITEMVALUE M2 1\r\nGETDLGITEMVALUE M3 2\r\nGETDLGITEMVALUE M4 3\r\nGETDLGITEMVALUE M5 4\r\nGETDLGITEMVALUE M9 14\r\nGETDLGITEMVALUE M1015\r\n<$STR(S2)>.GIVEEX<$DLGITEMNAME>10=<$STR(M1)>,1=<$STR(M2)>,2=<$STR(M3)>,3=<$STR(M4)>,4=<$STR(M5)>,14=<$STR(M9)>,15=<$STR(M10)>\r\n<$STR(S2)>.SENDMSG 5 你收到了友人赠送的：<$DLGITEMNAME>\r\nMOV M1 0\r\nMOV M2 0\r\nMOV M3 0\r\nMOV M4 0\r\nMOV M5 0\r\nMOV M9 0\r\nMOV M10 0\r\nTAKEDLGITEM\r\nCLOSE\r\nBREAK\r\n#IF\r\nCHECKDLGITEMTYPE ARMRING\r\nCHECKONLINE <$STR(S2)>\r\n#ACT\r\nSENDMSG 0 <$STR(S2)>收到了友人:<$USERNAME>,赠送的：<$DLGITEMNAME> 246 245\r\nGETDLGITEMVALUE M1 0\r\nGETDLGITEMVALUE M2 1\r\nGETDLGITEMVALUE M3 2\r\nGETDLGITEMVALUE M4 3\r\nGETDLGITEMVALUE M5 4\r\nGETDLGITEMVALUE M9 14\r\nGETDLGITEMVALUE M1015\r\n<$STR(S2)>.GIVEEX<$DLGITEMNAME>10=<$STR(M1)>,1=<$STR(M2)>,2=<$STR(M3)>,3=<$STR(M4)>,4=<$STR(M5)>,14=<$STR(M9)>,15=<$STR(M10)>\r\n<$STR(S2)>.SENDMSG 5 你收到了友人赠送的：<$DLGITEMNAME>\r\nMOV M1 0\r\nMOV M2 0\r\nMOV M3 0\r\nMOV M4 0\r\nMOV M5 0\r\nMOV M9 0\r\nMOV M10 0\r\nTAKEDLGITEM\r\nCLOSE\r\nBREAK\r\n#IF\r\nCHECKDLGITEMTYPE RING\r\nCHECKONLINE <$STR(S2)>\r\n#ACT\r\nSENDMSG 0 <$STR(S2)>收到了友人:<$USERNAME>,赠送的：<$DLGITEMNAME> 246 245\r\nGETDLGITEMVALUE M1 0\r\nGETDLGITEMVALUE M2 1\r\nGETDLGITEMVALUE M3 2\r\nGETDLGITEMVALUE M4 3\r\nGETDLGITEMVALUE M5 4\r\nGETDLGITEMVALUE M9 14\r\nGETDLGITEMVALUE M1015\r\n<$STR(S2)>.GIVEEX<$DLGITEMNAME>10=<$STR(M1)>,1=<$STR(M2)>,2=<$STR(M3)>,3=<$STR(M4)>,4=<$STR(M5)>,14=<$STR(M9)>,15=<$STR(M10)>\r\n<$STR(S2)>.SENDMSG 5 你收到了友人赠送的：<$DLGITEMNAME>\r\nMOV M1 0\r\nMOV M2 0\r\nMOV M3 0\r\nMOV M4 0\r\nMOV M5 0\r\nMOV M9 0\r\nMOV M10 0\r\nTAKEDLGITEM\r\nCLOSE\r\nBREAK\r\n#IF\r\nCHECKDLGITEMTYPE BOOTS\r\nCHECKONLINE <$STR(S2)>\r\n#ACT\r\nSENDMSG 0 <$STR(S2)>收到了友人:<$USERNAME>,赠送的：<$DLGITEMNAME> 246 245\r\nGETDLGITEMVALUE M1 0\r\nGETDLGITEMVALUE M2 1\r\nGETDLGITEMVALUE M3 2\r\nGETDLGITEMVALUE M4 3\r\nGETDLGITEMVALUE M5 4\r\nGETDLGITEMVALUE M9 14\r\nGETDLGITEMVALUE M1015\r\n<$STR(S2)>.GIVEEX<$DLGITEMNAME>10=<$STR(M1)>,1=<$STR(M2)>,2=<$STR(M3)>,3=<$STR(M4)>,4=<$STR(M5)>,14=<$STR(M9)>,15=<$STR(M10)>\r\n<$STR(S2)>.SENDMSG 5 你收到了友人赠送的：<$DLGITEMNAME>\r\nMOV M1 0\r\nMOV M2 0\r\nMOV M3 0\r\nMOV M4 0\r\nMOV M5 0\r\nMOV M9 0\r\nMOV M10 0\r\nTAKEDLGITEM\r\nCLOSE\r\nBREAK\r\n#IF\r\nCHECKDLGITEMTYPE BELT\r\nCHECKONLINE <$STR(S2)>\r\n#ACT\r\nSENDMSG 0 <$STR(S2)>收到了友人:<$USERNAME>,赠送的：<$DLGITEMNAME> 246 245\r\nGETDLGITEMVALUE M1 0\r\nGETDLGITEMVALUE M2 1\r\nGETDLGITEMVALUE M3 2\r\nGETDLGITEMVALUE M4 3\r\nGETDLGITEMVALUE M5 4\r\nGETDLGITEMVALUE M9 14\r\nGETDLGITEMVALUE M1015\r\n<$STR(S2)>.GIVEEX<$DLGITEMNAME>10=<$STR(M1)>,1=<$STR(M2)>,2=<$STR(M3)>,3=<$STR(M4)>,4=<$STR(M5)>,14=<$STR(M9)>,15=<$STR(M10)>\r\n<$STR(S2)>.SENDMSG 5 你收到了友人赠送的：<$DLGITEMNAME>\r\nMOV M1 0\r\nMOV M2 0\r\nMOV M3 0\r\nMOV M4 0\r\nMOV M5 0\r\nMOV M9 0\r\nMOV M10 0\r\nTAKEDLGITEM\r\nCLOSE\r\nBREAK\r\n#IF\r\nCHECKONLINE <$STR(S2)>\r\n#ACT\r\nSENDMSG 0 <$STR(S2)>收到了友人:<$USERNAME>,赠送的：<$DLGITEMNAME> 246 245\r\n<$STR(S2)>.GIVE <$DLGITEMNAME> 1\r\n<$STR(S2)>.SENDMSG 5 你收到了友人赠送的：<$DLGITEMNAME>\r\nTAKEDLGITEM\r\nCLOSE\r\n#ELSEACT\r\nMessageBox <$STR(S2)>\\不在线,你不能赠送！\r\nCLOSE \r\n");

                    cd = new CompletionData("结婚脚本", "获取结婚脚本");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("结婚脚本", "[@main]\r\n<了解条件/@a> \\\r\n<结婚/@marry>\\\r\n<离婚/@unmarry>\\\r\n<退出/@exit>\\\r\n\r\n\r\n[@a]\r\n#SAY\r\n结婚需要4个条件:\\\r\n(1).只有双方要面对面站好才可以结婚和离婚.\\\r\n(2).只有男方才可以求婚\\\r\n(3).男方需要100W金币才可以求婚\\\r\n(4).申请结婚时求婚戒指必须带在手上\\\\\r\n<退出/@exit> \r\n\r\n[@marry]\r\n#if\r\ncheckmarry\r\n#act \r\nbreak\r\n#say\r\n你都结过婚了,还来注册结婚,想犯重婚罪呀！！！ \\ \\\r\n<返回/@main>\r\n#if\r\ncheckposemarry\r\n#say\r\n对方已经结过婚了,是不是想犯重婚罪呀！！！\\ \\\r\n<返回/@main>\r\n#act\r\nbreak\r\n#if\r\ngender man\r\n#elsesay\r\n只有男的向女的求婚,还没见过大姑娘向小伙子求婚的。\\ \\\r\n<返回/@main>\r\n#elseact\r\nbreak\r\n#if\r\ncheckposegender 男\r\n#say\r\n你变态呀！！！想搞同性恋！！！ \\ \\\r\n<返回/@main>\r\n#act\r\nbreak\r\n#if\r\ncheckposedir 2\r\n#elsesay\r\n你们二个面对面站好呀,不要乱动。\\ \\\r\n<返回/@main>\r\n#elseact\r\nbreak\r\n#if\r\ncheckgold 1000000\r\n#elsesay\r\n你连100W都没有还想结婚？！！！\\\r\n<返回/@main>\r\n#elseact\r\nbreak \r\n\r\n#if \r\ncheckitemw 求婚戒指 1\r\n#elsesay\r\n你没求婚戒指,弄到求婚戒指再来找我吧！！！\\ \\\r\n<返回/@main>\r\n#elseact\r\nbreak\r\n#if\r\n#act\r\ntakew 求婚戒指 1\r\ntake 金币 1000000\r\nmarry\r\n[@StartMarry]\r\n#if\r\ncheckmarry\r\n#say\r\n你都结过婚了,还来注册结婚,想犯重婚罪呀！！！ \\ \\\r\n<返回/@main>\r\n#act\r\nbreak\r\n#if\r\ngender man\r\n#act\r\nGMEXECUTE SHOWEFFECT 78\r\nGMEXECUTE SHOWEFFECT 79\r\nGMEXECUTE SHOWEFFECT 80\r\nGMEXECUTE SHOWEFFECT 81\r\nGMEXECUTE SHOWEFFECT 82\r\nGMEXECUTE SHOWEFFECT 83\r\nGMEXECUTE SHOWEFFECT 84\r\nbreak\r\n#say\r\n婚礼现在正式开始。\\ \\\r\n你愿意娶对方为妻,并照顾她一生一世吗？\\ \\\r\n<我愿意/@RequestMarry>\r\n#if\r\ngender\r\n#act\r\nbreak\r\n#say\r\n婚礼现在正式开始。\\ \\\r\n请耐心等待你心爱的人向你求婚\\ \\\r\n[@RequestMarry]\r\n#if\r\ncheckmarry\r\n#act\r\nbreak\r\n#say\r\n你都结过婚了,还来注册结婚,想犯重婚罪呀！！！ \\ \\\r\n<返回/@main>\r\n#if\r\n#act\r\nGMEXECUTE SHOWEFFECT 78\r\nGMEXECUTE SHOWEFFECT 79\r\nGMEXECUTE SHOWEFFECT 80\r\nGMEXECUTE SHOWEFFECT 81\r\nGMEXECUTE SHOWEFFECT 82\r\nGMEXECUTE SHOWEFFECT 83\r\nGMEXECUTE SHOWEFFECT 84\r\nmarry requestmarry\r\n[@WateMarry]\r\n你已向对方求婚,请耐心等待对方的答复。\r\n[@RevMarry]\r\n#if\r\ncheckmarry\r\n#act\r\nbreak\r\n#say\r\n你都结过婚了,还来注册结婚,想犯重婚罪呀！！！ \\ \\\r\n<返回/@main>\r\n#if\r\n#say\r\n对方向你求婚,你是否答应嫁给他？ \\ \\\r\n<我愿意/@ResposeMarry> \\\r\n<我不愿意/@ResposeMarryFail>\r\n[@ResposeMarry]\r\n#if\r\ncheckmarry\r\n#act\r\nbreak\r\n#say\r\n你都结过婚了,还来注册结婚,想犯重婚罪呀！！！ \\ \\\r\n<返回/@main>\r\n#if\r\n#act\r\nGMEXECUTE mobfireburn m101 14 38 4 360 0\r\nGMEXECUTE mobfireburn m101 14 37 4 360 0\r\nGMEXECUTE mobfireburn m101 14 36 4 360 0\r\nGMEXECUTE mobfireburn m101 14 35 4 360 0\r\nGMEXECUTE mobfireburn m101 14 34 4 360 0\r\nGMEXECUTE mobfireburn m101 15 39 4 360 0\r\nGMEXECUTE mobfireburn m101 15 38 4 360 0\r\nGMEXECUTE mobfireburn m101 15 37 4 360 0\r\nGMEXECUTE mobfireburn m101 15 36 4 360 0\r\nGMEXECUTE mobfireburn m101 15 35 4 360 0\r\nGMEXECUTE mobfireburn m101 15 33 4 360 0\r\nGMEXECUTE mobfireburn m101 16 40 4 360 0\r\nGMEXECUTE mobfireburn m101 17 41 4 360 0\r\nGMEXECUTE mobfireburn m101 18 42 4 360 0\r\nGMEXECUTE mobfireburn m101 19 43 4 360 0\r\nGMEXECUTE mobfireburn m101 20 44 4 360 0\r\nGMEXECUTE mobfireburn m101 20 43 4 360 0\r\nGMEXECUTE mobfireburn m101 20 42 4 360 0\r\nGMEXECUTE mobfireburn m101 19 42 4 360 0\r\nGMEXECUTE mobfireburn m101 18 41 4 360 0\r\nGMEXECUTE mobfireburn m101 17 40 4 360 0\r\nGMEXECUTE mobfireburn m101 16 39 4 360 0\r\nGMEXECUTE mobfireburn m101 21 43 4 360 0\r\nGMEXECUTE mobfireburn m101 22 42 4 360 0\r\nGMEXECUTE mobfireburn m101 23 41 4 360 0\r\nGMEXECUTE mobfireburn m101 24 40 4 360 0\r\nGMEXECUTE mobfireburn m101 25 39 4 360 0\r\nGMEXECUTE mobfireburn m101 24 39 4 360 0\r\nGMEXECUTE mobfireburn m101 23 40 4 360 0\r\nGMEXECUTE mobfireburn m101 22 41 4 360 0\r\nGMEXECUTE mobfireburn m101 21 42 4 360 0\r\nGMEXECUTE mobfireburn m101 25 38 4 360 0\r\nGMEXECUTE mobfireburn m101 25 37 4 360 0\r\nGMEXECUTE mobfireburn m101 25 36 4 360 0\r\nGMEXECUTE mobfireburn m101 25 35 4 360 0\r\nGMEXECUTE mobfireburn m101 25 34 4 360 0\r\nGMEXECUTE mobfireburn m101 25 33 4 360 0\r\nGMEXECUTE mobfireburn m101 26 34 4 360 0\r\nGMEXECUTE mobfireburn m101 26 35 4 360 0\r\nGMEXECUTE mobfireburn m101 26 36 4 360 0\r\nGMEXECUTE mobfireburn m101 26 37 4 360 0\r\nGMEXECUTE mobfireburn m101 26 38 4 360 0\r\nGMEXECUTE mobfireburn m101 24 33 4 360 0\r\nGMEXECUTE mobfireburn m101 23 33 4 360 0\r\nGMEXECUTE mobfireburn m101 22 33 4 360 0\r\nGMEXECUTE mobfireburn m101 21 33 4 360 0\r\nGMEXECUTE mobfireburn m101 20 33 4 360 0\r\nGMEXECUTE mobfireburn m101 19 33 4 360 0\r\nGMEXECUTE mobfireburn m101 18 33 4 360 0\r\nGMEXECUTE mobfireburn m101 17 33 4 360 0\r\nGMEXECUTE mobfireburn m101 16 33 4 360 0\r\nGMEXECUTE mobfireburn m101 15 33 4 360 0\r\nGMEXECUTE mobfireburn m101 16 32 4 360 0\r\nGMEXECUTE mobfireburn m101 17 32 4 360 0\r\nGMEXECUTE mobfireburn m101 18 32 4 360 0\r\nGMEXECUTE mobfireburn m101 19 32 4 360 0\r\nGMEXECUTE mobfireburn m101 20 32 4 360 0\r\nGMEXECUTE mobfireburn m101 21 32 4 360 0\r\nGMEXECUTE mobfireburn m101 22 32 4 360 0\r\nGMEXECUTE mobfireburn m101 23 32 4 360 0\r\nGMEXECUTE mobfireburn m101 24 32 4 360 0\r\nGMEXECUTE mobfireburn m101 19 34 4 360 0\r\nGMEXECUTE mobfireburn m101 18 35 4 360 0\r\nGMEXECUTE mobfireburn m101 17 36 4 360 0\r\nGMEXECUTE mobfireburn m101 17 37 4 360 0\r\nGMEXECUTE mobfireburn m101 18 38 4 360 0\r\nGMEXECUTE mobfireburn m101 19 39 4 360 0\r\nGMEXECUTE mobfireburn m101 20 40 4 360 0\r\nGMEXECUTE mobfireburn m101 21 39 4 360 0\r\nGMEXECUTE mobfireburn m101 22 38 4 360 0\r\nGMEXECUTE mobfireburn m101 23 37 4 360 0\r\nGMEXECUTE mobfireburn m101 22 36 4 360 0\r\nGMEXECUTE mobfireburn m101 22 35 4 360 0\r\nGMEXECUTE mobfireburn m101 21 34 4 360 0\r\nGMEXECUTE mobfireburn m101 20 34 4 360 0\r\nGMEXECUTE mobfireburn m101 18 36 4 360 0\r\nGMEXECUTE mobfireburn m101 19 36 4 360 0\r\nGMEXECUTE mobfireburn m101 20 36 4 360 0\r\nGMEXECUTE mobfireburn m101 21 36 4 360 0\r\nGMEXECUTE mobfireburn m101 22 36 4 360 0\r\nGMEXECUTE mobfireburn m101 20 37 4 360 0\r\nGMEXECUTE mobfireburn m101 19 38 4 360 0\r\nGMEXECUTE mobfireburn m101 20 38 4 360 0\r\nGMEXECUTE mobfireburn m101 21 38 4 360 0\r\nmarry responsemarry ok\r\n[@ResposeMarryFail]\r\n#if\r\ncheckmarry\r\n#act\r\nbreak\r\n#say\r\n你都结过婚了,还来注册结婚,想犯重婚罪呀！！！ \\ \\\r\n<返回/@main>\r\n#if\r\n#act\r\nmarry responsemarry fail\r\n[@EndMarry]\r\n#if\r\n#act\r\n#say\r\n你们二个已经成为了一对合法夫妻了。\\ \\\r\n让我为你们点燃爱火.相亲相爱吧!\\ \\\r\n<关闭/@exit>\r\n[@EndMarryFail]\r\n结婚失败！\\ \\\r\n<关闭/@exit>\r\n[@MarryDirErr]\r\n对方没站好位置\r\n[@MarryCheckDir]\r\n请站好位置\\ \\\r\n[@HumanTypeErr]\r\n你变态呀,既然选择一个非人类作为结婚对象。\r\n[@MarrySexErr]\r\n你变态呀,既然同性恋。\r\n;==========================================\r\n离婚开始\r\n[@unmarry]\r\n#if\r\ncheckmarry\r\n#act\r\nunmarry\r\n#elsesay\r\n你都没结婚离什么婚？？\\ \\\r\n<返回/@main>\r\n;=======================================================\r\n;双方离婚时没面对面站好显示的信息\r\n[@UnMarryCheckDir]\r\n要离婚是吧？离婚是二个人的事,必须二个人对面对站好位置,\\\r\n如果人来不了你只能选择强行离婚姻了。\\ \\\r\n<我要强行离婚/@fUnMarry>\\\r\n<返回/@main>\r\n;=======================================================\r\n;对面位置不是人物时显示的信息\r\n[@UnMarryTypeErr]\r\n你对面站了个什么东西,怎么不太象人来的。\\ \\\r\n<返回/@main>\r\n;=======================================================\r\n;开始离婚程序后,双方显示的信息\r\n[@StartUnMarry]\r\n#if\r\ngender man\r\n#act\r\nBREAK\r\n#say\r\n是否确定真的要与你共事多年的妻子离婚吗？\\ \\\r\n<确定/@RequestUnMarry>\r\n#if\r\ngender\r\n#say\r\n是否确定真的要与你共事多年的老公离婚吗？ \\ \\\r\n<确定/@RequestUnMarry>\r\n#act\r\nbreak\r\n;=======================================================\r\n;发出离婚请求\r\n[@RequestUnMarry]\r\n#if\r\ncheckmarry\r\n#act\r\nunmarry requestunmarry\r\n;=======================================================\r\n;回应离婚请求\r\n[@ResposeUnMarry]\r\n#if\r\ncheckmarry\r\n#act\r\nunmarry responseunmarry\r\n;===============================================\r\n;请求离婚后显示的信息\r\n[@WateUnMarry]\r\n你已向对方发出离婚请求,请耐心等待对方的答复。\r\n;===============================================\r\n;请求离婚后对方显示的信息\r\n[@RevUnMarry]\r\n对方向你离婚请求,你是否答应离婚？ \\ \\\r\n<我愿意/@RequestUnMarry>\r\n<返回/@main>\r\n;===============================================\r\n;没结过婚的人点离婚后出的提示信息\r\n[@ExeMarryFail]\r\n你都没结过婚,跑来做什么？ \\ \\\r\n[返回/@main]\r\n;==============================================\r\n;强行离婚\r\n[@fUnMarry]\r\n#if\r\ncheckitem 金砖 1\r\ncheckmarry\r\n#act\r\ntake 金砖 1\r\nunmarry requestunmarry force\r\n#elsesay\r\n要收一根金砖的手续费,你没有金砖,\\\r\n我不能让你离婚。\\\r\n<确定/@exit>\r\n;==============================================\r\n;离婚完成后的提示信息\r\n[@UnMarryEnd]\r\n呵呵,你已经脱离苦海了！！！ \\ \\\r\n<退出/@exit>\r\n[@asktime]\r\n你调查结婚时间的请求已发出,\\\r\n请稍后。\\\r\n<确定/@exit>\r\n\r\n\r\n#if\r\ncheckgold 1000000\r\n#elsesay\r\n你连100W金币都没有还想结婚？！！！\r\n<返回/@main>\r\n#elseact\r\nbreak \r\n");

                    cd = new CompletionData("自由加点脚本", "获取自由加点脚本");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("自由加点脚本", "[@AdjusBonus]\r\n恭喜：你已经到了下一个等级了,请选择你想提高的能力：\\\r\nDC: <$DC>-<$MAXDC> <$BONUSABIL_DC>/<$BONUSTICK_DC> <+/@AddDC> <-/@decDC> HP: <$HP>-<$MAXHP> <$BONUSABIL_HP>/<$BONUSTICK_HP> <+/@AddHP> <-/@decHP>\\\r\nMC: <$MC>-<$MAXMC> <$BONUSABIL_MC>/<$BONUSTICK_MC> <+/@AddMC> <-/@decMC> MP: <$MP>-<$MAXMP> <$BONUSABIL_MP>/<$BONUSTICK_MP> <+/@AddMP> <-/@decMP>\\\r\nSC: <$MC>-<$MAXSC> <$BONUSABIL_SC>/<$BONUSTICK_SC> <+/@AddSC> <-/@decSC> AC: <$MC>-<$MAXAC> <$BONUSABIL_AC>/<$BONUSTICK_AC> <+/@AddAC> <-/@decAC>\\\r\nMAC: <$MAC>-<$MAXMAC> <$BONUSABIL_MAC>/<$BONUSTICK_MAC> <+/@AddMAC> <-/@decMAC>\\\r\nHIT: <$HIT>-<$HIT> <$BONUSABIL_HIT>/<$BONUSTICK_HIT> <+/@AddHIT> <-/@decHIT>\\\r\nSPD: <$SPD>-<$SPD> <$BONUSABIL_SPD>/<$BONUSTICK_SPD> <+/@AddSPD> <-/@decSPD>\\\r\n剩余点数：<$BONUSPOINT> <重新分配/@RestBonus> <返回/@main>\\ \r\n\r\n[@RestBonus]\r\n#ACT\r\nRESTBONUSPOINT\r\ngoto @AdjusBonus \r\n\r\n[@AddDC]\r\n#ACT\r\nBONUSABIL DC + 1\r\ngoto @AdjusBonus \r\n\r\n[@DecDC]\r\n#ACT\r\nBONUSABIL DC - 1\r\ngoto @AdjusBonus\r\n\r\n\r\n[@AddMC]\r\n#ACT\r\nBONUSABIL MC + 1\r\ngoto @AdjusBonus \r\n\r\n[@DecMC]\r\n#ACT\r\nBONUSABIL MC - 1\r\ngoto @AdjusBonus \r\n\r\n[@AddSC]\r\n#ACT\r\nBONUSABIL SC + 1\r\ngoto @AdjusBonus \r\n\r\n[@DecSC]\r\n#ACT\r\nBONUSABIL SC - 1\r\ngoto @AdjusBonus \r\n\r\n[@AddAC]\r\n#ACT\r\nBONUSABIL AC + 1\r\ngoto @AdjusBonus \r\n\r\n[@DecAC]\r\n#ACT\r\nBONUSABIL AC - 1\r\ngoto @AdjusBonus \r\n\r\n[@AddMAC]\r\n#ACT\r\nBONUSABIL MAC + 1\r\ngoto @AdjusBonus \r\n\r\n[@DecMAC]\r\n#ACT\r\nBONUSABIL MAC - 1\r\ngoto @AdjusBonus \r\n\r\n[@AddHP]\r\n#ACT\r\nBONUSABIL HP + 1\r\ngoto @AdjusBonus \r\n\r\n[@DecHP]\r\n#ACT\r\nBONUSABIL HP - 1\r\ngoto @AdjusBonus \r\n\r\n[@AddMP]\r\n#ACT\r\nBONUSABIL MP + 1\r\ngoto @AdjusBonus \r\n\r\n[@DecMP]\r\n#ACT\r\nBONUSABIL MP - 1\r\ngoto @AdjusBonus \r\n\r\n[@AddHIT]\r\n#ACT\r\nBONUSABIL HIT + 1\r\ngoto @AdjusBonus \r\n\r\n[@DecHIT]\r\n#ACT\r\nBONUSABIL HIT - 1\r\ngoto @AdjusBonus \r\n\r\n[@AddSPD]\r\n#ACT\r\nBONUSABIL SPD + 1\r\ngoto @AdjusBonus \r\n\r\n[@DecSPD]\r\n#ACT\r\nBONUSABIL SPD - 1\r\ngoto @AdjusBonus \r\n");

                    cd = new CompletionData("账号更名脚本", "获取账号更名脚本");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("账号更名脚本", "(@@inputstring)\r\n\r\n[@main]\r\n#IF\r\n#say\r\n变更角色名条件：\\\r\n1、对于人物,行会成员需要先退出行会；需要回收英雄。\\\r\n2、对于英雄,需要召唤出英雄。\\\r\n<更换我的角色名/@@InputString5>  <更换英雄的名字/@@InputString6>\\ \\\r\n<关闭/@exit>\r\n\r\n \r\n\r\n[@@InPutString5]\r\n#if\r\nhaveGuild\r\n#say\r\n失败,行会成员不能变更角色名！\\ \\\r\n<关闭/@exit>\r\n#act\r\nsendmsg 0 行会成员不能变更角色名！\r\nbreak\r\n\r\n#if\r\nhaveHero\r\ncheckHeroOnline\r\n#say\r\n失败,英雄需下线才能更名！\\ \\\r\n<关闭/@exit>\r\n#act\r\nsendmsg 0 英雄需下线才能更名！\r\nbreak\r\n\r\n#if\r\n#act\r\nchangeCharName <$STR(S5)>\r\n\r\n[@@InPutString6]\r\n#if\r\n!haveHero\r\n#act\r\nsendmsg 0 你还没有英雄！\r\nbreak\r\n\r\n#if\r\n!checkHeroOnline\r\n#act\r\nsendmsg 0 失败,英雄在线才能更名！\r\nbreak\r\n\r\n#if\r\n#act\r\nh.changeCharName <$STR(S6)>\r\n\r\n; 下面为改名结果调用,<$PARAMSTR(0)>:“[英雄] ”,如果非英雄改名则为空\r\n[@chgname_success]\r\n#IF\r\nCOMPVAL <$PARAMSTR(0)> = \r\n#act\r\n;在这里<$PARAMSTR(1)>:原角色名,<$PARAMSTR(2)>:新角色名\r\n;执行 一些包含了角色名文本的重命名操作...（可以使用StringsReplace和INI_RENAME_SECTION命令来操作）\r\nStringsReplace ..\\QuestDiary\\rename.txt <$PARAMSTR(1)> <$PARAMSTR(2)> 0 HardDisk\r\nsendmsg 0 人物\r\n#elseACT\r\nsendmsg 0 英雄\r\n\r\n\r\n[@chgname_fail_name_used]\r\n#say\r\n<$PARAMSTR(0)>失败,新角色名已被使用！\\ \\\r\n<关闭/@exit>\r\n\r\n[@chgname_fail_name_wrong_format]\r\n#say\r\n<$PARAMSTR(0)>失败,新角色名包含了过滤字符！\\ \\\r\n<关闭/@exit>\r\n\r\n[@chgname_fail_no_character]\r\n#say\r\n<$PARAMSTR(0)>失败,查询不到角色！\\ \\\r\n<关闭/@exit>\r\n\r\n[@chgname_fail_incomplete_data]\r\n#say\r\n<$PARAMSTR(0)>失败,数据错误！\\ \\\r\n<关闭/@exit>\r\n\r\n[@chgname_fail_unknow]\r\n#say\r\n<$PARAMSTR(0)>失败,未知错误！\\ \\\r\n<关闭/@exit>\r\n\r\n[@IsInFilterList]\r\n#say\r\n失败,输入文字中包含了过滤字符！\\ \\\r\n<关闭/@exit>\r\n\r\n\r\n[@chgname_fail_wrong_format]\r\n#say\r\n失败,输入文字中包含了过滤字符！\\ \\\r\n<关闭/@exit>\r\n");

                    cd = new CompletionData("装备属性转移脚本", "获取装备属性转移脚本");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("装备属性转移脚本", "[@main]\r\n<获取并修改武器属性示例/@GetEquipProps> \\ \\\r\n<关闭/@exit>\r\n\r\n[@GetEquipProps]\r\n; 检测并修改武器的属性\r\n#if\r\nCheckUseItem 1 \r\n#act\r\nMov S_Eval_Base_Prop \"\"\r\nMov S_Eval_Myst_Prop \"\"\r\nMov M_Eval_Spec_Prop 0\r\nMov M_Eval_Skill_Prop 0\r\nMov M_Eval_Times_Prop 0\r\nGetEquipProps 1\r\n\r\n[@OnGetEquipProps]\r\n; ------------------------------------------------------------------\r\n; StrTok 可以分解这样的字符：\r\n#act\r\nSendMsg 5 分解字符串例子...\r\nMovS_Eval_Props1=11,2=12,3=13,9=15|6=11,10=12,11=13,12=15|67|127|2\r\nStrTok S_Eval_Props S_LeftStr1 |\r\nStrTok S_Eval_Props S_LeftStr2 |\r\nStrTok S_Eval_Props S_LeftStr3 |\r\nStrTok S_Eval_Props S_LeftStr4 |\r\nStrTok S_Eval_Props S_LeftStr5 |\r\nSendMsg 5 \"先按“|”分解出：<$STR(S_LeftStr1)>,  <$STR(S_LeftStr2)>,  <$STR(S_LeftStr3)>,  <$STR(S_LeftStr4)>,  <$STR(S_LeftStr5)>\"\r\n\r\n; 分解S_LeftStr1,其他S_LeftStrXX方法一致\r\nStrTok S_LeftStr1 S_PropVal1 ,\r\nStrTok S_LeftStr1 S_PropVal2 ,\r\nStrTok S_LeftStr1 S_PropVal3 ,\r\nStrTok S_LeftStr1 S_PropVal4 ,\r\nSendMsg 5 \"再按“,”分解出：<$STR(S_PropVal1)>,  <$STR(S_PropVal2)>,  <$STR(S_PropVal3)>,  <$STR(S_PropVal4)>\"\r\n\r\nStrTok S_PropVal1 S_Prop1 =\r\nStrTok S_PropVal2 S_Prop2 =\r\nStrTok S_PropVal3 S_Prop3 =\r\nStrTokS_PropVal4S_Prop4=<BR>SendMsg5\"得到属性<$STR(S_Prop1)>=<$STR(S_PropVal1)>,<$STR(S_Prop2)>=<$STR(S_PropVal2)>,<$STR(S_Prop3)>=<$STR(S_PropVal3)>,<$STR(S_Prop4)>=<$STR(S_PropVal4)>\"\r\n\r\n; ------------------------------------------------------------------\r\n#if\r\ntrue\r\n#act\r\n; 获取鉴定特殊属性到M_Eval_Spec_Prop\r\nFormatStr <$STR(M_EQUIP[%s]_EVAL_SPEC_PROPS{0})%s <$PARAM(3)> >\r\nMov M_Eval_Spec_Prop <$CALCRESULT>\r\n\r\n; 更改鉴定特殊属性,可以使用#Call简化脚本\r\n#if\r\nIsBitSet M_Eval_Spec_Prop 0\r\n#act\r\nSendMsg 5 \"拥有特殊属性：八卦护身神技\"\r\n; 删除掉该技能\r\nUnSetBit M_Eval_Spec_Prop 0\r\n\r\n#if\r\nIsBitSet M_Eval_Spec_Prop 1\r\n#act\r\nSendMsg 5 \"拥有特殊属性：战意麻痹神技\"\r\n; 删除掉该技能\r\nUnSetBit M_Eval_Spec_Prop 1\r\n\r\n#if\r\nIsBitSet M_Eval_Spec_Prop 2\r\n#act\r\nSendMsg 5 \"拥有特殊属性：重生神技\"\r\n; 删除掉该技能\r\nUnSetBit M_Eval_Spec_Prop 2\r\n\r\n#if\r\nIsBitSet M_Eval_Spec_Prop 3\r\n#act\r\nSendMsg 5 \"拥有特殊属性：探测神技\"\r\n; 删除掉该技能\r\nUnSetBit M_Eval_Spec_Prop 3\r\n\r\n#if\r\nIsBitSet M_Eval_Spec_Prop 4\r\n#act\r\nSendMsg 5 \"拥有特殊属性：传送神技\"\r\n; 删除掉该技能\r\nUnSetBit M_Eval_Spec_Prop 4\r\n\r\n#if\r\nIsBitSet M_Eval_Spec_Prop 5\r\n#act\r\nSendMsg 5 \"拥有特殊属性：麻痹神技\"\r\n#elseact\r\n; 没有“麻痹神技”？那就设置一个“麻痹神技”,然后可以将M_Eval_Spec_Prop的值回设给装备,即拥有“麻痹神技”\r\nSetBit M_Eval_Spec_Prop 5\r\n\r\n#if\r\n!IsBitSet M_Eval_Spec_Prop 6\r\n#act\r\nSendMsg 5 \"赋予特殊属性：魔道麻痹神技\"\r\nSetBit M_Eval_Spec_Prop 6\r\n\r\n\r\n; ------------------------------------------------------------------\r\n; 获取鉴定神技属性到M_Eval_Skill_Prop\r\nFormatStr <$STR(M_EQUIP[%s]_EVAL_SKILL_PROPS{0})%s <$PARAM(3)> >\r\nMov M_Eval_Skill_Prop <$CALCRESULT> \r\n\r\n; 如果是武器,是拥有技能,其他非武器类装备是技能等级+1\r\n#if\r\nIsBitSet M_Eval_Skill_Prop 0\r\n#act\r\nSendMsg 5 \"拥有神技：五岳独尊特技\"\r\n; 删除掉该技能\r\nUnSetBit M_Eval_Skill_Prop 0 \r\n\r\n#if\r\nIsBitSet M_Eval_Skill_Prop 1\r\n#act\r\nSendMsg 5 \"拥有神技：召唤巨魔特技\"\r\n; 删除掉该技能\r\nUnSetBit M_Eval_Skill_Prop 1 \r\n\r\n#if\r\nIsBitSet M_Eval_Skill_Prop 2\r\n#act\r\nSendMsg 5 \"拥有神技：神龙附体特技\"\r\n; 删除掉该技能\r\nUnSetBit M_Eval_Skill_Prop 2 \r\n\r\n#if\r\nIsBitSet M_Eval_Skill_Prop 3\r\n#act\r\nSendMsg 5 \"拥有神技：倚天劈地特技\" \r\n\r\n\r\n; ------------------------------------------------------------------\r\nFormatStr <$STR(M_EQUIP[%s]_EVAL_TIMES_PROPS{0})%s <$PARAM(3)> >\r\nMov M_Eval_Times_Prop <$CALCRESULT> \r\n\r\n \r\n\r\n; ------------------------------------------------------------------\r\n#if\r\n; 升级身上装备,只是特殊技能\r\n!CompVal <$PARAM(3)> = 10000\r\n#act\r\n; 故意留了空格,防止分解字符串是出现问题\r\nFormatStr \"%s |%s |%s |%s | %s\" $STR(S_Eval_Base_Prop) $STR(S_Eval_Myst_Prop) $STR(M_Eval_Spec_Prop) $STR(M_Eval_Skill_Prop) $STR(M_Eval_Times_Prop)\r\nSendMsg 5 \"即将设置的鉴定属性：<$CALCRESULT>\"\r\n; 第5参数本来处理失败的结果,现在扩展为可以设置99,即忽略普通属性的升级,这里只升级神秘属性\r\nUpgradeItemEx <$PARAM(3)> 11 1 1 99 \"\" 1 <$CALCRESULT>\r\n; 升级自定义装备框的装备,略...\r\n;#elseact\r\n;UpgradeDlgItem\r\n");

                    cd = new CompletionData("人物变量会员类型", "新增的人物变量\n<$MEMBRETYPE>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("人物变量会员类型", "<$MEMBRETYPE>");

                    cd = new CompletionData("人物变量会员等级", "新增的人物变量\n<$MEMBRELEVEL>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("人物变量会员等级", "<$MEMBRELEVEL>");

                    cd = new CompletionData("英雄变量", "全局变量跟人物一致\r\n<$H.STR(P/G/D/M/I/S/H)>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("英雄变量", "<$H.STR(P/G/D/M/I/S/H)>");

                    cd = new CompletionData("英雄自由变量", "<$H.HUMAN()>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("英雄自由变量", "<$H.HUMAN()>");

                    cd = new CompletionData("英雄会员类型变量", "<$H.MEMBRETYPE>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("英雄会员类型变量", "<$H.MEMBRETYPE>");

                    cd = new CompletionData("英雄会员等级变量", "<$H.MEMBRELEVEL>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("英雄会员等级变量", "<$H.MEMBRELEVEL>");

                    cd = new CompletionData("杀死英雄的怪物名称变量", "<$H.MONKILLER>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("杀死英雄的怪物名称变量", "<$H.MONKILLER>");

                    cd = new CompletionData("杀死英雄的人物名称变量", "<$H.DECEDENT>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("杀死英雄的人物名称变量", "<$H.DECEDENT>");

                    cd = new CompletionData("英雄被杀者名称变量", "<$H.RELEVEL>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("英雄被杀者名称变量", "<$H.RELEVEL>");

                    cd = new CompletionData("英雄人物名字全称变量", "英雄人物名字全称(可包含行会封号,结婚对象,师徒名……等等)\r\n<$H.HUMANSHOWNAME>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("英雄人物名字全称变量", "<$H.HUMANSHOWNAME>");

                    cd = new CompletionData("英雄所在地图设置名称变量", "<$H.CURRENTMAPDESC>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("英雄所在地图设置名称变量", "<$H.CURRENTMAPDESC>");

                    cd = new CompletionData("英雄所在地图文件名称变量", "<$H.CURRENTMAP>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("英雄所在地图文件名称变量", "<$H.CURRENTMAP>");

                    cd = new CompletionData("英雄所在坐标X变量", "<$H.CURRENTX>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("英雄所在坐标X变量", "<$H.CURRENTX>");

                    cd = new CompletionData("英雄所在坐标Y变量", "<$H.CURRENTY>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("英雄所在坐标Y变量", "<$H.CURRENTY>");

                    cd = new CompletionData("英雄性别变量", "<$H.GENDER>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("英雄性别变量", "<$H.GENDER>");

                    cd = new CompletionData("英雄职业变量", "<$H.JOB>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("英雄职业变量", "<$H.JOB>");

                    cd = new CompletionData("英雄名字变量", "<$H.USERNAME>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("英雄名字变量", "<$H.USERNAME>");

                    cd = new CompletionData("英雄级别变量", "<$H.LEVEL>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("英雄级别变量", "<$H.LEVEL>");

                    cd = new CompletionData("英雄当前生命值变量", "<$H.HP>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("英雄当前生命值变量", "<$H.HP>");

                    cd = new CompletionData("英雄最高生命值变量", "<$H.MAXHP>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("英雄最高生命值变量", "<$H.MAXHP>");

                    cd = new CompletionData("英雄魔法值变量", "<$H.MP>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("英雄魔法值变量", "<$H.MP>");

                    cd = new CompletionData("英雄最高魔法值变量", "<$H.MAXMP>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("英雄最高魔法值变量", "<$H.MAXMP>");

                    cd = new CompletionData("英雄防御变量", "<$H.AC>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("英雄防御变量", "<$H.AC>");

                    cd = new CompletionData("英雄最高防御变量", "<$H.MAXAC>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("英雄最高防御变量", "<$H.MAXAC>");

                    cd = new CompletionData("英雄魔御变量", "<$H.MAC>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("英雄魔御变量", "<$H.MAC>");

                    cd = new CompletionData("英雄最高魔御变量", "<$H.MAXMAC>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("英雄最高魔御变量", "<$H.MAXMAC>");

                    cd = new CompletionData("英雄攻击变量", "<$H.DC>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("英雄攻击变量", "<$H.DC>");

                    cd = new CompletionData("英雄最高攻击变量", "<$H.MAXDC>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("英雄最高攻击变量", "<$H.MAXDC>");

                    cd = new CompletionData("英雄魔法变量", "<$H.MC>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("英雄魔法变量", "<$H.MC>");

                    cd = new CompletionData("英雄最高魔法变量", "<$H.MAXMC>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("英雄最高魔法变量", "<$H.MAXMC>");

                    cd = new CompletionData("英雄道术变量", "<$H.SC>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("英雄道术变量", "<$H.SC>");

                    cd = new CompletionData("英雄最高道术变量", "<$H.MAXSC>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("英雄最高道术变量", "<$H.MAXSC>");

                    cd = new CompletionData("英雄准确变量", "<$H.HIT>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("英雄准确变量", "<$H.HIT>");

                    cd = new CompletionData("英雄躲避率变量", "<$H.SPD>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("英雄躲避率变量", "<$H.SPD>");

                    cd = new CompletionData("英雄幸运值变量", "<$H.LUCKPOINT>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("英雄幸运值变量", "<$H.LUCKPOINT>");

                    cd = new CompletionData("英雄当前经验变量", "<$H.EXP>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("英雄当前经验变量", "<$H.EXP>");

                    cd = new CompletionData("英雄升级经验值变量", "<$H.MAXEXP>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("英雄升级经验值变量", "<$H.MAXEXP>");

                    cd = new CompletionData("英雄PK点数变量", "<$H.PKPOINT>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("英雄PK点数变量", "<$H.PKPOINT>");

                    cd = new CompletionData("英雄声望点数变量", "<$H.CREDITPOINT>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("英雄声望点数变量", "<$H.CREDITPOINT>");

                    cd = new CompletionData("英雄荣誉值变量", "<$H.HEROCREDITPOINT>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("英雄荣誉值变量", "<$H.HEROCREDITPOINT>");

                    cd = new CompletionData("英雄腕力变量", "<$H.HW>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("英雄腕力变量", "<$H.HW>");

                    cd = new CompletionData("英雄最高腕力变量", "<$H.MAXHW>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("英雄最高腕力变量", "<$H.MAXHW>");

                    cd = new CompletionData("英雄背包重量变量", "<$H.BW>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("英雄背包重量变量", "<$H.BW>");

                    cd = new CompletionData("英雄最高背包重量变量", "<$H.MAXBW>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("英雄最高背包重量变量", "<$H.MAXBW>");

                    cd = new CompletionData("英雄负重力变量", "<$H.WW>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("英雄负重力变量", "<$H.WW>");

                    cd = new CompletionData("英雄最高负重变量", "<$H.MAXWW>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("英雄最高负重变量", "<$H.MAXWW>");

                    cd = new CompletionData("英雄金币变量", "<$H.GOLDCOUNT>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("英雄金币变量", "<$H.GOLDCOUNT>");

                    cd = new CompletionData("英雄元宝变量", "<$H.GAMEGOLD>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("英雄元宝变量", "<$H.GAMEGOLD>");

                    cd = new CompletionData("英雄灵气值变量", "<$H.NIMBUS>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("英雄灵气值变量", "<$H.NIMBUS>");

                    cd = new CompletionData("英雄英雄灵气值变量", "<$H.NIMBUS>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("英雄英雄灵气值变量", "<$H.NIMBUS>");

                    cd = new CompletionData("英雄游戏点变量", "<$H.GAMEPOINT>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("英雄游戏点变量", "<$H.GAMEPOINT>");

                    cd = new CompletionData("英雄金刚石数变量", "<$H.GAMEDIAMOND>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("英雄金刚石数变量", "<$H.GAMEDIAMOND>");

                    cd = new CompletionData("英雄灵符变量", "<$H.GAMEGIRD>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("英雄灵符变量", "<$H.GAMEGIRD>");

                    cd = new CompletionData("英雄饥饿程度变量", "<$H.HUNGER>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("英雄饥饿程度变量", "<$H.HUNGER>");

                    cd = new CompletionData("英雄登录时间变量", "<$H.LOGINTIME>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("英雄登录时间变量", "<$H.LOGINTIME>");

                    cd = new CompletionData("英雄登录时长变量", "<$H.LOGINLONG>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("英雄登录时长变量", "<$H.LOGINLONG>");

                    cd = new CompletionData("变量英雄身上衣服名称,下同", "<$H.DRESS>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量英雄身上衣服名称,下同", "<$H.DRESS>");

                    cd = new CompletionData("英雄身上武器名称变量", "<$H.WEAPON>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("英雄身上武器名称变量", "<$H.WEAPON>");

                    cd = new CompletionData("英雄蜡烛变量", "<$H.RIGHTHAND>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("英雄蜡烛变量", "<$H.RIGHTHAND>");

                    cd = new CompletionData("英雄头盔变量", "<$H.HELMET>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("英雄头盔变量", "<$H.HELMET>");

                    cd = new CompletionData("英雄斗笠变量", "<$H.HELMETEX>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("英雄斗笠变量", "<$H.HELMETEX>");

                    cd = new CompletionData("英雄项链变量", "<$H.NECKLACE>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("英雄项链变量", "<$H.NECKLACE>");

                    cd = new CompletionData("英雄戒指右变量", "<$H.RING_R>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("英雄戒指右变量", "<$H.RING_R>");

                    cd = new CompletionData("英雄戒指左变量", "<$H.RING_L>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("英雄戒指左变量", "<$H.RING_L>");

                    cd = new CompletionData("英雄手镯右变量", "<$H.ARMRING_R>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("英雄手镯右变量", "<$H.ARMRING_R>");

                    cd = new CompletionData("英雄手镯左变量", "<$H.ARMRING_L>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("英雄手镯左变量", "<$H.ARMRING_L>");

                    cd = new CompletionData("英雄宝石变量", "<$H.BUJUK>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("英雄宝石变量", "<$H.BUJUK>");

                    cd = new CompletionData("英雄腰带变量", "<$H.BELT>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("英雄腰带变量", "<$H.BELT>");

                    cd = new CompletionData("英雄鞋子变量", "<$H.BOOTS>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("英雄鞋子变量", "<$H.BOOTS>");

                    cd = new CompletionData("英雄符毒变量", "<$H.CHARM>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("英雄符毒变量", "<$H.CHARM>");

                    cd = new CompletionData("英雄IP地址变量", "<$H.IPADDR>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("英雄IP地址变量", "<$H.IPADDR>");

                    cd = new CompletionData("英雄IP地区信息变量", "英雄IP地区信息 如：来自于[<$H.IPLOCAL>]的玩家[<$H.USERNAME>]先生进入了游戏..\n<$H.IPLOCAL>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("英雄IP地区信息变量", "<$H.IPLOCAL>");

                    cd = new CompletionData("英雄个人变量变量", "<$H.INTS 0~29>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("英雄个人变量变量", "<$H.INTS 0~29>");

                    cd = new CompletionData("变量英雄扣除操作一", "支持扣除操作,其中的全局变量不要频繁使用,否则造成服务器卡顿\n<$H.STR(XX)>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量英雄扣除操作一", "<$H.STR(XX)>");

                    cd = new CompletionData("变量英雄扣除操作二", "支持扣除操作,定义为内存操作将会自动保存,否则不自动保存；此变量不要高频使用,否则有效率问题\n<$H.HUMAN(XX)>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量英雄扣除操作二", "<$H.HUMAN(XX)>");

                    cd = new CompletionData("变量英雄HP附加点数", "<$H.ABILITYADDPOINT0>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量英雄HP附加点数", "<$H.ABILITYADDPOINT0>");

                    cd = new CompletionData("变量英雄MP附加点数", "<$H.ABILITYADDPOINT1>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量英雄MP附加点数", "<$H.ABILITYADDPOINT1>");

                    cd = new CompletionData("变量英雄防御附加点数", "<$H.ABILITYADDPOINT2>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量英雄防御附加点数", "<$H.ABILITYADDPOINT2>");

                    cd = new CompletionData("变量英雄魔御附加点数", "<$H.ABILITYADDPOINT3>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量英雄魔御附加点数", "<$H.ABILITYADDPOINT3>");

                    cd = new CompletionData("变量英雄攻击附加点数", "<$H.ABILITYADDPOINT4>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量英雄攻击附加点数", "<$H.ABILITYADDPOINT4>");

                    cd = new CompletionData("变量英雄魔法附加点数", "<$H.ABILITYADDPOINT5>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量英雄魔法附加点数", "<$H.ABILITYADDPOINT5>");

                    cd = new CompletionData("变量英雄道术附加点数", "<$H.ABILITYADDPOINT6>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量英雄道术附加点数", "<$H.ABILITYADDPOINT6>");

                    cd = new CompletionData("变量英雄HP附加点数时间", "<$H.ABILITYADDTIME0>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量英雄HP附加点数时间", "<$H.ABILITYADDTIME0>");

                    cd = new CompletionData("变量英雄MP附加点数时间", "<$H.ABILITYADDTIME1>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量英雄MP附加点数时间", "<$H.ABILITYADDTIME1>");

                    cd = new CompletionData("变量英雄防御附加点数时间", "<$H.ABILITYADDTIME2>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量英雄防御附加点数时间", "<$H.ABILITYADDTIME2>");

                    cd = new CompletionData("变量英雄魔御附加点数时间", "<$H.ABILITYADDTIME3>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量英雄魔御附加点数时间", "<$H.ABILITYADDTIME3>");

                    cd = new CompletionData("变量英雄攻击附加点数时间", "<$H.ABILITYADDTIME4>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量英雄攻击附加点数时间", "<$H.ABILITYADDTIME4>");

                    cd = new CompletionData("变量英雄魔法附加点数时间", "<$H.ABILITYADDTIME5>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量英雄魔法附加点数时间", "<$H.ABILITYADDTIME5>");

                    cd = new CompletionData("变量英雄道术附加点数时间", "<$H.ABILITYADDTIME6>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量英雄道术附加点数时间", "<$H.ABILITYADDTIME6>");

                    cd = new CompletionData("攻击触发专属变量目标Race", "<$TARINFO_RACE>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("攻击触发专属变量目标Race", "<$TARINFO_RACE>");

                    cd = new CompletionData("攻击触发专属变量是否英雄", "目标是否英雄 {'0', '1'}\n<$TARINFO_ISHERO>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("攻击触发专属变量是否英雄", "<$TARINFO_ISHERO>");

                    cd = new CompletionData("攻击触发专属变量目标名称", "<$TARINFO_NAME>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("攻击触发专属变量目标名称", "<$TARINFO_NAME>");

                    cd = new CompletionData("攻击触发专属变量目标全名", "<$TARINFO_FNAME>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("攻击触发专属变量目标全名", "<$TARINFO_FNAME>");

                    cd = new CompletionData("攻击触发专属变量攻击使用技能ID", "<$ATTINFO_SKILLID>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("攻击触发专属变量攻击使用技能ID", "<$ATTINFO_SKILLID>");

                    cd = new CompletionData("攻击触发专属变量攻击者Race", "<$ATTINFO_RACE>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("攻击触发专属变量攻击者Race", "<$ATTINFO_RACE>");

                    cd = new CompletionData("攻击触发专属变量攻击者是否英雄", "<$ATTINFO_ISHERO>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("攻击触发专属变量攻击者是否英雄", "<$ATTINFO_ISHERO>");

                    cd = new CompletionData("攻击触发专属变量攻击者名称", "<$ATTINFO_NAME>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("攻击触发专属变量攻击者名称", "<$ATTINFO_NAME>");

                    cd = new CompletionData("攻击触发专属变量攻击者全名", "<$ATTINFO_FNAME>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("攻击触发专属变量攻击者全名", "<$ATTINFO_FNAME>");

                    cd = new CompletionData("目标变量所在地图设置名称", "<$T.CURRENTMAPDESC>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("目标变量所在地图设置名称", "<$T.CURRENTMAPDESC>");

                    cd = new CompletionData("目标变量所在地图文件名称", "<$T.CURRENTMAP>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("目标变量所在地图文件名称", "<$T.CURRENTMAP>");

                    cd = new CompletionData("目标变量所在坐标X", "<$T.CURRENTX>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("目标变量所在坐标X", "<$T.CURRENTX>");

                    cd = new CompletionData("目标变量所在坐标Y", "<$T.CURRENTY>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("目标变量所在坐标Y", "<$T.CURRENTY>");

                    cd = new CompletionData("目标变量名称", "<$T.USERNAME>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("目标变量名称", "<$T.USERNAME>");

                    cd = new CompletionData("目标变量怪物名称", "<$T.MONKILLER>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("目标变量怪物名称", "<$T.MONKILLER>");

                    cd = new CompletionData("目标变量杀死怪物名称", "<$T.KILLER>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("目标变量杀死怪物名称", "<$T.KILLER>");

                    cd = new CompletionData("目标变量级别", "<$T.LEVEL>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("目标变量级别", "<$T.LEVEL>");

                    cd = new CompletionData("目标变量生命值", "<$T.HP>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("目标变量生命值", "<$T.HP>");

                    cd = new CompletionData("目标变量最大生命值", "<$T.MAXHP>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("目标变量最大生命值", "<$T.MAXHP>");

                    cd = new CompletionData("目标变量魔法值", "<$T.MP>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("目标变量魔法值", "<$T.MP>");

                    cd = new CompletionData("目标变量最大魔法值", "<$T.MAXMP>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("目标变量最大魔法值", "<$T.MAXMP>");

                    cd = new CompletionData("目标变量防御", "<$T.AC>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("目标变量防御", "<$T.AC>");

                    cd = new CompletionData("目标变量最高防御", "<$T.MAXAC>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("目标变量最高防御", "<$T.MAXAC>");

                    cd = new CompletionData("目标变量魔御", "<$T.MAC>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("目标变量魔御", "<$T.MAC>");

                    cd = new CompletionData("目标变量最高魔御", "<$T.MAXMAC>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("目标变量最高魔御", "<$T.MAXMAC>");

                    cd = new CompletionData("目标变量攻击力", "<$T.DC>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("目标变量攻击力", "<$T.DC>");

                    cd = new CompletionData("目标变量最高攻击力", "<$T.MAXDC>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("目标变量最高攻击力", "<$T.MAXDC>");

                    cd = new CompletionData("目标变量魔法", "<$T.MC>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("目标变量魔法", "<$T.MC>");

                    cd = new CompletionData("目标变量最高魔法", "<$T.MAXMC>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("目标变量最高魔法", "<$T.MAXMC>");

                    cd = new CompletionData("目标变量道术", "<$T.SC>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("目标变量道术", "<$T.SC>");

                    cd = new CompletionData("目标变量最高道术", "<$T.MAXSC>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("目标变量最高道术", "<$T.MAXSC>");

                    cd = new CompletionData("目标变量准确", "<$T.HIT>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("目标变量准确", "<$T.HIT>");

                    cd = new CompletionData("目标变量躲避率", "<$T.SPD>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("目标变量躲避率", "<$T.SPD>");

                    cd = new CompletionData("变量类型说明一", "仅获取当前说明内容,跟您写的代码没有任何关系");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量类型说明一", "P0-P9  私人变量(数字型) 不可保存 转换NPC或关闭对话框变量为0\r\nD0-D99 私人变量(数字型) 不可保存\r\nM0-M99 私人变量(数字型) 不可保存\r\nI0-I99 全局变量(数字型) 不可保存 重起服务器后自动为0 \r\nS0-S99 私人变量(字符型) 不可保存\r\nA0-A99 全局变量(字符型) 可保存\r\nH0-H99 全局变量(数字型) 可保存\r\nG0-G99 全局变量(数字型) 可保存\r\nINTS 0~29 私人变量(整数型) 可保存 (可以使用<$INTS0~29>获取和显示,英雄以<$H.INTS0~29>获取和显示)\r\nINTS 0~29 在记录日志在元宝类型中,搜索日志以关键字“整数”进行过滤");

                    cd = new CompletionData("变量扩展一说明", "仅获取当前说明内容,跟您写的代码没有任何关系");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量扩展一说明", "使用个人M、S变量在超出常规范围(0~99)时会自动扩展,M、S后面可以是任意字符,字符的长度最好保持在20字节以内,注：SUM命令对于自由扩展的M变量,结果放在：M99999999。\r\n例如：Mov M测试整数1 12345,Mov S测试字符1 字符串1,显示：<$STR(M测试整数1)>,<$STR(S测试字符1)>");

                    cd = new CompletionData("变量扩展二说明", "仅获取当前说明内容,跟您写的代码没有任何关系");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量扩展二说明", "使用全局G、A变量在超出常规范围(0~99)时会自动扩展,服务器关闭可保存,注：SUM命令对于自由扩展的G变量,结果放在：G99999999,用法同上。 \r\n\r\n增加：属于地图的整数变量E、字符串变量F,地图指向执行命令玩家所在的地图,服务器关闭不保存,注：SUM命令对于E变量,结果放在：E99999999,用法同上。 \r\n\r\n全局变量不再保存在!setup.txt,改为保存到QuestParams.ini \r\n\r\n数值支持到21E,比如：\r\n\r\nMOV G11 2147483647 \r\n");

                    cd = new CompletionData("变量类型说明二", "仅获取当前说明内容,跟您写的代码没有任何关系");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量类型说明二", "equal、large、small、isbitset支持解释嵌套变量,例如：\r\n  ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\r\n  #act\r\n  mov M80 12345\r\n  mov S14 M80\r\n  #if\r\n  equal <$STR(S14)> 12345\r\n  #act\r\n  SendMsg 7 正确结果\r\n  #elseact\r\n  SendMsg 7 错误结果\r\n  ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ \r\n\r\n\r\n例如:\r\n#IF\r\nSMALL D1 1\r\n#SAY\r\n检测D1值是否小于1值 ;SMALL 代表:小于\r\n\r\n\r\n例如:\r\n#IF\r\nLARGE D1 1\r\n#SAY\r\n检测D1值是否大于1值 ;LARGE 代表:大于\r\n\r\n\r\n例如:\r\n#IF\r\nEQUAL D1 1\r\n#SAY\r\n检测D1值是否等于1值 ;EQUAL 代表:等于\r\n\r\n\r\n例如:\r\n#IF\r\n#ACT\r\nMOV D1 1\r\n#SAY\r\n将D1值的变量设置为1值 ;MOV 代表:给予\r\n\r\n\r\n例如:\r\n#IF\r\n#ACT\r\nMOVR D1 1000\r\n#SAY\r\n将D1值的变量设置为0-1000之间的随机值 ;MOVR 代表:给予指定范围内的随机值\r\n\r\n\r\n例如:\r\n#IF\r\n#ACT\r\nINC D1 1\r\n#SAY\r\n将D1值的变量增加了1点 ;INC 代表:加法\r\n\r\n\r\n例如:\r\n#IF\r\n#ACT\r\nDEC D1 1\r\n#SAY\r\n将D1值的变量减少了1点 ;DEC 代表:减法\r\n\r\n\r\n例如:\r\n#IF\r\n#ACT\r\nMUL D1 1\r\n#SAY\r\n将D1值的变量乘以1数字 ;MUL 代表:乘法\r\n\r\n\r\n例如:\r\n#IF\r\n#ACT\r\nDIV D1 1\r\n#SAY\r\n将D1值的变量除以1数字 ;DIV 代表:除法 \r\n\r\n例如:\r\n#IF\r\n#ACT\r\nPERCENT M1 $STR(M2) \r\n#SAY\r\nM1的边量= M1除M2*100%的值 ;PERCENT 代表:变量1除以变量乘100%的值 \r\n");

                    cd = new CompletionData("变量与变量之间的常用格式说明", "仅获取当前说明内容,跟您写的代码没有任何关系");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量与变量之间的常用格式说明", "SMALL M88 <$STR(G88)> ;检测私人变量M88,是否小于全局变量G88 \r\n\r\nLARGE M88 <$STR(G88)> ;检测私人变量M88,是否大于全局变量G88 \r\n\r\nEQUAL M88 <$STR(G88)> ;检测私人变量M88,是否等于全局变量G88 \r\n\r\nMOV G88 <$STR(M88)>;设置全局变量G88= M88\r\n\r\nINC G88 <$STR(M88)>;设置全局变量G88= G88+M88\r\n\r\nDEC G88 <$STR(M88)>;设置全局变量G88= G88-M88\r\n\r\nMUL M88 $STR(M89);设置私人变量M88= M88*M89\r\n\r\nDIV M88 $STR(M89);设置私人变量M88= M88/M89\r\n\r\nPERCENT M88 $STR(M89);设置私人变量M88= M88/M89*100%\r\n");

                    cd = new CompletionData("其他变量命令说明", "仅获取当前说明内容,跟您写的代码没有任何关系");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("其他变量命令说明", "1.关于SUM命令的详解：\r\n\r\n首先有MOV (变量X) 0 ;(变量X)清0 \r\n\r\nSUM (变量A) (变量B);X= A+B\r\n\r\nSUM (变量C);X= X+C\r\n\r\n例如：设置M11=M12+M13+M14\r\n\r\nMOV M11 0 \r\n\r\nSUM M12 M13 \r\n\r\nSUM M14 \r\n\r\n2.关于movr命令的详解：\r\n\r\nmovr (变量) (数值) ;随机把指定数值以下的数(正数)给变量 \r\n\r\nmovr M8 5 \r\n\r\n意思就是 随机设置M8 为 0 1 2 3 4 \r\n\r\n3.DEC特殊用法\r\n\r\n支持A,S变量的DEC操作,格式：DEC A0 X Y \r\n\r\n其中X,Y表示位置,操作的结果：删除A0字符串中从X开始到Y结束之间的字符-\r\n\r\n以下操作去掉\"ABCDEFGHIJK\"\r\n\r\nMOV S0 屠龙ABCDEFGHIJK刀 \r\n\r\nDEC S0 5 15 \r\n");

                    cd = new CompletionData("自定义变量说明", "仅获取当前说明内容,跟您写的代码没有任何关系");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("自定义变量说明", "注意： 自定义变量不要以P、G、M、I、D、N、S、A开头 \r\n\r\n1.申明变量：\r\n\r\nVAR Integer HUMAN 剩余领取\r\nLOADVAR HUMAN 剩余领取 ..\\QuestDiary\\数据文件\\剩余领取Save.txt \r\n\r\n2.自定义变量常用格式:\r\n\r\nCHECKVAR HUMAN 自定义变量 (?、>、= 、<) 10000 \r\n\r\nCHECKVAR HUMAN 自定义变量 (?、>、= 、<) <$STR(D3)> \r\n\r\n-------------------------------------------\r\n\r\nCALCVAR HUMAN 自定义变量 (=、+、-) 10000 \r\nSAVEVAR HUMAN 自定义变量 ..\\QuestDiary\\数据文件\\自定义变量Save.txt \r\n\r\nCALCVAR HUMAN 自定义变量 (=、+、-) <$STR(D3)>\r\nSAVEVAR HUMAN 自定义变量 ..\\QuestDiary\\数据文件\\自定义变量Save.txt \r\n\r\n-------------------------------------------\r\n\r\nMOV M88 <$HUMAN(自定义变量)>;设置变量M88= [自定义变量]\r\n");

                    cd = new CompletionData("标识说明", "仅获取当前说明内容,跟您写的代码没有任何关系");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("标识说明", "[001]-[499](各引擎不同标识的上限值不能。正常的是499个,这不是变量,这是标识\r\n\r\n标识的初始值是关闭的,它不会因为人物下线或服务器重启而重置\r\n\r\n相关命令格式:\r\n\r\nCheck [001] 0 的意思是检测你的标识[001]是否关闭 \r\n\r\nSET [001] 1 的意思是设置你的标识[001]为开的状态 \r\n\r\n1.关于reset命令的详解：\r\n\r\nreset [XXX] 7 意思是将从XXX开始的7个变量回复到原始值0 \r\n\r\n比如：reset [100] 7 就是把100 101 102 103 104 105 106 这7个变量赋值为0。 \r\n\r\n它等同与：set [100] 0 \r\n\r\nset [101] 0 \r\n\r\nset [102] 0 \r\n\r\nset [103] 0 \r\n\r\nset [104] 0 \r\n\r\nset [105] 0 \r\n\r\nset [106] 0\r\n");

                    cd = new CompletionData("变量唯一ID", "确保ID唯一的前提是：不同的服务器,setup参数中的serverid唯一\n<$UID>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量唯一ID", "<$UID>");

                    cd = new CompletionData("变量邮箱", "<$EMAIL>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量邮箱", "<$EMAIL>");

                    cd = new CompletionData("变量生日", "<$BIRTHDAY>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量生日", "<$BIRTHDAY>");

                    cd = new CompletionData("变量注册电话", "<$PHONE2>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量注册电话", "<$PHONE2>");

                    cd = new CompletionData("变量手机号码", "<$MOBILEPHONE>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量手机号码", "<$MOBILEPHONE>");

                    cd = new CompletionData("变量问题1", "<$QUIZ>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量问题1", "<$QUIZ>");

                    cd = new CompletionData("变量答案1", "<$ANSWER>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量答案1", "<$ANSWER>");

                    cd = new CompletionData("变量问题2", "<$QUIZ2>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量问题2", "<$QUIZ2>");

                    cd = new CompletionData("变量答案2", "<$ANSWER2>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量答案2", "<$ANSWER2>");

                    cd = new CompletionData("变量人物幸运值", "<$LUCKPOINT>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量人物幸运值", "<$LUCKPOINT>");

                    cd = new CompletionData("变量英雄幸运值", "<$H.LUCKPOINT>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量英雄幸运值", "<$H.LUCKPOINT>");

                    cd = new CompletionData("变量当前将出产物品的ID", "<$CURITEMINDEX>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量当前将出产物品的ID", "<$CURITEMINDEX>");

                    cd = new CompletionData("变量背包的物品", "主要用于展示背包的物品,NN为物品的ID,客户端鼠标指向物品图标有属性展示\n<$ITEM(NN)>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量背包的物品", "<$ITEM(NN)>");

                    cd = new CompletionData("变量当前引擎在线人数", "<$ONLINEHUMNUM>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量当前引擎在线人数", "<$ONLINEHUMNUM>");

                    cd = new CompletionData("变量当前引擎离线人数", "<$OFFLINEHUMNUM>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量当前引擎离线人数", "<$OFFLINEHUMNUM>");

                    cd = new CompletionData("变量总在线人数(多引擎)", "<$TOTALHUMNUM>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量总在线人数(多引擎)", "<$TOTALHUMNUM>");

                    cd = new CompletionData("变量多少秒后可以复活", "<$REVIVALDURATION> (秒)\n-1表示不能复活,0可以立即复活,其他正数表示n秒后被杀可以复活,在对方没有反复活技能的情况下");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量多少秒后可以复活", "<$REVIVALDURATION>");

                    cd = new CompletionData("变量战斗状态持续时间", "<$BATTLEMODEDURATION (毫秒)>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量战斗状态持续时间", "<$BATTLEMODEDURATION (毫秒)>");

                    cd = new CompletionData("变量掩码", "<$ATTACKFILTERMASK>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量掩码", "<$ATTACKFILTERMASK>");

                    cd = new CompletionData("变量分离", "添加将<$DATETIME>分离的变量：<$DATE>、<$TIME>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量分离", "<$DATETIME>");

                    cd = new CompletionData("变量", "<$H.ABILITIES 0~100>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量", "<$H.ABILITIES 20>");

                    cd = new CompletionData("变量年", "<$YEAR>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量年", "<$YEAR>");

                    cd = new CompletionData("变量月", "<$MONTH>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量月", "<$MONTH>");

                    cd = new CompletionData("变量日", "<$DAY>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量日", "<$DAY>");

                    cd = new CompletionData("变量时", "<$HOUR>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量时", "<$HOUR>");

                    cd = new CompletionData("变量分", "<$MINUTE>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量分", "<$MINUTE>");

                    cd = new CompletionData("变量秒", "<$SECOND>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量秒", "<$SECOND>");

                    cd = new CompletionData("变量毫秒", "<$MILLISECONDS>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量毫秒", "<$MILLISECONDS>");

                    cd = new CompletionData("变量服务器名称", "<$SERVERNAME>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量服务器名称", "<$SERVERNAME>");

                    cd = new CompletionData("变量服务器IP", "<$SERVERIP>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量服务器IP", "<$SERVERIP>");

                    cd = new CompletionData("变量网站", "<$WEBSITE>\n在String.ini设置");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量网站", "<$WEBSITE>");

                    cd = new CompletionData("变量论坛", "<$BBSSITE>\n在String.ini设置");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量论坛", "<$BBSSITE>");

                    cd = new CompletionData("变量客户端下载地址", "<$CLIENTDOWNLOAD>\n在String.ini设置");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量客户端下载地址", "<$CLIENTDOWNLOAD>");

                    cd = new CompletionData("变量QQ", "<$QQ>\n在String.ini设置");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量QQ", "<$QQ>");

                    cd = new CompletionData("变量电话", "<$PHONE>\n在String.ini设置");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量电话", "<$PHONE>");

                    cd = new CompletionData("变量银行信息0", "<$BANKACCOUNT0>\n在String.ini设置");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量银行信息0", "<$BANKACCOUNT0>");

                    cd = new CompletionData("变量银行信息1", "<$BANKACCOUNT1>\n在String.ini设置");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量银行信息1", "<$BANKACCOUNT1>");

                    cd = new CompletionData("变量银行信息2", "<$BANKACCOUNT2>\n在String.ini设置");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量银行信息2", "<$BANKACCOUNT2>");

                    cd = new CompletionData("变量银行信息3", "<$BANKACCOUNT3>\n在String.ini设置");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量银行信息3", "<$BANKACCOUNT3>");

                    cd = new CompletionData("变量银行信息4", "<$BANKACCOUNT4>\n在String.ini设置");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量银行信息4", "<$BANKACCOUNT4>");

                    cd = new CompletionData("变量银行信息5", "<$BANKACCOUNT5>\n在String.ini设置");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量银行信息5", "<$BANKACCOUNT5>");

                    cd = new CompletionData("变量银行信息6", "<$BANKACCOUNT6>\n在String.ini设置");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量银行信息6", "<$BANKACCOUNT6>");

                    cd = new CompletionData("变量银行信息7", "<$BANKACCOUNT7>\n在String.ini设置");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量银行信息7", "<$BANKACCOUNT7>");

                    cd = new CompletionData("变量银行信息8", "<$BANKACCOUNT8>\n在String.ini设置");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量银行信息8", "<$BANKACCOUNT8>");

                    cd = new CompletionData("变量银行信息9", "<$BANKACCOUNT9>\n在String.ini设置");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量银行信息9", "<$BANKACCOUNT9>");

                    cd = new CompletionData("变量游戏币名称", "<$GAMEGOLDNAME>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量游戏币名称", "<$GAMEGOLDNAME>");

                    cd = new CompletionData("变量游戏点名称", "<$GAMEPOINTNAME>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量游戏点名称", "<$GAMEPOINTNAME>");

                    cd = new CompletionData("变量在线人数", "<$USERCOUNT>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量在线人数", "<$USERCOUNT>");

                    cd = new CompletionData("变量服务器运行天数", "<$MACRUNTIME>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量服务器运行天数", "<$MACRUNTIME>");

                    cd = new CompletionData("变量服务器运行时间", "<$SERVERRUNTIME>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量服务器运行时间", "<$SERVERRUNTIME>");

                    cd = new CompletionData("变量服务器时间", "<$DATETIME>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量服务器时间", "<$DATETIME>");

                    cd = new CompletionData("变量最高级别信息", "<$HIGHLEVELINFO>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量最高级别信息", "<$HIGHLEVELINFO>");

                    cd = new CompletionData("变量最高PK值", "<$HIGHPKINFO>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量最高PK值", "<$HIGHPKINFO>");

                    cd = new CompletionData("变量最高攻击信息", "<$HIGHDCINFO>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量最高攻击信息", "<$HIGHDCINFO>");

                    cd = new CompletionData("变量最高魔法信息", "<$HIGHMCINFO>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量最高魔法信息", "<$HIGHMCINFO>");

                    cd = new CompletionData("变量最高道术信息", "<$HIGHSCINFO>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量最高道术信息", "<$HIGHSCINFO>");

                    cd = new CompletionData("变量在线最长时间玩家的信息", "<$HIGHONLINEINFO>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量在线最长时间玩家的信息", "<$HIGHONLINEINFO>");

                    cd = new CompletionData("变量人物当前地图名称信息", "<$CURRENTMAPDESC>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量人物当前地图名称信息", "<$CURRENTMAPDESC>");

                    cd = new CompletionData("变量人物当前地图名称编号", "<$CURRENTMAP>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量人物当前地图名称编号", "<$CURRENTMAP>");

                    cd = new CompletionData("变量人物当前坐标X", "<$CURRENTX>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量人物当前坐标X", "<$CURRENTX>");

                    cd = new CompletionData("变量人物当前坐标Y", "<$CURRENTY>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量人物当前坐标Y", "<$CURRENTY>");

                    cd = new CompletionData("变量人物性别", "<$GENDER>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量人物性别", "<$GENDER>");

                    cd = new CompletionData("变量英雄性别", "<$H.GENDER>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量英雄性别", "<$H.GENDER>");

                    cd = new CompletionData("变量人物职业", "<$JOB>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量人物职业", "<$JOB>");

                    cd = new CompletionData("变量英雄职业", "<$H.JOB>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量英雄职业", "<$H.JOB>");

                    cd = new CompletionData("变量人物名称", "<$USERNAME>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量人物名称", "<$USERNAME>");

                    cd = new CompletionData("变量点击后可得到的物品名", "<$DLGITEMNAME>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量点击后可得到的物品名", "<$DLGITEMNAME>");

                    cd = new CompletionData("变量随机值变量", "<$RANDOMNO>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量随机值变量", "<$RANDOMNO>");

                    cd = new CompletionData("变量元宝交易对象", "<$DEALGOLDPLAY>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量元宝交易对象", "<$DEALGOLDPLAY>");

                    cd = new CompletionData("变量杀人怪物变量", "<$MONKILLER>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量杀人怪物变量", "<$MONKILLER>");

                    cd = new CompletionData("变量杀人者名称", "<$KILLER>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量杀人者名称", "<$KILLER>");

                    cd = new CompletionData("变量被杀者名称", "<$DECEDENT>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量被杀者名称", "<$DECEDENT>");

                    cd = new CompletionData("变量转生级别", "<$RELEVEL>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量转生级别", "<$RELEVEL>");

                    cd = new CompletionData("变量英雄转生级别", "<$H.RELEVEL>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量英雄转生级别", "<$H.RELEVEL>");

                    cd = new CompletionData("变量行会名称", "<$GUILDNAME>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量行会名称", "<$GUILDNAME>");

                    cd = new CompletionData("变量行会职位名称", "<$RANKNAME>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量行会职位名称", "<$RANKNAME>");

                    cd = new CompletionData("变量级别", "<$LEVEL>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量级别", "<$LEVEL>");

                    cd = new CompletionData("变量当前生命值", "<$HP>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量当前生命值", "<$HP>");

                    cd = new CompletionData("变量最高生命值", "<$MAXHP>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量最高生命值", "<$MAXHP>");

                    cd = new CompletionData("变量魔法值", "<$MP>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量魔法值", "<$MP>");

                    cd = new CompletionData("变量最高魔法值", "<$MAXMP>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量最高魔法值", "<$MAXMP>");

                    cd = new CompletionData("变量防御", "<$AC>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量防御", "<$AC>");

                    cd = new CompletionData("变量最高防御", "<$MAXAC>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量最高防御", "<$MAXAC>");

                    cd = new CompletionData("变量魔御", "<$MAC>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量魔御", "<$MAC>");

                    cd = new CompletionData("变量最高魔御", "<$MAXMAC>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量最高魔御", "<$MAXMAC>");

                    cd = new CompletionData("变量攻击", "<$DC>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量攻击", "<$DC>");

                    cd = new CompletionData("变量最高攻击", "<$MAXDC>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量最高攻击", "<$MAXDC>");

                    cd = new CompletionData("变量魔法", "<$MC>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量魔法", "<$MC>");

                    cd = new CompletionData("变量最高魔法", "<$MAXMC>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量最高魔法", "<$MAXMC>");

                    cd = new CompletionData("变量道术", "<$SC>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量道术", "<$SC>");

                    cd = new CompletionData("变量最高道术", "<$MAXSC>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量最高道术", "<$MAXSC>");

                    cd = new CompletionData("变量准确", "<$HIT>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量准确", "<$HIT>");

                    cd = new CompletionData("变量躲避率", "<$SPD>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量躲避率", "<$SPD>");

                    cd = new CompletionData("变量当前经验", "<$EXP>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量当前经验", "<$EXP>");

                    cd = new CompletionData("变量升级经验值", "<$MAXEXP>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量升级经验值", "<$MAXEXP>");

                    cd = new CompletionData("变量PK点数", "<$PKPOINT>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量PK点数", "<$PKPOINT>");

                    cd = new CompletionData("变量声望点数", "<$CREDITPOINT>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量声望点数", "<$CREDITPOINT>");

                    cd = new CompletionData("变量荣誉值", "<$HEROCREDITPOINT>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量荣誉值", "<$HEROCREDITPOINT>");

                    cd = new CompletionData("变量腕力", "<$HW>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量腕力", "<$HW>");

                    cd = new CompletionData("变量最高腕力", "<$MAXHW>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量最高腕力", "<$MAXHW>");

                    cd = new CompletionData("变量背包重量", "<$BW>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量背包重量", "<$BW>");

                    cd = new CompletionData("变量最高背包重量", "<$MAXBW>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量最高背包重量", "<$MAXBW>");

                    cd = new CompletionData("变量负重力", "<$WW>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量负重力", "<$WW>");

                    cd = new CompletionData("变量最高负重", "<$MAXWW>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量最高负重", "<$MAXWW>");

                    cd = new CompletionData("变量金币", "<$GOLDCOUNT>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量金币", "<$GOLDCOUNT>");

                    cd = new CompletionData("变量元宝", "<$GAMEGOLD>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量元宝", "<$GAMEGOLD>");

                    cd = new CompletionData("变量灵气值", "<$NIMBUS>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量灵气值", "<$NIMBUS>");

                    cd = new CompletionData("变量英雄灵气值", "<$H.NIMBUS>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量英雄灵气值", "<$H.NIMBUS>");

                    cd = new CompletionData("变量游戏点", "<$GAMEPOINT>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量游戏点", "<$GAMEPOINT>");

                    cd = new CompletionData("变量金刚石数", "<$GAMEDIAMOND>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量金刚石数", "<$GAMEDIAMOND>");

                    cd = new CompletionData("变量灵符", "<$GAMEGIRD>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量灵符", "<$GAMEGIRD>");

                    cd = new CompletionData("变量饥饿程度", "<$HUNGER>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量饥饿程度", "<$HUNGER>");

                    cd = new CompletionData("变量登录时间", "<$LOGINTIME>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量登录时间", "<$LOGINTIME>");

                    cd = new CompletionData("变量登录时长", "<$LOGINLONG>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量登录时长", "<$LOGINLONG>");

                    cd = new CompletionData("变量身上衣服名称,下同", "<$DRESS>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量身上衣服名称,下同", "<$DRESS>");

                    cd = new CompletionData("变量身上武器名称", "<$WEAPON>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量身上武器名称", "<$WEAPON>");

                    cd = new CompletionData("变量蜡烛", "<$RIGHTHAND>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量蜡烛", "<$RIGHTHAND>");

                    cd = new CompletionData("变量头盔", "<$HELMET>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量头盔", "<$HELMET>");

                    cd = new CompletionData("变量斗笠", "<$HELMETEX>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量斗笠", "<$HELMETEX>");

                    cd = new CompletionData("变量项链", "<$NECKLACE>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量项链", "<$NECKLACE>");

                    cd = new CompletionData("变量戒指右", "<$RING_R>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量戒指右", "<$RING_R>");

                    cd = new CompletionData("变量戒指左", "<$RING_L>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量戒指左", "<$RING_L>");

                    cd = new CompletionData("变量手镯右", "<$ARMRING_R>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量手镯右", "<$ARMRING_R>");

                    cd = new CompletionData("变量手镯左", "<$ARMRING_L>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量手镯左", "<$ARMRING_L>");

                    cd = new CompletionData("变量符", "<$BUJUK>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量符", "<$BUJUK>");

                    cd = new CompletionData("变量腰带", "<$BELT>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量腰带", "<$BELT>");

                    cd = new CompletionData("变量鞋子", "<$BOOTS>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量鞋子", "<$BOOTS>");

                    cd = new CompletionData("变量宝石", "<$CHARM>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量宝石", "<$CHARM>");

                    cd = new CompletionData("变量军鼓", "<$DRUM>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量军鼓", "<$DRUM>");

                    cd = new CompletionData("变量时装", "<$HORSE>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量时装", "<$HORSE>");

                    cd = new CompletionData("变量IP地址", "<$FASHION>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量IP地址", "<$FASHION>");

                    cd = new CompletionData("变量IP地区信息", "<$IPLOCAL>\n如：来自于[<$IPLOCAL>]的玩家[<$USERNAME>]先生进入了游戏..");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量IP地区信息", "<$IPLOCAL>");

                    cd = new CompletionData("变量人物名字全称", "<$HUMANSHOWNAME>\n可包含行会封号,结婚对象,师徒名……等等");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量人物名字全称", "<$HUMANSHOWNAME>");

                    cd = new CompletionData("变量行会人数", "<$GUILDHUMCOUNT>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量行会人数", "<$GUILDHUMCOUNT>");

                    cd = new CompletionData("变量行会建筑度", "<$GUILDBUILDPOINT>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量行会建筑度", "<$GUILDBUILDPOINT>");

                    cd = new CompletionData("变量行会人气度", "<$GUILDAURAEPOINT>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量行会人气度", "<$GUILDAURAEPOINT>");

                    cd = new CompletionData("变量行会安定度", "<$GUILDSTABILITYPOINT>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量行会安定度", "<$GUILDSTABILITYPOINT>");

                    cd = new CompletionData("变量行会繁荣度", "<$GUILDFLOURISHPOINT>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量行会繁荣度", "<$GUILDFLOURISHPOINT>");

                    cd = new CompletionData("变量攻城需要的物品", "<$REQUESTCASTLEWARITEM>\n攻城需要的物品(祖玛头像)");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量攻城需要的物品", "<$REQUESTCASTLEWARITEM>");

                    cd = new CompletionData("变量多少天后攻城", "<$REQUESTCASTLEWARDAY>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量多少天后攻城", "<$REQUESTCASTLEWARDAY>");

                    cd = new CompletionData("变量允许建立行会的物品", "<$REQUESTBUILDGUILDITEM>\n允许建立行会的物品(号角)");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量允许建立行会的物品", "<$REQUESTBUILDGUILDITEM>");

                    cd = new CompletionData("变量城保所属行会", "<$OWNERGUILD>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量城保所属行会", "<$OWNERGUILD>");

                    cd = new CompletionData("变量城堡所属行会的老大", "<$LORD>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量城堡所属行会的老大", "<$LORD>");

                    cd = new CompletionData("变量城堡名称", "<$CASTLENAME>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量城堡名称", "<$CASTLENAME>");

                    cd = new CompletionData("变量申请行会战需要金币数", "<$GUILDWARFEE>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量申请行会战需要金币数", "<$GUILDWARFEE>");

                    cd = new CompletionData("变量建立行会所需的金币数", "<$BUILDGUILDFEE>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量建立行会所需的金币数", "<$BUILDGUILDFEE>");

                    cd = new CompletionData("变量攻城的日期", "<$CASTLEWARDATE>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量攻城的日期", "<$CASTLEWARDATE>");

                    cd = new CompletionData("变量攻城的时间表", "<$LISTOFWAR>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量攻城的时间表", "<$LISTOFWAR>");

                    cd = new CompletionData("变量占领日期", "<$CASTLECHANGEDATE>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量占领日期", "<$CASTLECHANGEDATE>");

                    cd = new CompletionData("变量最后一次攻城战的日期", "<$CASTLEWARLASTDATE>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量最后一次攻城战的日期", "<$CASTLEWARLASTDATE>");

                    cd = new CompletionData("变量占领天数", "<$CASTLEGETDAYS>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量占领天数", "<$CASTLEGETDAYS>");

                    cd = new CompletionData("记路标识所配套的变量", "<$TAGMAPNAME0>~<$TAGMAPNAME06> //0~6目标地图名\r\n<$TAGX0>~<$TAGX6> //0~6目标坐标X\r\n<$TAGY0>~<$TAGY6> //0~6目标坐标Y \r\n\r\n例：\r\nMAPMOVE TAGMAPNAME3 <$TAGX3> <$TAGY3> \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("记路标识所配套的变量", "<$TAGMAPNAME0>");

                    cd = new CompletionData("人物以及全局变量", "<$STR()> 变量：\r\nP //整数 0~9 私人\r\nG //整数 0~99 私人\r\nD //整数 0~99 私人\r\nM //整数 0~99 私人\r\nI //整数 0~99 全局\r\nA //字符 0~99 全局\r\nS //字符 0~99 全局\r\nH //整数 0~99 全局");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("人物以及全局变量", "<$STR()>");

                    cd = new CompletionData("个人自定义变量显示方式", "<$HUMAN()> //个人自定义变量显示方式");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("个人自定义变量显示方式", "<$HUMAN()>");

                    cd = new CompletionData("行会自定义变量显示方式", "<$GUILD()> //行会自定义变量显示方式");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("行会自定义变量显示方式", "<$GUILD()>");

                    cd = new CompletionData("全局自定义变量显示方式", "<$GLOBAL()> //全局自定义变量显示方式");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("全局自定义变量显示方式", "<$GLOBAL()>");

                    cd = new CompletionData("定义变量", "VAR //定义变量");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("定义变量", "VAR");

                    cd = new CompletionData("读取变量", "LOADVAR //读取");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("读取变量", "LOADVAR");

                    cd = new CompletionData("存储变量", "SAVEVAR //存储");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("存储变量", "SAVEVAR");

                    cd = new CompletionData("变量进行运算", "CALCVAR //对变量进行运算(+、-、*、/)");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量进行运算", "CALCVAR");

                    cd = new CompletionData("变量城堡金币数", "<$CASTLEGOLD> //城堡金币数");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量城堡金币数", "<$CASTLEGOLD>");

                    cd = new CompletionData("变量每天的收入", "<$TODAYINCOME> //每天的收入");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量每天的收入", "<$TODAYINCOME>");

                    cd = new CompletionData("变量城门状态", "<$CASTLEDOORSTATE> //城门状态");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量城门状态", "<$CASTLEDOORSTATE>");

                    cd = new CompletionData("变量修理城门的费用", "<$REPAIRDOORGOLD> //修理城门的费用");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量修理城门的费用", "<$REPAIRDOORGOLD>");

                    cd = new CompletionData("变量修理皇宫城墙的费用", "<$REPAIRWALLGOLD> //修理皇宫城墙的费用");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量修理皇宫城墙的费用", "<$REPAIRWALLGOLD>");

                    cd = new CompletionData("变量雇佣守卫费用", "<$GUARDFEE> //雇佣守卫费用");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量雇佣守卫费用", "<$GUARDFEE>");

                    cd = new CompletionData("变量雇佣弓箭手费用", "<$ARCHERFEE> //雇佣弓箭手费用");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量雇佣弓箭手费用", "<$ARCHERFEE>");

                    cd = new CompletionData("变量守卫状态", "<$GUARDRULE> //守卫状态(未起用)");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量守卫状态", "<$GUARDRULE>");

                    cd = new CompletionData("变量攻城列表", "<$REQUESTCASTLELIST> //攻城列表");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量攻城列表", "<$REQUESTCASTLELIST>");

                    cd = new CompletionData("变量价格倍数", "<$PRICERATE> //价格倍数");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量价格倍数", "<$PRICERATE>");

                    cd = new CompletionData("变量升级武器的价格", "<$UPGRADEWEAPONFEE> //升级武器的价格");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量升级武器的价格", "<$UPGRADEWEAPONFEE>");

                    cd = new CompletionData("变量手上拿的武器的名称", "<$USERWEAPON> //手上拿的武器的名称");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量手上拿的武器的名称", "<$USERWEAPON>");

                    cd = new CompletionData("变量元宝寄售交易记录", "<$QUERYYBDEALLOG> //元宝寄售交易记录(寄售人不在线,交易成功后,使用此变量回收已交易的元宝)");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量元宝寄售交易记录", "<$QUERYYBDEALLOG>");

                    cd = new CompletionData("变量老寄售税收", "<$SELLOFFRATE> //老寄售税收(已不使用)");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量老寄售税收", "<$SELLOFFRATE>");

                    cd = new CompletionData("变量老寄售物品列表", "<$SELLOFFITEM> //老寄售物品列表(已不使用)");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量老寄售物品列表", "<$SELLOFFITEM>");

                    cd = new CompletionData("变量老寄售所得金额", "<$SELLOUTGOLD> //老寄售所得金额(已不使用)");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量老寄售所得金额", "<$SELLOUTGOLD>");

                    cd = new CompletionData("变量HP附加点数", "<$ABILITYADDPOINT0> //HP附加点数");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量HP附加点数", "<$ABILITYADDPOINT0>");

                    cd = new CompletionData("变量MP附加点数", "<$ABILITYADDPOINT1> //MP附加点数");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量MP附加点数", "<$ABILITYADDPOINT1>");

                    cd = new CompletionData("变量防御附加点数", "<$ABILITYADDPOINT2> //防御附加点数");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量防御附加点数", "<$ABILITYADDPOINT2>");

                    cd = new CompletionData("变量魔御附加点数", "<$ABILITYADDPOINT3> //魔御附加点数");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量魔御附加点数", "<$ABILITYADDPOINT3>");

                    cd = new CompletionData("变量攻击附加点数", "<$ABILITYADDPOINT4> //攻击附加点数");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量攻击附加点数", "<$ABILITYADDPOINT4>");

                    cd = new CompletionData("变量魔法附加点数", "<$ABILITYADDPOINT5> //魔法附加点数");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量魔法附加点数", "<$ABILITYADDPOINT5>");

                    cd = new CompletionData("变量道术附加点数", "<$ABILITYADDPOINT6> //道术附加点数");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量道术附加点数", "<$ABILITYADDPOINT6>");

                    cd = new CompletionData("变量HP附加点数时间", "<$ABILITYADDTIME0> //HP附加点数时间");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量HP附加点数时间", "<$ABILITYADDTIME0>");

                    cd = new CompletionData("变量MP附加点数时间", "<$ABILITYADDTIME1> //MP附加点数时间");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量MP附加点数时间", "<$ABILITYADDTIME1>");

                    cd = new CompletionData("变量防御附加点数时间", "<$ABILITYADDTIME2> //防御附加点数时间");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量防御附加点数时间", "<$ABILITYADDTIME2>");

                    cd = new CompletionData("变量魔御附加点数时间", "<$ABILITYADDTIME3> //魔御附加点数时间");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量魔御附加点数时间", "<$ABILITYADDTIME3>");

                    cd = new CompletionData("变量攻击附加点数时间", "<$ABILITYADDTIME4> //攻击附加点数时间");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量攻击附加点数时间", "<$ABILITYADDTIME4>");

                    cd = new CompletionData("变量魔法附加点数时间", "<$ABILITYADDTIME5> //魔法附加点数时间");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量魔法附加点数时间", "<$ABILITYADDTIME5>");

                    cd = new CompletionData("变量道术附加点数时间", "<$ABILITYADDTIME6> //道术附加点数时间");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量道术附加点数时间", "<$ABILITYADDTIME6>");

                    cd = new CompletionData("变量天地结晶开启元宝", "<$GCEPAYMENT> //天地结晶开启元宝");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量天地结晶开启元宝", "<$GCEPAYMENT>");

                    cd = new CompletionData("变量天地结晶当前经验", "<$COLLECTEXP> //天地结晶当前经验");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量天地结晶当前经验", "<$COLLECTEXP>");

                    cd = new CompletionData("变量天地结晶当前内功经验", "<$COLLECTIPEXP> //天地结晶当前内功经验");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量天地结晶当前内功经验", "<$COLLECTIPEXP>");

                    cd = new CompletionData("变量天地结晶当前能提取的经验", "<$GAINCOLLECTEXP> //天地结晶当前能提取的经验");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量天地结晶当前能提取的经验", "<$GAINCOLLECTEXP>");

                    cd = new CompletionData("变量天地结晶当前能提取的内功经验", "<$GAINCOLLECTIPEXP> //天地结晶当前能提取的内功经验");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量天地结晶当前能提取的内功经验", "<$GAINCOLLECTIPEXP>");

                    cd = new CompletionData("变量剩余点数", "<$BONUSPOINT> //剩余点数");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量剩余点数", "<$BONUSPOINT>");

                    cd = new CompletionData("变量已+防御点数", "<$BONUSABIL_AC> //已+防御点数");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量已+防御点数", "<$BONUSABIL_AC>");

                    cd = new CompletionData("变量已+魔御点数", "<$BONUSABIL_MAC> //已+魔御点数");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量已+魔御点数", "<$BONUSABIL_MAC>");

                    cd = new CompletionData("变量已+攻击点数", "<$BONUSABIL_DC> //已+攻击点数");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量已+攻击点数", "<$BONUSABIL_DC>");

                    cd = new CompletionData("变量已+魔法点数", "<$BONUSABIL_MC> //已+魔法点数");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量已+魔法点数", "<$BONUSABIL_MC>");

                    cd = new CompletionData("变量已+道术点数", "<$BONUSABIL_SC> //已+道术点数");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量已+道术点数", "<$BONUSABIL_SC>");

                    cd = new CompletionData("变量已+HP点数", "<$BONUSABIL_HP> //已+HP点数");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量已+HP点数", "<$BONUSABIL_HP>");

                    cd = new CompletionData("变量已+MP点数", "<$BONUSABIL_MP> //已+MC点数");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量已+MP点数", "<$BONUSABIL_MP>");

                    cd = new CompletionData("变量已+准确点数", "<$BONUSABIL_HIT> //已+准确点数");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量已+准确点数", "<$BONUSABIL_HIT>");

                    cd = new CompletionData("变量已+躲避点数", "<$BONUSABIL_SPD> //已+躲避点数");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量已+躲避点数", "<$BONUSABIL_SPD>");

                    cd = new CompletionData("变量已+X2点数", "<$BONUSABIL_X2> //已+X2点数(未起用)");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量已+X2点数", "<$BONUSABIL_X2>");

                    cd = new CompletionData("变量+防御自由点数", "<$BONUSTICK_AC>\n//增加1点AC所需要的自由点数,如X/17,表示需要17自由点才+1点AC值,AC上下限自动分配");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量+防御自由点数", "<$BONUSTICK_AC>");

                    cd = new CompletionData("变量+魔御自由点数", "<$BONUSTICK_MAC>\n//增加1点MAC所需要的自由点数,如X/17,表示需要17自由点才+1点MAC值,MAC上下限自动分配");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量+魔御自由点数", "<$BONUSTICK_MAC>");

                    cd = new CompletionData("变量+攻击自由点数", "<$BONUSTICK_DC>\n//增加1点DC所需要的自由点数,如X/17,表示需要17自由点才+1点DC值,DC上下限自动分配");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量+攻击自由点数", "<$BONUSTICK_DC>");

                    cd = new CompletionData("变量+魔法自由点数", "<$BONUSTICK_MC>\n//增加1点MC所需要的自由点数,如X/17,表示需要17自由点才+1点MC值,MC上下限自动分配");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量+魔法自由点数", "<$BONUSTICK_MC>");

                    cd = new CompletionData("变量+道术自由点数", "<$BONUSTICK_SC>\n//增加1点SC所需要的自由点数,如X/17,表示需要17自由点才+1点SC值,SC上下限自动分配");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量+道术自由点数", "<$BONUSTICK_SC>");

                    cd = new CompletionData("变量+HP自由点数", "<$BONUSTICK_HP>\n//增加1点HP所需要的自由点数,如X/17,表示需要17自由点才+1点HP值,HP上下限自动分配");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量+HP自由点数", "<$BONUSTICK_HP>");

                    cd = new CompletionData("变量+MP自由点数", "<$BONUSTICK_MP>\n//增加1点MP所需要的自由点数,如X/17,表示需要17自由点才+1点MP值,MP上下限自动分配");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量+MP自由点数", "<$BONUSTICK_MP>");

                    cd = new CompletionData("变量+准确自由点数", "<$BONUSTICK_HIT>\n//增加1点HIT所需要的自由点数,如X/17,表示需要17自由点才+1点HIT值,HIT上下限自动分配");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量+准确自由点数", "<$BONUSTICK_HIT>");

                    cd = new CompletionData("变量+躲避自由点数", "<$BONUSTICK_SPD>\n//增加1点SPD所需要的自由点数,如X/17,表示需要17自由点才+1点SPD值,SPD上下限自动分配");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量+躲避自由点数", "<$BONUSTICK_SPD>");

                    cd = new CompletionData("变量+X2自由点数", "<$BONUSTICK_X2>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("变量+X2自由点数", "<$BONUSTICK_X2>");

                    cd = new CompletionData("加点脚本", "脚本示例\n仅获取当前说明内容,跟您写的代码没有任何关系");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("加点脚本", "DC: <$DC>-<$MAXDC> <$BONUSABIL_DC>/<$BONUSTICK_DC> <+/@AddDC> <-/@decDC> HP: <$HP>-<$MAXHP> <$BONUSABIL_HP>/<$BONUSTICK_HP> <+/@AddHP> <-/@decHP>\\\r\nMC: <$MC>-<$MAXMC> <$BONUSABIL_MC>/<$BONUSTICK_MC> <+/@AddMC> <-/@decMC> MP: <$MP>-<$MAXMP> <$BONUSABIL_MP>/<$BONUSTICK_MP> <+/@AddMP> <-/@decMP>\\\r\nSC: <$MC>-<$MAXSC> <$BONUSABIL_SC>/<$BONUSTICK_SC> <+/@AddSC> <-/@decSC> AC: <$MC>-<$MAXAC> <$BONUSABIL_AC>/<$BONUSTICK_AC> <+/@AddAC> <-/@decAC>\\\r\nMAC: <$MAC>-<$MAXMAC> <$BONUSABIL_MAC>/<$BONUSTICK_MAC> <+/@AddMAC> <-/@decMAC>\\\r\nHIT: <$HIT>-<$HIT> <$BONUSABIL_HIT>/<$BONUSTICK_HIT> <+/@AddHIT> <-/@decHIT>\\\r\nSPD: <$SPD>-<$SPD> <$BONUSABIL_SPD>/<$BONUSTICK_SPD> <+/@AddSPD> <-/@decSPD>\\\r\n剩余点数：<$BONUSPOINT> <重新分配/@RestBonus> <返回/@main>\\");
                    
                    cd = new CompletionData("检测是否正在摆摊的NPC", ";=========================================================\r\n购买摆摊物品触发：QFunction [@OnBuyItemFromStall] <$PARAM(1)>:物品名 <$PARAM(2)>:价格 <$PARAM(3)>:价格类型(金币/元宝) <$PARAM(4)>:卖家名\r\n;=========================================================\r\n举例：\r\n;=========================================================\r\n[@摆摊]\r\n#IF\r\n#SAY\r\n<查找摆摊玩家/@查找人名>\r\n\r\n[@查找人名]\r\n#IF\r\n!ISMARKETSTALLOPENED\r\n#ACT\r\nQUERYVALUE 1 0 30 ~人物名 请输入你要查找摆摊对象角色名字(如：小名)\r\nBREAK\r\n#ELSEACT\r\nMessagebox 玩家您正在摆摊，无法查询他人的摊位！\r\nbreak\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检测是否正在摆摊的NPC", "ISMARKETSTALLOPENED");

                    cd = new CompletionData("增加可以点击摆摊中的角色名称", "[~人物名]\r\n#IF\r\nCHECKONLINE <$STR(S1)>\r\n#ACT\r\nCLICKNPC <$STR(S1)>\r\n#ELSEACT\r\nMessagebox 玩家<$STR(S1)>暂时不在线！\r\nbreak");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("增加可以点击摆摊中的角色名称", "CLICKNPC");

                    cd = new CompletionData("新增加IF多条件检测", "功能： \r\n\r\n#IF(3) //扩展，如果众多条件中只要满足3条件，即可执行#ACT,#SAY...等 \r\n\r\n例如： \r\n\r\n#IF(1)\r\nISONMAP H010\r\nISONMAP 3\r\nISONMAP 2\r\n#SAY\r\n您在规定地图内 \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("新增加IF多条件检测", "#IF(3)");

                    cd = new CompletionData("检测人物名称", "功能： \r\n\r\nISNEARBY H/人物名  // 检测指定角色名玩家/怪物等是否在附近，H指自己的英雄 \r\n\r\n例如： \r\n\r\n#IF\r\nISNEARBY 黄泉教主\r\n#SAY\r\n黄泉教主就在附近！  //如果有玩家名称叫做：黄泉教主，那么将先按人物读取。 \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检测人物名称", "ISNEARBY H/人物名");

                    cd = new CompletionData("检查指定地图 指定范围内 玩家数量", "称号命令： \r\n\r\nCHECKRANGEPLAYERCOUNT 地图 X Y 范围 >/</= 数量 包含死亡（0不包含 1包含） //检测指定地图 指定范围内 玩家数量 \r\n\r\n\r\n[@CHECKRANGEPLAYERCOUNT]\r\n#IF \r\nCHECKRANGEPLAYERCOUNT 3 330 330 10 > 10\r\n#SAY\r\n盟重内玩家数量大于10位！\r\n#ELSESAY\r\n盟重内玩家数量小于10位！\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查指定地图 指定范围内 玩家数量", "CHECKRANGEPLAYERCOUNT");
                     

                    cd = new CompletionData("检查性别", "检查性别：男/女\r\n\r\nENDER MAN/WOMAN");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查性别", "GENDER MAN/WOMAN");

                    cd = new CompletionData("检查一个地图内的人数", "检查一个地图内的人数 CHECKHUM 3 数量");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查一个地图内的人数", "CHECKHUM");

                    cd = new CompletionData("检查攻击怪物", "检查攻击怪物\r\n\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查攻击怪物", "CHECKKILLPLAYMON");

                    cd = new CompletionData("检查人物是否有英雄", "检查人物是否有英雄\r\n\r\nHAVEHERO");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查人物是否有英雄", "HAVEHERO");

                    cd = new CompletionData("检查人物是否管理员", "检查人物是否管理员\r\n\r\nISADMIN");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查人物是否管理员", "ISADMIN");

                    cd = new CompletionData("反向检测", "CHECK检测的相反\r\n\r\n!CHECK");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("反向检测", "!CHECK");

                    cd = new CompletionData("检测文本列表", "功能：CHECKCODELIST //检测文本列表\r\n举例：CLEARCODELIST //清理列表信息\r\n;==========================================\r\n(@@INPUTINTEGER @@INPUTSTRING)\r\n[@@INPUTSTRING6]\r\n#IF\r\nCHECKCODELIST ..\\QUESTDIARY\\卡号\\激活码.TXT HARDDISK\r\n#ACT\r\nCLEARCODELIST ..\\QUESTDIARY\\卡号\\激活码.TXT HARDDISK\r\n#ELSESAY\r\n激活码不正确！\\\r\n<返回/@MAIN>");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检测文本列表", "CHECKCODELIST");

                    cd = new CompletionData("增加人物技能", "功能：ADDSKILL 技能名称\r\n;==========================================\r\n[@ADDSKILL]\r\n#IF\r\n#ACT\r\nADDSKILL 雷电术\r\n#SAY\r\n你已经练习雷电术了。\r\n;==========================================");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("增加人物技能", "ADDSKILL");

                    cd = new CompletionData("检查技能级别", "功能：CHECKMAGICLEVEL 技能名 < = > 等级\r\n================================\r\n[@CHECKMAGICLEVEL]\r\n#IF \r\nCHECKMAGICLEVEL 雷电术 > 2\r\n#SAY\r\n你的雷电术大于2级！ \r\n================================");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查技能级别", "CHECKMAGICLEVEL");

                    cd = new CompletionData("添加获取普通技能等级变量", "添加获取普通技能等级变量、括号内是技能ID，返回-1表示技能未学习或其他错误\r\n\r\n功能：获取技能等级：<$MAGICLEVEL(1~255)>\r\n;==========================================\r\n[@技能等级]\r\n#IF\r\nCOMPVAL  <$MAGICLEVEL(27)> ? 3\r\n#ACT\r\nMESSAGEBOX 当前技能【野蛮冲撞】已经达到：<$MAGICLEVEL(27)>级、无法进行升级！\r\nBREAK\r\n\r\n#IF\r\nCHECKMAGICNAME 野蛮冲撞\r\n#act\r\nSendMsg 7 技能野蛮冲撞：<$MAGICLEVEL(27)>级！\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("添加获取普通技能等级变量", "<$MAGICLEVEL(1~255)>");

                    cd = new CompletionData("检查镖车等级", "功能：RANDOM               //检查镖车等级\r\n功能：STARTESCORT 镖车名字 //开始任务\r\n;==========================================\r\n[@STARTESCORT]\r\n#IF\r\nRANDOM 10\r\n#ACT \r\nSTARTESCORT 无敌镖车 \r\nSENDMSG 0 玩家%S，接到了无敌镖车.谁也别想抢他的车哦！");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查镖车等级", "RANDOM");

                    cd = new CompletionData("调整人物经脉穴位", "功能：BREAKVENATIONPOINT V P //打通穴位\r\n格式：V //范围：0~3 表示四条经脉之一\r\n格式：P //范围：1~5 表示经脉的5个穴位\r\n;==========================================\r\n#IF\r\nCHECKIPLEVEL ？255\r\n#ACT\r\nBREAKVENATIONPOINT 0 1         //此设置直接打通了冲脉第一条穴位");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("调整人物经脉穴位", "BREAKVENATIONPOINT V P");

                    cd = new CompletionData("调整改变经络等级", "功能：CHECKVENATIONLEVEL V 控制符 < = > ? P //检测经脉等级\r\n格式：V //范围：0~3 表示四条经脉之一  P //范围：0~5 要检测的重数\r\n;==========================================\r\n#IF\r\nCHECKIPLEVEL ？255\r\n#ACT\r\nCHANGEVENATIONLEVEL 0 4      //此设置直接打通了冲脉所有穴位");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("调整改变经络等级", "CHANGEVENATIONLEVEL");

                    cd = new CompletionData("清理人物经络数据", "功能：清理经络数据 \r\n格式：CLEARVENATIONDATA\r\n;==========================================\r\n#IF\r\nCHECKIPLEVEL ？255\r\n#ACT\r\nCLEARVENATIONDATA           //清理经络数据 ");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("清理人物经络数据", "CLEARVENATIONDATA");

                    cd = new CompletionData("检查玩家等级", ";检查人物的等级是否等于指定级别\r\n[@CHECKLEVEL]\r\n#IF\r\nCHECKLEVEL 50\r\nH.CHECKLEVEL 50\r\n#SAY\r\n您的等级等于50级。\r\n您的英雄等级等于50级。\r\n#ELSESAY\r\n您的等级不等于50级。\r\n您的英雄等级不等于50级。");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("检查玩家等级", "CHECKLEVEL");

                    cd = new CompletionData("QMapEvent.txt-地图坐标触发", "功能：\r\n\r\n地图坐标触发.到达指定坐标按参数条件触发\r\n\r\n合击也支持技能触发\r\n改进普通下属超远飞回主人身边逻辑\r\n！QMapEvent的[@OnMapChanging]改为[@LeaveMap]，例：SendMsg 7 \"是否英雄：<$PARAM(0)>，原地图：<$PARAM(1)>，即将进入地图：<$PARAM(2)>\" // 支持下线和切图触发，<$PARAM(2)>空时表示下线，非空表示切图\r\n！QMapEvent的[@OnMapChanged ]改为[@EnterMap]，例：SendMsg 7 \"是否英雄：<$PARAM(0)>，原地图：<$PARAM(1)>，已经进入地图：<$PARAM(2)>\" // 支持上线和切图触发，<$PARAM(1)>空时表示上线，非空表示切图\r\n\r\n说明：\r\n\r\n配置文件MIR200\\ENVIR\\@MAPEVENT.TXT\r\n\r\n;触发标识\r\n;==========================================\r\n;标识:(-1 - 800) -1 代表不检查标识\r\n;值: (0 - 1)\r\n\r\n;触发条件\r\n;==========================================\r\n;格式: 代码:物品:组队\r\n;代码: 0:无效 1:扔物品 2:捡物品 3:挖矿 4:走路(不支持物品条件) 5:跑步(不支持物品条件)\r\n;物品: (物品名称 - *) * 代表不需要物品\r\n;组队: (0 - 1) 0为不需要组队，1为必须组队才触发(支持)\r\n\r\n;触发机率\r\n;==========================================\r\n; 数字越大，机率越低\r\n; 范围:(0 - 999999) 0 的机率为100%\r\n\r\n;事件类型\r\n;==========================================\r\n; 格式: 代码:内容\r\n; 代码:(现在只支持脚本事件)\r\n; 0:无效 1:调用脚本(调用QFUNCTION-0.TXT中的内容)\r\n\r\n;注意事项\r\n;==========================================\r\n;在相同地图座标，不支持相同触发标识及条件(触发条件中的物品名称除外)，如果有相同的设置，只有最后一个设置有效\r\n;地图号 座标X 座标Y 触发标识 触发条件 触发机率 事件类型\r\n3 333 333 -1:1 1:回城卷:0 2 1:@MAPEVENTDROPITEM\r\n3 333 333 -1:1 2:回城卷:0 2 1:@MAPEVENTPICKUPITEM\r\n3 338 331 -1:1 3:*:0 2 1:@MAPEVENTMINE\r\n3 330 330 -1:1 4:*:0 2 1:@MAPEVENTWALK\r\n3 331 335 -1:1 5:*:0 2 1:@MAPEVENTRUN \r\n;==========================================\r\n示例：\r\n;==========================================\r\n;MIR200\\ENVIR\\MAPEVENT.TXT中内容\r\n3 333 333 -1:1 1:回城卷:0 2 1:@MAPEVENTDROPITEM\r\n3 333 333 -1:1 2:回城卷:0 2 1:@MAPEVENTPICKUPITEM\r\n;==========================================\r\n;QFUNCTION-0.TXT中的内容\r\n\r\n[@MAPEVENTDROPITEM]\r\n#ACT\r\nSENDMSG 1 <$USERNAME>在(%M,%X,%Y)丢掉物品：回城卷\r\n[@MAPEVENTPICKUPITEM]\r\n#ACT\r\nSENDMSG 1 <$USERNAME>在(%M,%X,%Y)拣到物品：回城卷\r\n;==========================================\r\n使用瞬间移动功能，并且 当前地图<>目标地图\r\n;==========================================\r\n瞬移之前，触发 QMAPEVENT-0.TXT 的 [@ONMAPCHANGING] 改为[@LeaveMap]\r\n瞬移之后，触发 QMAPEVENT-0.TXT 的 [@ONMAPCHANGED]  改为[@EnterMap]\r\n例：\r\nNPC1： //进MAP001地图，限时30分钟\r\n;==========================================\r\n#IF\r\nTRUE\r\n#ACT\r\nTIMERECALL 30\r\nMAP 3\r\n\r\nQMAPEVENT： //在3瞬移到其他地图，清理TIMERECALL\r\n;========================================== \r\n\r\n[@LeaveMap]\r\n#IF\r\nISONMAP 3\r\n#ACT\r\nBREAKTIMERECALL\r\nSendMsg 7 \"是否英雄：<$PARAM(0)>，原地图：<$PARAM(1)>，即将进入地图：<$PARAM(2)>\r\n;========================================== \r\n\r\n[@EnterMap]\r\n#IF\r\nCHECKMISSION 3 = 14\r\n#ACT\r\nDELAYGOTO 1000 @cd\r\n#SAY\r\n<$USERNAME>，\\\r\n你将自动寻路到6 23这个坐标上！\\\r\n\r\n[@cd]\r\n#IF\r\nISONMAP 0105\r\n#ACT\r\nMOVETOCELL 7 19 比奇项链店老板\r\n\r\n\r\n支持调用QMAPEVENT脚本\r\n\r\n举例：\r\n;==========================================\r\n1/1  金条|22,祝福油|33,力量戒指|44,麻痹戒指|55,复活戒指|66  1  #CALL  @爆出条件检测  @爆出物品执行标签\r\n\r\n[@爆出条件检测]\r\n#IF\r\nCHECKGAMEGOLD < 10\r\n#ACT\r\n;执行APPROVE 0，即是检测不通过，不会爆出物品；注意默认的APPROVE结果为允许！\r\nAPPROVE 0\r\n[@爆出物品执行标签]\r\n;<$PARAM(1)>物品名，$PARAM(2)>LOOKS，<$PARAM(3)>怪物名，<$PARAM(4)>地图名，<$PARAM(5)>X坐标，<$PARAM(6)>Y坐标\r\n#ACT\r\nGAMEGOLD - 10\r\n;==========================================\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("QMapEvent.txt-地图坐标触发", "功能：\r\n\r\n地图坐标触发.到达指定坐标按参数条件触发\r\n\r\n合击也支持技能触发\r\n改进普通下属超远飞回主人身边逻辑\r\n！QMapEvent的[@OnMapChanging]改为[@LeaveMap]，例：SendMsg 7 \"是否英雄：<$PARAM(0)>，原地图：<$PARAM(1)>，即将进入地图：<$PARAM(2)>\" // 支持下线和切图触发，<$PARAM(2)>空时表示下线，非空表示切图\r\n！QMapEvent的[@OnMapChanged ]改为[@EnterMap]，例：SendMsg 7 \"是否英雄：<$PARAM(0)>，原地图：<$PARAM(1)>，已经进入地图：<$PARAM(2)>\" // 支持上线和切图触发，<$PARAM(1)>空时表示上线，非空表示切图\r\n\r\n说明：\r\n\r\n配置文件MIR200\\ENVIR\\@MAPEVENT.TXT\r\n\r\n;触发标识\r\n;==========================================\r\n;标识:(-1 - 800) -1 代表不检查标识\r\n;值: (0 - 1)\r\n\r\n;触发条件\r\n;==========================================\r\n;格式: 代码:物品:组队\r\n;代码: 0:无效 1:扔物品 2:捡物品 3:挖矿 4:走路(不支持物品条件) 5:跑步(不支持物品条件)\r\n;物品: (物品名称 - *) * 代表不需要物品\r\n;组队: (0 - 1) 0为不需要组队，1为必须组队才触发(支持)\r\n\r\n;触发机率\r\n;==========================================\r\n; 数字越大，机率越低\r\n; 范围:(0 - 999999) 0 的机率为100%\r\n\r\n;事件类型\r\n;==========================================\r\n; 格式: 代码:内容\r\n; 代码:(现在只支持脚本事件)\r\n; 0:无效 1:调用脚本(调用QFUNCTION-0.TXT中的内容)\r\n\r\n;注意事项\r\n;==========================================\r\n;在相同地图座标，不支持相同触发标识及条件(触发条件中的物品名称除外)，如果有相同的设置，只有最后一个设置有效\r\n;地图号 座标X 座标Y 触发标识 触发条件 触发机率 事件类型\r\n3 333 333 -1:1 1:回城卷:0 2 1:@MAPEVENTDROPITEM\r\n3 333 333 -1:1 2:回城卷:0 2 1:@MAPEVENTPICKUPITEM\r\n3 338 331 -1:1 3:*:0 2 1:@MAPEVENTMINE\r\n3 330 330 -1:1 4:*:0 2 1:@MAPEVENTWALK\r\n3 331 335 -1:1 5:*:0 2 1:@MAPEVENTRUN \r\n;==========================================\r\n示例：\r\n;==========================================\r\n;MIR200\\ENVIR\\MAPEVENT.TXT中内容\r\n3 333 333 -1:1 1:回城卷:0 2 1:@MAPEVENTDROPITEM\r\n3 333 333 -1:1 2:回城卷:0 2 1:@MAPEVENTPICKUPITEM\r\n;==========================================\r\n;QFUNCTION-0.TXT中的内容\r\n\r\n[@MAPEVENTDROPITEM]\r\n#ACT\r\nSENDMSG 1 <$USERNAME>在(%M,%X,%Y)丢掉物品：回城卷\r\n[@MAPEVENTPICKUPITEM]\r\n#ACT\r\nSENDMSG 1 <$USERNAME>在(%M,%X,%Y)拣到物品：回城卷\r\n;==========================================\r\n使用瞬间移动功能，并且 当前地图<>目标地图\r\n;==========================================\r\n瞬移之前，触发 QMAPEVENT-0.TXT 的 [@ONMAPCHANGING] 改为[@LeaveMap]\r\n瞬移之后，触发 QMAPEVENT-0.TXT 的 [@ONMAPCHANGED]  改为[@EnterMap]\r\n例：\r\nNPC1： //进MAP001地图，限时30分钟\r\n;==========================================\r\n#IF\r\nTRUE\r\n#ACT\r\nTIMERECALL 30\r\nMAP 3\r\n\r\nQMAPEVENT： //在3瞬移到其他地图，清理TIMERECALL\r\n;========================================== \r\n\r\n[@LeaveMap]\r\n#IF\r\nISONMAP 3\r\n#ACT\r\nBREAKTIMERECALL\r\nSendMsg 7 \"是否英雄：<$PARAM(0)>，原地图：<$PARAM(1)>，即将进入地图：<$PARAM(2)>\r\n;========================================== \r\n\r\n[@EnterMap]\r\n#IF\r\nCHECKMISSION 3 = 14\r\n#ACT\r\nDELAYGOTO 1000 @cd\r\n#SAY\r\n<$USERNAME>，\\\r\n你将自动寻路到6 23这个坐标上！\\\r\n\r\n[@cd]\r\n#IF\r\nISONMAP 0105\r\n#ACT\r\nMOVETOCELL 7 19 比奇项链店老板\r\n\r\n\r\n支持调用QMAPEVENT脚本\r\n\r\n举例：\r\n;==========================================\r\n1/1  金条|22,祝福油|33,力量戒指|44,麻痹戒指|55,复活戒指|66  1  #CALL  @爆出条件检测  @爆出物品执行标签\r\n\r\n[@爆出条件检测]\r\n#IF\r\nCHECKGAMEGOLD < 10\r\n#ACT\r\n;执行APPROVE 0，即是检测不通过，不会爆出物品；注意默认的APPROVE结果为允许！\r\nAPPROVE 0\r\n[@爆出物品执行标签]\r\n;<$PARAM(1)>物品名，$PARAM(2)>LOOKS，<$PARAM(3)>怪物名，<$PARAM(4)>地图名，<$PARAM(5)>X坐标，<$PARAM(6)>Y坐标\r\n#ACT\r\nGAMEGOLD - 10\r\n;==========================================\r\n");

                    cd = new CompletionData("MapQuest--添加拾取触发", "功能：\r\n\r\nMapQuest--添加拾取触发，文件MapQuest.txt，\r\n触发时支持的变量：同上\r\n掉落物品的角色，如果下线或消失，角色名将为空，一般设置物品消失的时间比怪物消失的时间长\r\n例：\r\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\r\n;地图 变量 判断 角色名   物品名  执行文件名\r\n3  [123] 0  蛤蟆/玩家名  乌木剑  QPickup1   // QPickup1指向MapQuest_def目录下的QPickup.txt脚本\r\n3  [123] 0  *    屠龙  QPickup2\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("MapQuest--添加拾取触发", "功能：\r\n\r\nMapQuest--添加拾取触发，文件MapQuest.txt，\r\n触发时支持的变量：同上\r\n掉落物品的角色，如果下线或消失，角色名将为空，一般设置物品消失的时间比怪物消失的时间长\r\n例：\r\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\r\n;地图 变量 判断 角色名   物品名  执行文件名\r\n3  [123] 0  蛤蟆/玩家名  乌木剑  QPickup1   // QPickup1指向MapQuest_def目录下的QPickup.txt脚本\r\n3  [123] 0  *    屠龙  QPickup2\r\n");
                    
                    cd = new CompletionData("QFUNCTION-称号触发", "功能：\r\n\r\n玩家改变使用称号或刚上线有使用到称号，触发：QFUNCTION-0 的\r\n\r\n人物：[@TITLECHANGED_XX]\r\n英雄：[@HEROTITLECHANGED_XX]\r\nXX代表物品DB中的SHAPE\r\n\r\n格式：\r\n\r\n在QFUNCTION-0.TXT里写入以下 里面的判断自己写。 \r\n\r\n[@TITLECHANGED_XX]\r\n#IF\r\nISCASTLEMASTER\r\n#ACT\r\nSENDMSG 0 伟大的沙巴克城主<$USERNAME>进入了游戏! \r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("QFUNCTION-称号触发", "功能：\r\n\r\n玩家改变使用称号或刚上线有使用到称号，触发：QFUNCTION-0 的\r\n\r\n人物：[@TITLECHANGED_XX]\r\n英雄：[@HEROTITLECHANGED_XX]\r\nXX代表物品DB中的SHAPE\r\n\r\n格式：\r\n\r\n在QFUNCTION-0.TXT里写入以下 里面的判断自己写。 \r\n\r\n[@TITLECHANGED_XX]\r\n#IF\r\nISCASTLEMASTER\r\n#ACT\r\nSENDMSG 0 伟大的沙巴克城主<$USERNAME>进入了游戏! \r\n");

                    cd = new CompletionData("QFunction--怪物掉落触发", "QF添加怪物掉落极品触发：\r\n\r\n[@MONDROPITEMDOWN]，极品指HINTITEMLIST.TXT中存在的物品，需要同时更新客户端\r\n触发时支持的变量：<$PARAM(0)>:手动丢弃(0/1) <$PARAM(1)>:物品名 <$PARAM(2)>:LOOKS <$PARAM(3)>:掉落者名称，<$PARAM(4)>:地图名，\r\n<$PARAM(5)>:X坐标 <$PARAM(6)>:Y坐标 <$PARAM(7)>:物品ID <$PARAM(8)>:掉落者RACE(人物1，英雄60)\r\n\r\n例1：\r\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\r\n[@MONDROPITEMDOWN]\r\n#ACT\r\n; 下面几句是为了显示为一条信息，可能不太准确\r\nMOV MDROPITEMID <$PARAM(7)>\r\nINC SMONDROPDOWNITEMS <$ITEM(MDROPITEMID)>\r\nDELAYGOTO 64 ~MONDROPITEMDOWN\r\n\r\n[~MONDROPITEMDOWN]\r\n#IF\r\nCOMPVAL <$STR(SMONDROPDOWNITEMS)> ! \"\"\r\n#ACT\r\nSENDSCROLLMSG <$PARAM(4)>的<$PARAM(3)>掉落：<$STR(SMONDROPDOWNITEMS)>(点击物品可暂停漂移) 151 16\r\nMOV SMONDROPDOWNITEMS \"\"\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("QFunction--怪物掉落触发", "QF添加怪物掉落极品触发：\r\n\r\n[@MONDROPITEMDOWN]，极品指HINTITEMLIST.TXT中存在的物品，需要同时更新客户端\r\n触发时支持的变量：<$PARAM(0)>:手动丢弃(0/1) <$PARAM(1)>:物品名 <$PARAM(2)>:LOOKS <$PARAM(3)>:掉落者名称，<$PARAM(4)>:地图名，\r\n<$PARAM(5)>:X坐标 <$PARAM(6)>:Y坐标 <$PARAM(7)>:物品ID <$PARAM(8)>:掉落者RACE(人物1，英雄60)\r\n\r\n例1：\r\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\r\n[@MONDROPITEMDOWN]\r\n#ACT\r\n; 下面几句是为了显示为一条信息，可能不太准确\r\nMOV MDROPITEMID <$PARAM(7)>\r\nINC SMONDROPDOWNITEMS <$ITEM(MDROPITEMID)>\r\nDELAYGOTO 64 ~MONDROPITEMDOWN\r\n\r\n[~MONDROPITEMDOWN]\r\n#IF\r\nCOMPVAL <$STR(SMONDROPDOWNITEMS)> ! \"\"\r\n#ACT\r\nSENDSCROLLMSG <$PARAM(4)>的<$PARAM(3)>掉落：<$STR(SMONDROPDOWNITEMS)>(点击物品可暂停漂移) 151 16\r\nMOV SMONDROPDOWNITEMS \"\"\r\n");

                    cd = new CompletionData("QFunction--杀怪触发脚本", "// QF例子\r\n\r\n[@Attack]    // 人或随从攻击列表指定怪物，会触发\r\n#IF\r\nCOMPVAL <$TARINFO_FNAME> = 蜈蚣\r\nCOMPVAL <$T.HP> > 50\r\n#ACT\r\nT.HUMANHP - 50\r\nSENDMSG 7 目标【<$TARINFO_NAME>】血量<$T.HP>\r\n\r\n#IF\r\nCOMPVAL <$ATTINFO_RACE> ! 1  // Race=1 是玩家，这里就是非玩家（即随从）\r\n#ACT\r\nSENDMSG 7 你的随从<$ATTINFO_NAME>正在攻击<$TARINFO_NAME>\r\n\r\n \r\n\r\n[@UnderAttack]      // 被人或随从或列表指定怪物攻击，会触发\r\n#IF\r\nCOMPVAL <$ATTINFO_FNAME> = 蜈蚣    // 蜈蚣打随从或自己\r\n#ACT\r\nHUMANHP - 50      // 主人扣血\r\n\r\n#IF\r\nCOMPVAL <$ATTINFO_MNAME> ! \"\"    // 攻击者主人名称不为空\r\n#ACT\r\nSENDMSG 7 【<$ATTINFO_MNAME>】的随从<$ATTINFO_NAME>正在攻击你\r\n#ELSEIF\r\nCOMPVAL <$TARINFO_RACE> ! 1     // 自己的随从\r\n#ACT\r\nSENDMSG 7 【<$ATTINFO_NAME>】正在攻击你的随从【<$TARINFO_NAME>】\r\n#ELSEACT\r\nSENDMSG 7 【<$ATTINFO_NAME>】正在攻击你\r\n");
                    CompletionDataList.Add(cd);
                    BlueCheckedData.Add("QFunction--杀怪触发脚本", "// QF例子\r\n\r\n[@Attack]    // 人或随从攻击列表指定怪物，会触发\r\n#IF\r\nCOMPVAL <$TARINFO_FNAME> = 蜈蚣\r\nCOMPVAL <$T.HP> > 50\r\n#ACT\r\nT.HUMANHP - 50\r\nSENDMSG 7 目标【<$TARINFO_NAME>】血量<$T.HP>\r\n\r\n#IF\r\nCOMPVAL <$ATTINFO_RACE> ! 1  // Race=1 是玩家，这里就是非玩家（即随从）\r\n#ACT\r\nSENDMSG 7 你的随从<$ATTINFO_NAME>正在攻击<$TARINFO_NAME>\r\n\r\n \r\n\r\n[@UnderAttack]      // 被人或随从或列表指定怪物攻击，会触发\r\n#IF\r\nCOMPVAL <$ATTINFO_FNAME> = 蜈蚣    // 蜈蚣打随从或自己\r\n#ACT\r\nHUMANHP - 50      // 主人扣血\r\n\r\n#IF\r\nCOMPVAL <$ATTINFO_MNAME> ! \"\"    // 攻击者主人名称不为空\r\n#ACT\r\nSENDMSG 7 【<$ATTINFO_MNAME>】的随从<$ATTINFO_NAME>正在攻击你\r\n#ELSEIF\r\nCOMPVAL <$TARINFO_RACE> ! 1     // 自己的随从\r\n#ACT\r\nSENDMSG 7 【<$ATTINFO_NAME>】正在攻击你的随从【<$TARINFO_NAME>】\r\n#ELSEACT\r\nSENDMSG 7 【<$ATTINFO_NAME>】正在攻击你\r\n");
                     

                   
  
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

    }
}
