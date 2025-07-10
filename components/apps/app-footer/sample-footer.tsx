"use client";
import { Bot } from "lucide-react";
import Link from "next/link";

export default function SampleFooter() {
  return (
    <footer className="w-full border-t bg-background">
      <div className="container flex flex-col items-center justify-center gap-4 py-6 md:h-16 md:flex-row md:py-0">
        <div className="flex flex-col items-center gap-4 px-8 md:flex-row md:gap-2 md:px-0">
          <div className="flex items-center gap-2">
            {/* Replace with your app icon or logo */}
            <Bot className="h-4 w-4 text-primary" />
            {/* Replace with your copyright period & app name */}
            <p className="text-center text-sm leading-loose md:text-left">
              &copy; 2023 App Name. All rights reserved.
            </p>
          </div>
          <nav className="flex gap-4 sm:gap-6">
            <Link
              href="#"
              className="text-xs hover:underline underline-offset-4"
            >
              Terms
            </Link>
            <Link
              href="#"
              className="text-xs hover:underline underline-offset-4"
            >
              Privacy
            </Link>
            <Link
              href="#"
              className="text-xs hover:underline underline-offset-4"
            >
              Help
            </Link>
          </nav>
        </div>
      </div>
    </footer>
  );
}
