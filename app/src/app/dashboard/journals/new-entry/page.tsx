"use client";

import { Button } from "@/components/button/Button";
import { EditorContent, useEditor } from "@tiptap/react";
import StarterKit from "@tiptap/starter-kit";
import Link from "next/link";

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
    <div className="">
      <div className="mb-2">
        <Tiptap />
      </div>

      <div className="flex gap-2">
        <Button variant="secondary">Cancel</Button>
        <Button>Submit</Button>
      </div>
    </div>
  );
};

export default Journal;
