"use client";

import { Button } from "@/components/button/Button";
import { EditorContent, useEditor } from "@tiptap/react";
import StarterKit from "@tiptap/starter-kit";
import Link from "next/link";

const RecentJournalsItem = () => {
  return (
    <div className="flex  justify-between rounded-lg p-4 hover:bg-[#F4F4F3] border">
      <div>
        <div className="flex gap-1 mb-2">
          <p className="border rounded-full px-3 text-sm text-[#6C6C6C]">CBT</p>
          <p className="border rounded-full px-3 text-sm text-[#6C6C6C]">
            Anxiety
          </p>
        </div>
        <h3 className="font-bold text-lg">Mindfulness Journal</h3>
        <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit</p>
      </div>
      <div className="text-sm text-[#C6C6C6]">5d</div>
    </div>
  );
};

const Tiptap = () => {
  const editor = useEditor({
    extensions: [StarterKit],
    content: "<p>Hello World! 🌎️</p>",

    editorProps: {
      attributes: {
        class: "border rounded-lg p-3",
      },
    },
  });

  return <EditorContent editor={editor} />;
};

const Journal = () => {
  return (
    <div className="flex flex-col gap-3">
      <Link href="/dashboard/journals/new-entry">
        <Button>New Entry</Button>
      </Link>

      <div className="flex flex-col gap-2">
        <RecentJournalsItem />
        <RecentJournalsItem />
        <RecentJournalsItem />
      </div>
    </div>
  );
};

export default Journal;
