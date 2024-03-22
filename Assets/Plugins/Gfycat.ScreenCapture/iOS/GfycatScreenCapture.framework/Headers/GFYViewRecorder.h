//
//  GFYViewRecorder.h
//
//  Created by Victor Pavlychko on 8/26/16.
//  Copyright © 2016. All rights reserved.
//

#import <UIKit/UIKit.h>

@interface GFYViewRecorder : NSObject

@property (nonatomic, strong) UIView *sourceView;
@property (nonatomic, assign) CGRect sourceRect;
@property (nonatomic, assign) CGFloat scale;

@property (nonatomic, copy) NSURL *outputURL;

@property (nonatomic, assign) NSInteger frameRate;
@property (nonatomic, assign) NSInteger keyframeRate;
@property (nonatomic, assign) NSTimeInterval bufferDuration;
@property (nonatomic, assign) NSInteger averageBitrate;
@property (nonatomic, assign) CGFloat encodingBppps; // bits per pixer per second

- (void)start;
- (void)pause;
- (void)resume;
- (void)cancel;
- (void)finishWithCompletionHandler:(dispatch_block_t)completionHandler;

@end
