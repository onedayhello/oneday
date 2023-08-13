'use client'

import { useRouter } from 'next/navigation';

const Navbar = () => {
    const router = useRouter();

    return (
        <nav className="flex items-center justify-between py-8">
        <h1 className="text-3xl text-[#08251C] font-bold font-sans">one day</h1>
        <div className="hidden lg:flex ml-auto mr-14 gap-12">
          <span className="text-xl tracking-wide">Mindfulness Journal</span>
          <span className="text-xl tracking-wide">Mood Tracker</span>
        </div>
        <div className="lg:hidden items-center h-11 p-3 ml-auto mr-4 space-y-2 bg-gray-500 rounded shadow">
          <span className="block w-8 h-0.5 bg-gray-100 animate-pulse"></span>
          <span className="block w-8 h-0.5 bg-gray-100 animate-pulse"></span>
          <span className="block w-8 h-0.5 bg-gray-100 animate-pulse"></span>
        </div>
        <button onClick={() => router.push('/auth/login')} className="flex items-center py-2 h-11 tracking-wide font-serif font-bold text-xl rounded px-10 text-white bg-[#00A870]" >
          Login
        </button>
      </nav>
    )
}

export default Navbar;