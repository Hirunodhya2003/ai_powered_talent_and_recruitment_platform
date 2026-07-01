import { DashboardLayout } from "../../components/DashboardLayout";
import { GlassCard } from "../../components/GlassCard";
import { Badge } from "../../components/ui/badge";
import { Bell } from "lucide-react";

export function SeekerNotifications() {
  const items = [
    ["Your application is under review.", "WSO2 is reviewing APP-2026-1842.", "18/06/2026", "10:45", false],
    ["You have been shortlisted.", "99x shortlisted you for Frontend Developer Intern.", "18/06/2026", "09:15", false],
    ["New job matches found.", "3 new roles match your React and ASP.NET Core skills.", "17/06/2026", "16:20", true],
    ["Your profile is 90% complete.", "Add portfolio links to reach 100% profile strength.", "17/06/2026", "11:05", true],
  ] as const;
  return <DashboardLayout role="jobseeker"><div className="space-y-6"><div><p className="text-sm text-[#D4AF37]">Dashboard / Notifications</p><h1 className="mt-2 text-3xl font-semibold text-white">Notifications</h1><p className="text-gray-400">Application, interview, message, AI recommendation, and profile alerts.</p></div><div className="space-y-3">{items.map(([title,msg,date,time,read]) => <GlassCard key={title} className={`p-5 ${!read ? "border-l-4 border-l-[#D4AF37]" : ""}`}><div className="flex gap-4"><div className="rounded-xl bg-[#D4AF37]/10 p-3 h-fit"><Bell className="h-5 w-5 text-[#D4AF37]" /></div><div className="flex-1"><div className="flex flex-wrap justify-between gap-3"><h2 className="text-white font-semibold">🔔 {title}</h2><Badge variant={read ? "outline" : "default"}>{read ? "Read" : "Unread"}</Badge></div><p className="text-gray-300 mt-1">{msg}</p><p className="text-xs text-gray-500 mt-3">{date} • {time}</p></div></div></GlassCard>)}</div></div></DashboardLayout>;
}
