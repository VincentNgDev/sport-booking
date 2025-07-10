"use client";

import { Dumbbell, LucideIcon } from "lucide-react";
import React from "react";

type AppGlobalProviderContext = {
  appName: string;
  AppLogo: LucideIcon;
  appCopyRight: string;
};

const appGlobalContext = React.createContext<
  AppGlobalProviderContext | undefined
>(undefined);

export function useAppGlobalContext() {
  const context = React.useContext(appGlobalContext);
  if (!context) {
    throw new Error(
      "useAppGlobalContext must be used within an AppGlobalProvider"
    );
  }
  return context;
}

export function AppGlobalProvider({ children }: { children: React.ReactNode }) {
  const context = React.useMemo(
    () => ({
      appName: "SportBook",
      AppLogo: Dumbbell,
      appCopyRight: `© ${new Date().getFullYear()} SportBook. All rights reserved.`,
    }),
    []
  );

  return (
    <appGlobalContext.Provider value={context}>
      {children}
    </appGlobalContext.Provider>
  );
}
