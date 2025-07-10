"use client";

import {
  Sidebar,
  SidebarContent,
  SidebarGroup,
  SidebarGroupContent,
  SidebarGroupLabel,
  SidebarHeader,
} from "@/components/ui/sidebar";
import { cn } from "@/lib/utils";
import { useAppGlobalContext } from "../components/app-global-provider";
import ActivePopover from "@/components/apps/app-button/active-popover";
import { ActiveLink } from "@/components/apps/app-button/active-link";
import {
  House,
  LinkIcon,
  MessageSquare,
  RefreshCw,
  UserRoundPen,
  Wrench,
} from "lucide-react";
import AppHeader from "../components/app-header";
import AppFooter from "../components/app-footer";

const sideBarData = [
  {
    group: "Quick Links",
    groupIcon: LinkIcon,
    items: [
      {
        href: "/home",
        icon: House,
        label: "Home",
      },
    ],
  },
  {
    group: "Settings",
    groupIcon: Wrench,
    items: [
      {
        href: "",
        icon: RefreshCw,
        label: "Platform",
        selection: {
          value: "facebook",
          items: [
            {
              value: "facebook",
              label: "Facebook",
              iconImage: "/icons/facebook.svg",
            },
            {
              value: "google",
              label: "Google",
              iconImage: "/icons/google.svg",
            },
            {
              value: "telegram",
              label: "Telegram",
              iconImage: "/icons/telegram.svg",
            },
            {
              value: "apple",
              label: "Apple",
              iconImage: "/icons/apple.svg",
            },
          ],
        },
      },
      {
        href: "",
        icon: UserRoundPen,
        label: "Profile",
      },
    ],
  },
];

export default function AppLayout({ children }: { children: React.ReactNode }) {
  const { appName, AppLogo } = useAppGlobalContext();
  return (
    <>
      <Sidebar>
        <SidebarHeader
          className={cn(
            "flex flex-col h-16 items-start justify-center px-4 md:px-6"
          )}
        >
          <div className="flex items-center gap-2 text-xl font-bold">
            <AppLogo className="h-8 w-8 text-primary" />
            <span>{appName}</span>
          </div>
        </SidebarHeader>
        <SidebarContent className={cn("space-y-2 px-2")}>
          {sideBarData.map((g) => (
            <SidebarGroup
              key={g.group}
              className={cn("space-y-2 rounded-xl border shadow-sm")}
            >
              <SidebarGroupLabel>
                <div className="flex items-center gap-2">
                  {<g.groupIcon className="h-5 w-5 text-primary" />}
                  <h2 className="text-lg font-semibold text-primary">
                    {g.group}
                  </h2>
                </div>
              </SidebarGroupLabel>
              <SidebarGroupContent className="space-y-2">
                {g.items.map((item) =>
                  item.selection !== undefined ? (
                    <ActivePopover
                      key={item.label}
                      defaultValue={item.selection.value}
                      items={item.selection.items}
                      onSelect={(value) => {
                        // Handle selection
                      }}
                    />
                  ) : (
                    <ActiveLink key={item.label} href={item.href}>
                      {<item.icon className="h-4 w-4" />}
                      <span>{item.label}</span>
                    </ActiveLink>
                  )
                )}
              </SidebarGroupContent>
            </SidebarGroup>
          ))}
        </SidebarContent>
      </Sidebar>
      <div className="flex min-h-screen flex-col w-full">
        <AppHeader></AppHeader>
        <div className={cn("flex flex-col flex-1 px-4 md:px-6 py-4")}>
          {children}
        </div>
        <AppFooter />
      </div>
    </>
  );
}
