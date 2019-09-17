# Internal-Archeage-API
This API can be used for bots, mods, automation, and quality of life enhancements. It uses Process.NET to read internal structures and call internal functions. In order for this library to be used, the CLR must also be loaded into Archeage's process.

# Structs Implemented
 SSystemGlobalEnvironment
 IEntity
 IEntityIt
 IEntitySystem
 IGame
 IGameFramework
 Vec3
 Quat
 
 # Lua
Archeage uses lua for alot of functionality, and has bindings for a significant amount of C++ functions. Because of this, I created a wrapper around lua's C API functions. There are C# bindings for lua_PCall, lua_LoadBuffer, lua_GetField, and a few other functions.

Normally, none of the API functions would be callable due to lua's lack of multithreading support. However, this can be bypassed by using the API function Lua.Execute. It accepts a delegate, which then gets put into a queue and executed inside of the games lua thread(lua_PCall is the function that gets hooked to make this possible). 

Before you can access the lua API, Hooks.HookPCall must be called(only once, ideally at the start of your program).
