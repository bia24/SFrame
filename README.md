# SFrame
A simple framework for Unity game developing
## 项目说明
本项目旨在完成一个完整的unity游戏开发基本框架，包括：
* 配置文件模块
* 日志模块
* 资源加载模块
* UI模块
* 消息中心模块
* 动画管理模块
* 战斗模块
******
目前已完成的模块:    
* **消息中心模块**    
* **UI模块**    
参考：[游戏框架设计](https://www.cnblogs.com/LiuGuozhu/p/6416098.html)
## 更多使用说明
1. 关于游戏框架的所有文件保存在Assets/SFrame/目录下，使用本框架内容，请引入命名空间 **SFrame**     
2. 除1中目录外，其余文件为测试调试文件，每个模块对应一个Scene方便测试。
3. UI模块涉及UI面板prefabs的创建，请保证Assets/Resources/UI/内的预制体资源正确存在，和每个面板预制体对应的是Assets/TempScripts/UIMoudle/下的脚本。

## 问题存在     
**消息中心模块**        
Message moudle 目前已进行了优化，改为嵌套dictionary结构，使消息定位更加准确，操作码结构更加清晰。暂无问题存在               
******    
**UI模块**   
1. 结构冗余，存在优化可能：    
UI中的类型结构体，包括UIShow，UIPosType，UIPellucidityType，存在性有待商榷；     
Canvas预制体中预设了三个节点，配合UIPosType使用，略显多余。关于层级管理，可以参考UIMaskMgr中动态改变Gameobject的transform位置来实现置顶显示；     
因此，对于遮罩“模态”管理也不需要存在，后期存在优化可能；     
2. 存在Bug：     
由于UIShow结构的判定复杂性，对于三种类型混合使用的面板，需要进行前期安排部署，才能达到效果。不合适的搭配将出现难以预料的问题；    
另外，能确定的是，对于使用了UIMaskMgr“模态”功能的弹出窗体，其上不能再出现UIShow 为Normal和HideOther类型的窗体，不然功能失效。这也是1.里提到的问题所致，结构过于复杂冗余，导致UIMaskMgr判定条件不完善；      
*********

## 关于后期和优化
1. 针对未完成的模块功能，由于时间问题，暂时先搁置。后面搜集到比较好的框架模块，可以参考，并结合进来。比较重要的是资源、动画、战斗。     
2. UI模块的重构和优化：     
结合之前学习的另一种UI框架设计，目前我想了大致的优化思路：    
   
   1. UIManager需要存在，提供相关字典，存储UI面板的引用，并对外提供open、close接口，在这里只处理它们的层级管理逻辑，并调用UIBase中各自的display、hide、redisplay、freeze虚方法处理各自的逻辑，一切以减少互相使用引用，减少耦合为目的；       
   2. UIBase，每个面板的基类，可以使用更为简洁的类型Enum，减少结构冗余，分为“底层”“弹出”两类即可。
