//
//  GFYViewRecorderBridge.mm
//  GfycatScreenCapture
//
//  Created by Victor Pavlychko on 8/11/17.
//  Copyright © 2017 Gfycat. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <GfycatScreenCapture/GfycatScreenCapture.h>

__BEGIN_DECLS

CFTypeRef _GFYViewRecorder_new(void)
{
    GFYViewRecorder *instance = [[GFYViewRecorder alloc] init];
    instance.sourceView = [UIApplication sharedApplication].keyWindow.rootViewController.view;
    return CFBridgingRetain(instance);
}

void _GFYViewRecorder_release(CFTypeRef box)
{
    CFRelease(box);
}

void _GFYViewRecorder_setOutputPath(CFTypeRef box, const char *outputPath)
{
    GFYViewRecorder *instance = (__bridge GFYViewRecorder *)box;
    NSURL *outputURL = outputPath ? [NSURL fileURLWithPath:[NSString stringWithUTF8String:outputPath]] : nil;
    [[NSFileManager defaultManager] removeItemAtURL:outputURL error:nil];
    [[NSFileManager defaultManager] createDirectoryAtURL:outputURL.URLByDeletingLastPathComponent withIntermediateDirectories:YES attributes:nil error:nil];
    instance.outputURL = outputURL;
}

const char *_GFYViewRecorder_getOutputPath(CFTypeRef box)
{
    GFYViewRecorder *instance = (__bridge GFYViewRecorder *)box;
    return instance.outputURL ? strdup(instance.outputURL.absoluteURL.path.UTF8String) : NULL;
}

CFTypeRef _GFYViewRecorder_getSourceView(CFTypeRef box)
{
    GFYViewRecorder *instance = (__bridge GFYViewRecorder *)box;
    return (__bridge CFTypeRef)instance.sourceView;
}

void _GFYViewRecorder_setSourceView(CFTypeRef box, CFTypeRef sourceView)
{
    GFYViewRecorder *instance = (__bridge GFYViewRecorder *)box;
    instance.sourceView = (__bridge UIView *)sourceView;
}

void _GFYViewRecorder_getSourceRect(CFTypeRef box, float *left, float *top, float *width, float *height)
{
    GFYViewRecorder *instance = (__bridge GFYViewRecorder *)box;
    *left = CGRectGetMinX(instance.sourceRect);
    *top = CGRectGetMinY(instance.sourceRect);
    *width = CGRectGetWidth(instance.sourceRect);
    *height = CGRectGetHeight(instance.sourceRect);
}

void _GFYViewRecorder_setSourceRect(CFTypeRef box, float left, float top, float width, float height)
{
    GFYViewRecorder *instance = (__bridge GFYViewRecorder *)box;
    instance.sourceRect = CGRectMake(left, top, width, height);
}

float _GFYViewRecorder_getScale(CFTypeRef box)
{
    GFYViewRecorder *instance = (__bridge GFYViewRecorder *)box;
    return instance.scale;
}

void _GFYViewRecorder_setScale(CFTypeRef box, float scale)
{
    GFYViewRecorder *instance = (__bridge GFYViewRecorder *)box;
    instance.scale = scale;
}

int32_t _GFYViewRecorder_getFrameRate(CFTypeRef box)
{
    GFYViewRecorder *instance = (__bridge GFYViewRecorder *)box;
    return (int32_t)instance.frameRate;
}

void _GFYViewRecorder_setFrameRate(CFTypeRef box, int32_t frameRate)
{
    GFYViewRecorder *instance = (__bridge GFYViewRecorder *)box;
    instance.frameRate = frameRate;
}

int32_t _GFYViewRecorder_getKeyframeRate(CFTypeRef box)
{
    GFYViewRecorder *instance = (__bridge GFYViewRecorder *)box;
    return (int32_t)instance.keyframeRate;
}

void _GFYViewRecorder_setKeyframeRate(CFTypeRef box, int32_t keyframeRate)
{
    GFYViewRecorder *instance = (__bridge GFYViewRecorder *)box;
    instance.keyframeRate = keyframeRate;
}

float _GFYViewRecorder_getBufferDuration(CFTypeRef box)
{
    GFYViewRecorder *instance = (__bridge GFYViewRecorder *)box;
    return instance.bufferDuration;
}

void _GFYViewRecorder_setBufferDuration(CFTypeRef box, float bufferDuration)
{
    GFYViewRecorder *instance = (__bridge GFYViewRecorder *)box;
    instance.bufferDuration = bufferDuration;
}

void _GFYViewRecorder_start(CFTypeRef box)
{
    GFYViewRecorder *instance = (__bridge GFYViewRecorder *)box;
    [instance start];
}

void _GFYViewRecorder_pause(CFTypeRef box)
{
    GFYViewRecorder *instance = (__bridge GFYViewRecorder *)box;
    [instance pause];
}

void _GFYViewRecorder_resume(CFTypeRef box)
{
    GFYViewRecorder *instance = (__bridge GFYViewRecorder *)box;
    [instance resume];
}

void _GFYViewRecorder_cancel(CFTypeRef box)
{
    GFYViewRecorder *instance = (__bridge GFYViewRecorder *)box;
    [instance cancel];
}

void _GFYViewRecorder_finish(CFTypeRef box, void (*completionHandler)(void *), void *cookie)
{
    GFYViewRecorder *instance = (__bridge GFYViewRecorder *)box;
    [instance finishWithCompletionHandler:^{
        completionHandler(cookie);
    }];
}

__END_DECLS
