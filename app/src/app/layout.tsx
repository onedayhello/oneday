'use client';

import { useEffect } from "react";
import "./globals.css";
import { Roboto, Alegreya } from "next/font/google";
import { useRouter } from 'next/navigation'

const inter = Alegreya({ subsets: ["latin"], variable: "--font-alegreya" });
const roboto = Roboto({
  subsets: ["latin"],
  weight: ["300", "400", "500", "700"],
  variable: "--font-roboto",
});

export const metadata = {
  title: "One Day",
  description: "Open-source mood tracker and mindfulness journal",
};

export default function RootLayout({
  children,
}: {
  children: React.ReactNode;
}) {
    const router = useRouter()

  useEffect(() => {
    const token = localStorage.getItem("token");

    if (!token) {
      return;
      // user not logged in
    }    

    // TODO: Call auth endpoint to check if token is still valid
   
    router.push('/dashboard')
  });

  return (
    <html lang="en" className={`${inter.variable} ${roboto.variable}`}>
      <body>{children}</body>
    </html>
  );
}
