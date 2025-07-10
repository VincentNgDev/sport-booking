"use client";

import { buttonVariants } from "@/components/ui/button";
import { Command, CommandEmpty, CommandGroup, CommandInput, CommandItem, CommandList } from "@/components/ui/command";
import { Popover, PopoverContent, PopoverTrigger } from "@/components/ui/popover";
import { useIsMobile } from "@/hooks/use-mobile";
import { cn } from "@/lib/utils";
import React from "react";
import ImageFallback from "../app-image/image-fallback";
import ButtonIconImage from "./button-icon-image";

type Item = {
  value: string;
  label: string;
  iconImage: string;
};

type ActivePopoverProps = {
  defaultValue?: string;
  items?: Item[];
  onSelect?: (value: string) => void;
};

export default function ActivePopover({
  defaultValue,
  items,
  onSelect,
}: ActivePopoverProps) {
  const [open, setOpen] = React.useState(false);
  const [selectedItem, selectItem] = React.useState<Item | undefined>(
    items?.find((item) => item.value === defaultValue)
  );

  const isMobile = useIsMobile();

  return (
    <Popover open={open} onOpenChange={setOpen}>
      <PopoverTrigger asChild>
        <ButtonIconImage
          className={cn(
            buttonVariants({ variant: "ghost" }),
            "w-full justify-start shadow-none px-3 m-0",
            open
              ? "bg-primary text-primary-foreground hover:bg-primary hover:text-primary-foreground dark:hover:bg-primary"
              : "bg-transparent text-primary"
          )}
          image={{
            alt: selectedItem?.label ?? "Select",
            src: selectedItem?.iconImage ?? "",
            height: 20,
            width: 16,
          }}
          buttonText={selectedItem?.label ?? "Select"}
        />
      </PopoverTrigger>
      <PopoverContent
        className="p-1 rounded-xl border shadow-sm bg-primary-foreground"
        side={isMobile ? undefined : "right"}
        align="start"
      >
        <Command>
          <CommandInput placeholder="Select..." />
          <CommandList>
            <CommandEmpty>No results found.</CommandEmpty>
            <CommandGroup>
              {items?.map((item) => (
                <CommandItem
                  key={item.value}
                  value={item.value}
                  onSelect={(value) => {
                    selectItem(items.find((item) => item.value === value));
                    setOpen(false);
                    onSelect?.(value);
                  }}
                >
                  <ImageFallback
                    fallbackSrc="/images/placeholder.png"
                    src={item.iconImage}
                    alt={item.label}
                    height={20}
                    width={20}
                  />
                  <span>{item.label}</span>
                </CommandItem>
              ))}
            </CommandGroup>
          </CommandList>
        </Command>
      </PopoverContent>
    </Popover>
  );
}
