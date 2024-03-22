//
//  GFYVideoUploaderBridge.mm
//  GfycatScreenCapture
//
//  Created by Victor Pavlychko on 8/11/17.
//  Copyright © 2017 Gfycat. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <GfycatScreenCapture/GfycatScreenCapture.h>

__BEGIN_DECLS

CFTypeRef _GFYVideoUploader_new(void)
{
    GFYVideoUploader *instance = [[GFYVideoUploader alloc] init];
    return CFBridgingRetain(instance);
}

void _GFYVideoUploader_release(CFTypeRef box)
{
    CFRelease(box);
}

const char *_GFYVideoUploader_getVideoPath(CFTypeRef box)
{
    GFYVideoUploader *instance = (__bridge GFYVideoUploader *)box;
    return instance.videoURL ? strdup(instance.videoURL.absoluteURL.path.UTF8String) : NULL;
}

void _GFYVideoUploader_setVideoPath(CFTypeRef box, const char *videoPath)
{
    GFYVideoUploader *instance = (__bridge GFYVideoUploader *)box;
    instance.videoURL = videoPath ? [NSURL fileURLWithPath:[NSString stringWithUTF8String:videoPath]] : nil;
}

const char *_GFYVideoUploader_getTitle(CFTypeRef box)
{
    GFYVideoUploader *instance = (__bridge GFYVideoUploader *)box;
    return instance.title ? strdup(instance.title.UTF8String) : NULL;
}

void _GFYVideoUploader_setTitle(CFTypeRef box, const char *title)
{
    GFYVideoUploader *instance = (__bridge GFYVideoUploader *)box;
    instance.title = title ? [NSString stringWithUTF8String:title] : nil;
}

CFTypeRef _GFYVideoUploader_getListener(CFTypeRef box)
{
    GFYVideoUploader *instance = (__bridge GFYVideoUploader *)box;
    return (__bridge CFTypeRef)instance.listener;
}

void _GFYVideoUploader_setListener(CFTypeRef box, CFTypeRef listener)
{
    GFYVideoUploader *instance = (__bridge GFYVideoUploader *)box;
    instance.listener = (__bridge id<GFYVideoUploaderListener>)listener;
}

const char *_GFYVideoUploader_getGfyId(CFTypeRef box)
{
    GFYVideoUploader *instance = (__bridge GFYVideoUploader *)box;
    return instance.gfyId ? strdup(instance.gfyId.UTF8String) : NULL;
}

const char *_GFYVideoUploader_getGfycatURL(CFTypeRef box)
{
    GFYVideoUploader *instance = (__bridge GFYVideoUploader *)box;
    return instance.gfycatURL ? strdup(instance.gfycatURL.absoluteString.UTF8String) : NULL;
}

void _GFYVideoUploader_uploadVideo(CFTypeRef box)
{
    GFYVideoUploader *instance = (__bridge GFYVideoUploader *)box;
    [instance uploadVideo];
}

#pragma mark -

@interface _GFYVideoUploaderListener : NSObject <GFYVideoUploaderListener>

@property (nonatomic, copy) void (^didRegisterGfycatWithId)(GFYVideoUploader *uploader, NSString *gfyId);
@property (nonatomic, copy) void (^didUploadGfycatWithId)(GFYVideoUploader *uploader, NSString *gfyId);
@property (nonatomic, copy) void (^didFailWithError)(GFYVideoUploader *uploader, NSError *error);

@end

@implementation _GFYVideoUploaderListener

- (void)videoUploader:(GFYVideoUploader *)uploader didRegisterGfycatWithId:(NSString *)gfyId
{
    _didRegisterGfycatWithId(uploader, gfyId);
}

- (void)videoUploader:(GFYVideoUploader *)uploader didUploadGfycatWithId:(NSString *)gfyId
{
    _didUploadGfycatWithId(uploader, gfyId);
}

- (void)videoUploader:(GFYVideoUploader *)uploader didFailWithError:(NSError *)error
{
    _didFailWithError(uploader, error);
}

@end

CFTypeRef _GFYVideoUploaderListener_new(void *cookie,
                                        void (*didRegisterGfycatWithId)(void *cookie, CFTypeRef uploader, const char *gfyId),
                                        void (*didUploadGfycatWithId)(void *cookie, CFTypeRef uploader, const char *gfyId),
                                        void (*didFailWithError)(void *cookie, CFTypeRef uploader, const char *error))
{
    _GFYVideoUploaderListener *instance = [[_GFYVideoUploaderListener alloc] init];
    instance.didRegisterGfycatWithId = ^(GFYVideoUploader *uploader, NSString *gfyId) {
        didRegisterGfycatWithId(cookie, (__bridge CFTypeRef)uploader, gfyId.UTF8String);
    };
    instance.didUploadGfycatWithId = ^(GFYVideoUploader *uploader, NSString *gfyId) {
        didUploadGfycatWithId(cookie, (__bridge CFTypeRef)uploader, gfyId.UTF8String);
    };
    instance.didFailWithError = ^(GFYVideoUploader *uploader, NSError *error) {
        didFailWithError(cookie, (__bridge CFTypeRef)uploader, error.localizedDescription.UTF8String);
    };
    return CFBridgingRetain(instance);
}

void _GFYVideoUploaderListener_release(CFTypeRef box)
{
    CFRelease(box);
}

__END_DECLS
