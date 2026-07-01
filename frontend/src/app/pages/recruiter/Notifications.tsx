import { useState } from "react";
import { DashboardLayout } from "../../components/DashboardLayout";
import { GlassCard } from "../../components/GlassCard";
import { Badge } from "../../components/ui/badge";
import { Button } from "../../components/ui/button";
import {
  Bell,
  CheckCircle,
  Calendar,
  MessageSquare,
  Briefcase,
  TrendingUp,
  AlertCircle,
  Users,
  Check,
} from "lucide-react";

type NotifCategory = "All" | "Applications" | "Interviews" | "Performance" | "System";

interface Notification {
  id: number;
  category: Exclude<NotifCategory, "All">;
  icon: React.ReactNode;
  iconBg: string;
  title: string;
  description: string;
  time: string;
  read: boolean;
}

const initialNotifications: Notification[] = [
  {
    id: 1,
    category: "Applications",
    icon: <Users className="w-5 h-5" />,
    iconBg: "bg-green-500/20 text-green-400",
    title: "12 New Applications – Senior Backend Engineer",
    description:
      "Your job posting at Acme Corp has received 12 new applications overnight. Top candidate AI score: 96%. Review now.",
    time: "15 minutes ago",
    read: false,
  },
  {
    id: 2,
    category: "Applications",
    icon: <CheckCircle className="w-5 h-5" />,
    iconBg: "bg-[#D4AF37]/20 text-[#D4AF37]",
    title: "High-Match Candidate Alert – Jordan Lee",
    description:
      "Jordan Lee (98% match) applied for Principal React Engineer. 7 years experience, open-source contributor. Recommended for fast-track.",
    time: "1 hour ago",
    read: false,
  },
  {
    id: 3,
    category: "Interviews",
    icon: <Calendar className="w-5 h-5" />,
    iconBg: "bg-blue-500/20 text-blue-400",
    title: "Interview Confirmed – Priya Sharma",
    description:
      "Priya Sharma accepted the Technical Interview invitation for June 20, 2026 at 10:00 AM PST. Video link generated.",
    time: "2 hours ago",
    read: false,
  },
  {
    id: 4,
    category: "Interviews",
    icon: <Calendar className="w-5 h-5" />,
    iconBg: "bg-orange-500/20 text-orange-400",
    title: "Interview Reschedule Request – Marcus Johnson",
    description:
      "Marcus Johnson has requested to reschedule the Panel Interview originally set for June 19. Please suggest new times.",
    time: "3 hours ago",
    read: false,
  },
  {
    id: 5,
    category: "Performance",
    icon: <TrendingUp className="w-5 h-5" />,
    iconBg: "bg-purple-500/20 text-purple-400",
    title: "Job Posting Milestone – 500 Views",
    description:
      "Your \"Staff Data Scientist\" posting has reached 500 views with a 4.2% apply rate. Consider boosting for broader reach.",
    time: "5 hours ago",
    read: true,
  },
  {
    id: 6,
    category: "Applications",
    icon: <Briefcase className="w-5 h-5" />,
    iconBg: "bg-cyan-500/20 text-cyan-400",
    title: "Candidate Withdrew Application – Alex Torres",
    description:
      "Alex Torres has withdrawn their application for Product Manager at Acme Corp. They cited accepting another offer.",
    time: "Yesterday",
    read: true,
  },
  {
    id: 7,
    category: "Performance",
    icon: <MessageSquare className="w-5 h-5" />,
    iconBg: "bg-[#D4AF37]/20 text-[#D4AF37]",
    title: "Weekly Hiring Report Ready",
    description:
      "Your week of June 9–15 report is ready: 34 applications, 8 interviews scheduled, 2 offers extended, 1 accepted.",
    time: "June 16, 2026",
    read: true,
  },
  {
    id: 8,
    category: "System",
    icon: <AlertCircle className="w-5 h-5" />,
    iconBg: "bg-red-500/20 text-red-400",
    title: "Job Posting Expiring Soon",
    description:
      "Your \"Senior DevOps Engineer\" posting expires in 3 days (June 20, 2026). Renew now to continue receiving applications.",
    time: "June 15, 2026",
    read: true,
  },
];

const tabs: NotifCategory[] = ["All", "Applications", "Interviews", "Performance", "System"];

