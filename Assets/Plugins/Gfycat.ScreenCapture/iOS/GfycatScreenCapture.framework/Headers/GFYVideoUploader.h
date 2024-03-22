//
//  GFYVideoUploader.h
//  GfycatScreenCapture
//
//  Created by Victor Pavlychko on 8/9/17.
//  Copyright © 2017 Gfycat. All rights reserved.
//

#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

@class GFYVideoUploader;

@protocol GFYVideoUploaderListener <NSObject>

- (void)videoUploader:(GFYVideoUploader *)uploader didRegisterGfycatWithId:(NSString *)gfyId;
- (void)videoUploader:(GFYVideoUploader *)uploader didUploadGfycatWithId:(NSString *)gfyId;
- (void)videoUploader:(GFYVideoUploader *)uploader didFailWithError:(NSError *)error;

@end

@interface GFYVideoUploader : NSObject

@property (nonatomic, nullable, copy) NSURL *videoURL;
@property (nonatomic, nullable, copy) NSString *title;
@property (nonatomic, nullable, weak) id<GFYVideoUploaderListener> listener;

@property (nonatomic, nullable, readonly) NSString *gfyId;
@property (nonatomic, nullable, readonly) NSURL *gfycatURL;

- (void)uploadVideo;

@end

NS_ASSUME_NONNULL_END
