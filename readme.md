# EasyBus

It is a simple library implement Pub\Sub and CommandBus patterns. This pattern is very comfortable for async communicate between your modules. 

For example WebApi:

You receiving request POST /api/user/signIn. 
You need find User in data storage, create access token and excute some additional steps, for example push unreaded notifications, log to audit table and update last login time.
In tipical implementation you call these functions on SignIn method, but responsibility of this method to verify user credentials and -  take access token and additional actions will be added or changed.

With using EasyBus you publish event for example "UserSignIned" with any details (login, id, etc), create some event handlers and EasyBus call these handlers parallel. First Handler push notifications, second update login time and etc.

I will add examples later, some examples are in tests
