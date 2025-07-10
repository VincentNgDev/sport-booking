"use client";
import { Skeleton } from "@/components/ui/skeleton";
import { ImageProps } from "next/image";
import Image from "next/image";
import React from "react";

interface ImageWithFallbackProps extends ImageProps {
  fallbackSrc: string;
}

export default function ImageFallback({
  src,
  fallbackSrc,
  unoptimized = true,
  ...props
}: ImageWithFallbackProps) {
  const [isLoading, setIsLoading] = React.useState(true);
  const [imgSrc, setImgSrc] = React.useState(src);

  const handleSetImage = React.useCallback(
    (src: ImageWithFallbackProps["src"]) => {
      setIsLoading(true);
      setImgSrc(src);
    },
    [setImgSrc]
  );

  React.useEffect(() => {
    handleSetImage(src);
  }, [src]);

  const handleLoadingComplete = React.useCallback(
    (img: HTMLImageElement) => {
      setIsLoading(false);
      if (img.naturalWidth === 0) {
        handleSetImage(fallbackSrc);
      }
    },
    [handleSetImage]
  );

  const handleError = React.useCallback(() => {
    handleSetImage(fallbackSrc);
  }, [handleSetImage]);

  return (
    <>
      {isLoading && (
        <div
          className="flex flex-col space-y-3"
          style={{ height: props.height, width: props.width }}
        >
          <Skeleton className="rounded-xl flex-1" />
          <div className="space-y-2">
            <Skeleton className="rounded-xl h-[20px]" />
            <Skeleton className="rounded-xl h-[20px] w-[80%]" />
          </div>
        </div>
      )}
      <Image
        {...props}
        src={imgSrc}
        unoptimized={unoptimized}
        onLoad={(e) => {
          handleLoadingComplete(e.currentTarget);
        }}
        //onLoadingComplete={handleLoadingComplete} // deprecated in Next.js 14
        onError={handleError}
      />
    </>
  );
}
