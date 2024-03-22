//
//  GFYAccountManagerBridge.mm
//  GfycatScreenCaptureDemo
//
//  Created by Victor Pavlychko on 6/22/18.
//  Copyright © 2018 Gfycat. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <GfycatScreenCapture/GfycatScreenCapture.h>

__BEGIN_DECLS

void _GFYAccountManager_setClientCredentials(const char *appClientID, const char *appClientSecret)
{
    [[GfycatApi shared] setAppClientID:[NSString stringWithUTF8String:appClientID]
                            withSecret:[NSString stringWithUTF8String:appClientSecret]];
}

bool _GFYAccountManager_isLoggedIn()
{
    return [GfycatApi shared].isLoggedIn;
}

const char *_GFYAccountManager_getUsername()
{
    BOOL isLoggedIn = [GfycatApi shared].isLoggedIn;
    NSString *username = [GfycatApi shared].username;
    return (isLoggedIn && username) ? strdup(username.UTF8String) : NULL;
}

void _GFYAccountManager_login(void *cookie, void (*completionHandler)(void *, bool))
{
    [[GFYAccountManager sharedManager] loginUserFromViewController:[UIApplication sharedApplication].keyWindow.rootViewController withCompletion:^(BOOL succeeded) {
        completionHandler(cookie, succeeded);
    }];
}

void _GFYAccountManager_logout()
{
    [[GfycatApi shared] logout];
}

__END_DECLS
