//
//  GFYVideoWriter.h
//  GfycatScreenCapture
//
//  Created by Victor Pavlychko on 8/3/17.
//  Copyright © 2017 Gfycat. All rights reserved.
//

#import <Foundation/Foundation.h>

@interface GFYVideoWriter : NSObject

@property (nonatomic, copy) NSURL *outputURL;

@property (nonatomic, assign) CGSize size;

- (void)start;
- (void)cancel;
- (void)appendSampleBuffer:(CMSampleBufferRef)sampleBuffer;
- (void)requestSampleBuffersUsingBlock:(CMSampleBufferRef(^)())sampleBufferProvider withCompletionHandler:(dispatch_block_t)completionHandler;
- (void)finishWithCompletionHandler:(dispatch_block_t)completionHandler;

@end
