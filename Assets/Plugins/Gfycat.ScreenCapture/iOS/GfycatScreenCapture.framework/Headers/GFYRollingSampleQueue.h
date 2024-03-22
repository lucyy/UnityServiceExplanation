//
//  GFYRollingSampleQueue.h
//  GfycatScreenCapture
//
//  Created by Victor Pavlychko on 8/4/17.
//  Copyright © 2017 Gfycat. All rights reserved.
//

#import <AVFoundation/AVFoundation.h>

@interface GFYRollingSampleQueue : NSObject

@property (nonatomic, assign) NSInteger capacity;
@property (nonatomic, readonly) NSInteger count;

- (void)enqueueSampleBuffer:(CMSampleBufferRef)sampleBuffer;
- (CMSampleBufferRef)peekSampleBuffer;
- (CMSampleBufferRef)dequeueSampleBuffer;
- (void)purgeUntilKeyframe;

@end
