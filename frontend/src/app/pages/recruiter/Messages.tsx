import { useState } from "react";
import { DashboardLayout } from "../../components/DashboardLayout";
import { GlassCard } from "../../components/GlassCard";
import { Input } from "../../components/ui/input";
import { Button } from "../../components/ui/button";
import { Avatar, AvatarFallback } from "../../components/ui/avatar";
import { Search, Send } from "lucide-react";

export function RecruiterMessages() {
  const [message, setMessage] = useState("");

  return (
    <DashboardLayout role="recruiter">
      <div className="mb-6">
        <h1 className="text-3xl font-bold text-white mb-2">Messages</h1>
        <p className="text-gray-400">Communicate with candidates</p>
      </div>

      <GlassCard className="p-0 h-[calc(100vh-240px)]">
        <div className="grid grid-cols-3 h-full">
          <div className="border-r border-[#D4AF37]/20 p-4">
            <div className="relative mb-4">
              <Search className="absolute left-3 top-1/2 -translate-y-1/2 h-4 w-4 text-gray-400" />
              <Input placeholder="Search..." className="pl-10 bg-[#1E1E1E] border-[#D4AF37]/20 text-white" />
            </div>
          </div>
          <div className="col-span-2 flex flex-col">
            <div className="p-4 border-b border-[#D4AF37]/20">
              <h3 className="font-semibold text-white">Select a conversation</h3>
            </div>
            <div className="flex-1 p-4 flex items-center justify-center">
              <p className="text-gray-400">No conversation selected</p>
            </div>
            <div className="p-4 border-t border-[#D4AF37]/20">
              <div className="flex space-x-2">
                <Input
                  placeholder="Type a message..."
                  value={message}
                  onChange={(e) => setMessage(e.target.value)}
                  className="bg-[#1E1E1E] border-[#D4AF37]/20 text-white"
                />
                <Button><Send className="h-4 w-4" /></Button>
              </div>
            </div>
          </div>
        </div>
      </GlassCard>
    </DashboardLayout>
  );
}
