import type { Metadata } from "next";
import { Geist, Geist_Mono } from "next/font/google";
import "./globals.css";
import ProviderComposer from "@/components/providers/provider-composer";
import { ThemeProvider } from "@/components/providers/theme-provider";
import { SidebarProvider } from "@/components/ui/sidebar";
import { AppGlobalProvider } from "./components/app-global-provider";

const geistSans = Geist({
  variable: "--font-geist-sans",
  subsets: ["latin"],
});

const geistMono = Geist_Mono({
  variable: "--font-geist-mono",
  subsets: ["latin"],
});

export const metadata: Metadata = {
  title: "Frontend Scaffolding",
  description:
    "Generate a frontend scaffolding with Next.js, TypeScript, and Tailwind CSS",
};

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    //suppressHydrationWarning is used to prevent hydration errors
    // Recommended by Next.js when using next theme
    <html lang="en" suppressHydrationWarning>
      <body
        className={`${geistSans.variable} ${geistMono.variable} antialiased`}
      >
        <ProviderComposer
          providers={[
            [
              ThemeProvider,
              {
                attribute: "class",
                defaultTheme: "system",
                enableSystem: true,
                disableTransitionOnChange: true,
              },
            ],
            [SidebarProvider],
            [AppGlobalProvider]
          ]}
        >
          {children}
        </ProviderComposer>
      </body>
    </html>
  );
}