export function RecruiterNotifications() {
  const [activeTab, setActiveTab] = useState<NotifCategory>("All");
  const [notifications, setNotifications] = useState<Notification[]>(initialNotifications);

  const markAllRead = () =>
    setNotifications((prev) => prev.map((n) => ({ ...n, read: true })));

  const markRead = (id: number) =>
    setNotifications((prev) =>
      prev.map((n) => (n.id === id ? { ...n, read: true } : n))
    );

  const filtered =
    activeTab === "All"
      ? notifications
      : notifications.filter((n) => n.category === activeTab);

  const unreadCount = notifications.filter((n) => !n.read).length;

  return (
    <DashboardLayout role="recruiter">
      <div className="p-6 space-y-6">
        {/* Header */}
        <div className="flex items-center justify-between">
          <div className="flex items-center gap-3">
            <Bell className="w-6 h-6 text-[#D4AF37]" />
            <div>
              <h1 className="text-2xl font-bold text-white">Notifications</h1>
              <p className="text-gray-400 text-sm mt-0.5">
                {unreadCount} unread notification{unreadCount !== 1 ? "s" : ""}
              </p>
            </div>
            {unreadCount > 0 && (
              <Badge className="bg-[#D4AF37] text-black font-bold text-xs px-2">
                {unreadCount}
              </Badge>
            )}
          </div>
          <Button
            variant="outline"
            onClick={markAllRead}
            className="border-[#D4AF37]/20 text-[#D4AF37] hover:bg-[#D4AF37]/10"
          >
            <Check className="w-4 h-4 mr-2" />
            Mark All as Read
          </Button>
        </div>

        {/* Tabs */}
        <div className="flex items-center gap-2 flex-wrap">
          {tabs.map((tab) => {
            const count =
              tab === "All"
                ? notifications.filter((n) => !n.read).length
                : notifications.filter((n) => n.category === tab && !n.read).length;
            return (
              <button
                key={tab}
                onClick={() => setActiveTab(tab)}
                className={`px-4 py-1.5 rounded-full text-sm font-medium transition-all flex items-center gap-1.5 ${
                  activeTab === tab
                    ? "bg-[#D4AF37] text-black"
                    : "bg-[#2D2D2D] text-gray-300 hover:bg-[#D4AF37]/20 hover:text-[#D4AF37]"
                }`}
              >
                {tab}
                {count > 0 && (
                  <span
                    className={`text-xs rounded-full px-1.5 py-0.5 font-bold ${
                      activeTab === tab
                        ? "bg-black/20 text-black"
                        : "bg-[#D4AF37]/20 text-[#D4AF37]"
                    }`}
                  >
                    {count}
                  </span>
                )}
              </button>
            );
          })}
        </div>

        {/* Notification List */}
        <div className="space-y-3">
          {filtered.length === 0 ? (
            <GlassCard className="p-6">
              <p className="text-center text-gray-400 py-8">
                No notifications in this category.
              </p>
            </GlassCard>
          ) : (
            filtered.map((notif) => (
              <GlassCard
                key={notif.id}
                className={`p-6 cursor-pointer transition-all hover:border-[#D4AF37]/30 ${
                  !notif.read ? "border-l-2 border-l-[#D4AF37]" : ""
                }`}
                onClick={() => markRead(notif.id)}
              >
                <div className="flex items-start gap-4">
                  <div
                    className={`w-10 h-10 rounded-xl flex items-center justify-center shrink-0 ${notif.iconBg}`}
                  >
                    {notif.icon}
                  </div>
                  <div className="flex-1 min-w-0">
                    <div className="flex items-start justify-between gap-2">
                      <div className="flex items-center gap-2 flex-wrap">
                        <h3
                          className={`font-medium ${
                            notif.read ? "text-gray-300" : "text-white"
                          }`}
                        >
                          {notif.title}
                        </h3>
                        {!notif.read && (
                          <span className="w-2 h-2 rounded-full bg-[#D4AF37] shrink-0 mt-0.5" />
                        )}
                      </div>
                      <span className="text-gray-500 text-xs shrink-0">{notif.time}</span>
                    </div>
                    <p className="text-gray-400 text-sm mt-1 leading-relaxed">
                      {notif.description}
                    </p>
                    <div className="flex items-center gap-2 mt-2">
                      <Badge
                        className={`text-xs ${
                          notif.category === "Applications"
                            ? "bg-green-500/10 text-green-400 border border-green-500/20"
                            : notif.category === "Interviews"
                            ? "bg-blue-500/10 text-blue-400 border border-blue-500/20"
                            : notif.category === "Performance"
                            ? "bg-purple-500/10 text-purple-400 border border-purple-500/20"
                            : "bg-gray-500/10 text-gray-400 border border-gray-500/20"
                        }`}
                      >
                        {notif.category}
                      </Badge>
                    </div>
                  </div>
                </div>
              </GlassCard>
            ))
          )}
        </div>
      </div>
    </DashboardLayout>
  );
}
