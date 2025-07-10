"use client";
import { cn } from "@/lib/utils";
import { Bot } from "lucide-react";

export default function SampleHeader({
  children,
}: {
  children?: React.ReactNode;
}) {
  return (
    <header
      className={cn(
        "sticky top-0 z-40 w-full border-b bg-background/95 backdrop-blur supports-[backdrop-filter]:bg-background/60",
        "px-4 md:px-6" // Adjust padding
      )}
    >
      <div className="container flex h-16 items-center justify-between">
        <div className="flex items-center gap-2 text-xl font-bold">
          {/* Replace with your app icon or logo */}
          <Bot className="h-6 w-6 text-primary" />
          {/* Replace with your app name */}
          <span>App Name</span>
        </div>
        {children}
      </div>
    </header>
  );
}
