"use client";

import { StaticImport } from "next/dist/shared/lib/get-img-props";
import ImageFallback from "../app-image/image-fallback";
import { Button } from "@/components/ui/button";

export type ButtonImage = {
  src: string | StaticImport;
  alt: string;
  height: number;
  width: number;
};

type ButtonIconImageProps = {
  image?: ButtonImage;
  buttonText: string;
} & React.ComponentProps<"button">;

export default function ButtonIconImage({
  image,
  children,
  buttonText,
  ...props
}: ButtonIconImageProps) {
  return (
    <Button {...props}>
      {image && (
        <ImageFallback
          src={image.src}
          fallbackSrc="/images/placeholder.png"
          alt={image.alt}
          height={image.height}
          width={image.width}
        />
      )}
      {children}
      {buttonText}
    </Button>
  );
}
