# AMS
* 基于.net core 2.2 的webapi 权限管理后台
* swagger接口测试地址http://localhost:1000/
# 技术栈
## JWT
* 基于Json Web Token做身份验证介质。
* 用户登录成功后颁发Token，调用需授权接口时在请求头中携带。
    * Sample：Authorization Bearer {Token}
## Model Validation
* 基于模型验证DataAnnotations验证请求参数。
* 校验失败返回自定义错误信息，方便前台处理。
## Custom PolicyProvider
* 基于.net core自定义授权策略，权限控制精确到Action。
## Localization 
* 基于.net core国际化，支持多项目模型验证国际化。
* 请求头中携带 Accept-Language Value：en or zh。
    * Sample：Accept-Language en
## Log
* 基于Serilog做日志记录，支持自定义列，支持原生Logging日志输出。
## 仓储模式 Repository
* 使用仓储模式设计减少代码耦合,更易扩展。
## Ioc
* 基于.net core原生依赖注入，配合仓储使用。
## ORM
* 基于.net core Entity Framework 做数据操作。
# 待完善
* JWT令牌过期后自动刷新
    * 思路：增加Refresh Token做刷新令牌跟Token一起下发，token过期后使用Refresh token 刷新token，Refresh Token存入服务端内存数据库
* 打包成nuget包
    * 思路：安装即用，快速搭建项目，允许修改项目名
* 增加vue前端管理界面
    * 思路：慢慢来
