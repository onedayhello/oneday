"use client";

import { MouseEventHandler, ReactNode } from "react";
import Link from "next/link";
import Auth from "@/util/Auth";
import { usePathname, useRouter } from "next/navigation";

const NavbarItem = ({
  children,
  onClick,
  href,
}: {
  children: ReactNode;
  onClick?: MouseEventHandler<HTMLAnchorElement>;
  href?: string;
}) => {
  const pathname = usePathname();

  const active = pathname === href ? true : false;

  return (
    <Link
      onClick={onClick}
      className={`text-[#8F8A86] p-2 rounded ${
        active ? "bg-[#F1F4ED] text-[#474747]" : ""
      } `}
      href={href ?? ""}
    >
      {children}
    </Link>
  );
};

const App = ({ children }: { children: ReactNode }) => {
  const router = useRouter();

  return (
    <div className="flex">
      <aside className="flex shrink-0 flex-col content-center justify-between h-screen sticky top-0 p-10">
        <h1 className="text-3xl text-[#08251C] font-bold font-sans">one day</h1>
        <div className="flex flex-col gap-3">
          <NavbarItem href="/dashboard">Dashboard</NavbarItem>
          <NavbarItem href="/dashboard/journals">Journals</NavbarItem>
          <NavbarItem href="/dashboard/tasks">Your tasks </NavbarItem>
          <NavbarItem href="/dashboard/mood-tracker">Mood tracker</NavbarItem>
        </div>
        <div className="flex flex-col">
          <NavbarItem>Help & Information</NavbarItem>
          <NavbarItem
            href={"/"}
            onClick={() => {
              Auth.logout();
            }}
          >
            Logout
          </NavbarItem>
        </div>
      </aside>
      <main className="p-10 w-full">{children}</main>
    </div>
  );
};

export default App;
