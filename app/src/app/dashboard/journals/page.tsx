"use client";

import { Button } from "@/components/button/Button";
import { Dropdown } from "@/components/input/Dropdown";
import { TextInput } from "@/components/input/TextInput";
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
      <div className="border rounded-lg gap-1 lg:col-span-5 p-5  ">
        <div className="grid grid-cols-[1fr_1fr_1fr_auto] mb-2 gap-4">
          <span>Thought</span>
          <span>Type of thinking</span>
          <span>Better thought</span>
          <span>Actions</span>
          <div className="relative grid grid-cols-subgrid items-top col-span-4">
            <TextInput />
            <Dropdown />
            <TextInput />
            <div className="flex gap-2 justify-center items-center">
              <Button variant="secondary">-</Button>
              <Button>+</Button>
            </div>
          </div>
        </div>

        <div className="flex gap-2">
          <Button>Submit</Button>
        </div>
      </div>

      <div className="flex flex-col gap-2">
        <RecentJournalsItem />
        <RecentJournalsItem />
        <RecentJournalsItem />
      </div>


    </div>
  );
};

export default Journal;
