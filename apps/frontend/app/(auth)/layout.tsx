"use client";

import { cn } from "@/lib/utils";
import AppFooter from "../components/app-footer";
import AppHeader from "../components/app-header";
import {
  Card,
  CardContent,
  CardDescription,
  CardFooter,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";
import { useAppGlobalContext } from "../components/app-global-provider";

export default function AuthLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  const { appName } = useAppGlobalContext();
  return (
    <div className="flex flex-col min-h-screen w-full">
      <AppHeader />
      <div
        className={cn("w-full flex flex-1 justify-center items-center py-8")}
      >
        <Card className="border-2 w-full max-w-md">
          <CardHeader className="space-y-1 text-center">
            <CardTitle className="text-2xl font-bold">
              Welcome to {appName}
            </CardTitle>
            <CardDescription>
              Enter your credentials to access your account
            </CardDescription>
          </CardHeader>

          <CardContent>{children}</CardContent>

          <CardFooter className="flex flex-col space-y-4">
            <div className="text-center text-sm text-muted-foreground">
              By continuing, you agree to {appName}'s Terms of Service and
              Privacy Policy.
            </div>
          </CardFooter>
        </Card>
      </div>
      <AppFooter />
    </div>
  );
}
