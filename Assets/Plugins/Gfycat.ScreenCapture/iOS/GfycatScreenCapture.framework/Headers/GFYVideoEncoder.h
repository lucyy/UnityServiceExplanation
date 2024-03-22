//
//  GFYVideoEncoder.h
//  GfycatScreenCapture
//
//  Created by Victor Pavlychko on 8/3/17.
//  Copyright © 2017 Gfycat. All rights reserved.
//

#import <AVFoundation/AVFoundation.h>

@class GFYVideoEncoder;

@protocol GFYVideoEncoderOutput <NSObject>

- (void)videoEncoder:(GFYVideoEncoder *)encoder didProduceSampleBuffer:(CMSampleBufferRef)sampleBuffer forFrame:(NSInteger)frameIndex;

@end

@interface GFYVideoEncoder : NSObject

@property (nonatomic, weak) id<GFYVideoEncoderOutput> output;

@property (nonatomic, assign) CGSize size;
@property (nonatomic, assign) CGFloat scale;
@property (nonatomic, readonly) CGSize pixelSize;

@property (nonatomic, assign) NSInteger frameRate;
@property (nonatomic, assign) NSInteger keyframeRate;

@property (nonatomic, assign) NSInteger averageBitrate;
@property (nonatomic, assign) CGFloat encodingBppps; // bits per pixer per second

- (void)start;
- (void)cancel;
- (void)encodeFrameAt:(CFTimeInterval)timestamp render:(dispatch_block_t)renderBlock;
- (void)flushSamplesWithCompletionHandler:(dispatch_block_t)completionHandler;

@end
