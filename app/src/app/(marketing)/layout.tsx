import Navbar from "@/components/navigation/Navbar";
import { useEffect } from "react";

export default function AuthLayout({
  children,
}: {
  children: React.ReactNode;
}) {

  return (
    <>
      <div
        className={`flex flex-col min-h-screen  pb-8 lg:px-12 container mx-auto`}
      >
        <Navbar />
        {children}
        <footer className="mt-20 flex justify-between">
          <div className="flex flex-col gap-2 font-medium capitalize tracking-wider">
            <div className="  ">PRIVACY & LEGAL</div>
            <div className="">CONTACT</div>
          </div>
          <div>© 2023 - One Day, All rights reserved.</div>
        </footer>
      </div>
    </>
  );
}
