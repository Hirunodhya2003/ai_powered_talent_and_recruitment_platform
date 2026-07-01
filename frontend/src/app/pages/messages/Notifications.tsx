import { useState } from "react";
import { ModuleLayout } from "../../components/ModuleLayout";
import { GlassCard } from "../../components/GlassCard";
import { Button } from "../../components/ui/button";
import {
  MessageSquare,
  MessageCircle,
  Mail,
  Bell,
} from "lucide-react";

const messagesTabs = [
  { label: "Chat", path: "/messages/chat-list", icon: MessageSquare },
  { label: "Direct Message", path: "/messages/direct-chat", icon: MessageCircle },
  { label: "Email Center", path: "/messages/email-center", icon: Mail },
  { label: "Notifications", path: "/messages/notifications", icon: Bell },
];

type NotificationType = "Application" | "Interview" | "Message" | "System" | "JobMatch";

interface Notification {
  id: number;
  type: NotificationType;
  title: string;
  description: string;
  timeAgo: string;
  read: boolean;
  action?: string;
}

const allNotifications: Notification[] = [
  {
    id: 1,
    type: "Application",
    title: "New Application Received",
    description: "Marcus Johnson applied for Senior Frontend Engineer. 94% match score.",
    timeAgo: "2 min ago",
    read: false,
    action: "Review",
  },
  {
    id: 2,
    type: "Interview",
    title: "Interview Scheduled",
    description: "Technical interview for Priya Sharma confirmed for June 19 at 10:00 AM.",
    timeAgo: "15 min ago",
    read: false,
    action: "View Calendar",
  },
  {
    id: 3,
    type: "Message",
    title: "New Message from Carlos Rivera",
    description: "\"Excited about the opportunity! When should I expect to hear back?\"",
    timeAgo: "32 min ago",
    read: false,
    action: "Reply",
  },
  {
    id: 4,
    type: "JobMatch",
    title: "High-Quality Match Found",
    description: "Yuki Tanaka matches your Senior Data Engineer role at 97% compatibility.",
    timeAgo: "1h ago",
    read: false,
    action: "View Profile",
  },
  {
    id: 5,
    type: "Application",
    title: "Application Status Updated",
    description: "Aisha Patel moved to Panel Interview stage. Background check cleared.",
    timeAgo: "2h ago",
    read: true,
  },
  {
    id: 6,
    type: "System",
    title: "Background Check Complete",
    description: "Verification for Carlos Rivera completed. All checks passed successfully.",
    timeAgo: "3h ago",
    read: true,
  },
  {
    id: 7,
    type: "Interview",
    title: "Interview Reminder",
    description: "You have 3 interviews scheduled for tomorrow, June 18. Calendar sync updated.",
    timeAgo: "4h ago",
    read: true,
    action: "View Schedule",
  },
  {
    id: 8,
    type: "Message",
    title: "Email Reply Received",
    description: "Emma Thompson replied to your Interview Invitation email with an updated portfolio.",
    timeAgo: "5h ago",
    read: true,
  },
  {
    id: 9,
    type: "JobMatch",
    title: "Bulk Applications Received",
    description: "7 new applicants for Senior Data Engineer in the last 24 hours. Top match: 94%.",
    timeAgo: "6h ago",
    read: true,
    action: "Review All",
  },
  {
    id: 10,
    type: "System",
    title: "Weekly Report Ready",
    description: "Your weekly recruiting report is ready. 67 interviews completed, 3 offers extended.",
    timeAgo: "8h ago",
    read: true,
    action: "Download",
  },
  {
    id: 11,
    type: "Application",
    title: "Candidate Withdrew Application",
    description: "Derek Walsh withdrew from the Cloud Architect position. Pipeline updated.",
    timeAgo: "Yesterday",
    read: true,
  },
  {
    id: 12,
    type: "Interview",
    title: "Interview Feedback Submitted",
    description: "Sarah Chen submitted technical interview feedback for Marcus Johnson: Excellent.",
    timeAgo: "Yesterday",
    read: true,
  },
];

const typeConfig: Record<NotificationType, { label: string; icon: string; dotColor: string; bgColor: string; textColor: string; filterLabel: string }> = {
  Application: {
    label: "Application",
    icon: "📋",
    dotColor: "bg-green-400",
    bgColor: "bg-green-400/10",
    textColor: "text-green-400",
    filterLabel: "Applications",
  },
  Interview: {
    label: "Interview",
    icon: "🗓",
    dotColor: "bg-[#D4AF37]",
    bgColor: "bg-[#D4AF37]/10",
    textColor: "text-[#D4AF37]",
    filterLabel: "Interviews",
  },
  Message: {
    label: "Message",
    icon: "💬",
    dotColor: "bg-blue-400",
    bgColor: "bg-blue-400/10",
    textColor: "text-blue-400",
    filterLabel: "Messages",
  },
  JobMatch: {
    label: "Job Match",
    icon: "⚡",
    dotColor: "bg-purple-400",
    bgColor: "bg-purple-400/10",
    textColor: "text-purple-400",
    filterLabel: "Job Matches",
  },
  System: {
    label: "System",
    icon: "🔔",
    dotColor: "bg-red-400",
    bgColor: "bg-red-400/10",
    textColor: "text-red-400",
    filterLabel: "System",
  },
};

const stats = [
  { label: "Total Today", value: "23", color: "text-[#D4AF37]" },
  { label: "Unread", value: "8", color: "text-blue-400" },
  { label: "Important", value: "4", color: "text-red-400" },
];

const filterTabs = ["All", "Unread", "Applications", "Interviews", "Messages", "System"];

export function MessagesNotifications() {
  const [activeFilter, setActiveFilter] = useState("All");
  const [notifications, setNotifications] = useState(allNotifications);

  const filtered = notifications.filter((n) => {
    if (activeFilter === "All") return true;
    if (activeFilter === "Unread") return !n.read;
    return typeConfig[n.type].filterLabel === activeFilter;
  });

  const markAllRead = () => {
    setNotifications(notifications.map((n) => ({ ...n, read: true })));
  };

  const clearAll = () => {
    setNotifications([]);
  };

  const markRead = (id: number) => {
    setNotifications(notifications.map((n) => n.id === id ? { ...n, read: true } : n));
  };

  return (
    <ModuleLayout
      title="Notification Center"
      subtitle="All platform notifications and alerts"
      icon={Bell}
      tabs={messagesTabs}
      backPath="/recruiter/dashboard"
      backLabel="Back to Portal"
    >
      {/* Stats */}
      <div className="grid grid-cols-3 gap-4 mb-6">
        {stats.map((s) => (
          <GlassCard key={s.label} className="p-4 text-center">
            <div className={`text-3xl font-bold ${s.color}`}>{s.value}</div>
            <div className="text-gray-400 text-xs mt-1">{s.label}</div>
          </GlassCard>
        ))}
      </div>

      {/* Controls */}
      <div className="flex flex-col sm:flex-row items-start sm:items-center justify-between gap-4 mb-4">
        <div className="flex gap-2 flex-wrap">
          {filterTabs.map((f) => (
            <button
              key={f}
              onClick={() => setActiveFilter(f)}
              className={`px-3 py-1.5 rounded-full text-xs font-medium border transition-all ${
                activeFilter === f
                  ? "bg-[#D4AF37] text-black border-[#D4AF37]"
                  : "text-gray-400 border-[#D4AF37]/20 hover:text-white hover:border-[#D4AF37]/40"
              }`}
            >
              {f}
              {f === "Unread" && (
                <span className="ml-1.5 bg-white/20 text-[inherit] rounded-full px-1 text-[10px]">
                  {notifications.filter((n) => !n.read).length}
                </span>
              )}
            </button>
          ))}
        </div>
        <div className="flex gap-2">
          <Button
            size="sm"
            variant="ghost"
            onClick={markAllRead}
            className="text-gray-400 hover:text-white text-xs h-8"
          >
            Mark All Read
          </Button>
          <Button
            size="sm"
            variant="ghost"
            onClick={clearAll}
            className="text-red-400 hover:text-red-300 text-xs h-8"
          >
            Clear All
          </Button>
        </div>
      </div>

      {/* Notification list */}
      <div className="space-y-2">
        {filtered.length === 0 && (
          <GlassCard className="p-12 text-center">
            <Bell className="h-12 w-12 text-gray-700 mx-auto mb-3" />
            <p className="text-gray-500">No notifications to display</p>
          </GlassCard>
        )}
        {filtered.map((n) => {
          const config = typeConfig[n.type];
          return (
            <GlassCard
              key={n.id}
              className={`px-5 py-4 flex items-start gap-4 transition-all hover:border-[#D4AF37]/30 ${
                !n.read ? "border-[#D4AF37]/25" : ""
              }`}
            >
              {/* Icon */}
              <div className={`w-10 h-10 rounded-full ${config.bgColor} flex items-center justify-center flex-shrink-0 text-lg`}>
                {config.icon}
              </div>

              {/* Content */}
              <div className="flex-1 min-w-0">
                <div className="flex items-start justify-between gap-4">
                  <div>
                    <div className="flex items-center gap-2 mb-0.5">
                      <span
                        className={`text-[10px] font-semibold uppercase tracking-wider px-2 py-0.5 rounded-full border ${config.bgColor} ${config.textColor}`}
                        style={{ borderColor: "currentColor", borderWidth: "1px", borderStyle: "solid", opacity: 1 }}
                      >
                        {config.label}
                      </span>
                      {!n.read && (
                        <div className={`w-2 h-2 rounded-full ${config.dotColor} flex-shrink-0`} />
                      )}
                    </div>
                    <div className={`text-sm font-semibold ${n.read ? "text-gray-300" : "text-white"}`}>
                      {n.title}
                    </div>
                    <div className="text-gray-400 text-xs mt-0.5 leading-relaxed">{n.description}</div>
                  </div>
                  <div className="flex flex-col items-end gap-2 flex-shrink-0">
                    <span className="text-gray-600 text-[10px] whitespace-nowrap">{n.timeAgo}</span>
                    {!n.read && (
                      <button
                        onClick={() => markRead(n.id)}
                        className="text-[10px] text-gray-500 hover:text-gray-300 underline"
                      >
                        Mark read
                      </button>
                    )}
                  </div>
                </div>
                {n.action && (
                  <div className="mt-2">
                    <Button
                      size="sm"
                      className={`h-7 text-xs px-3 border ${config.bgColor} ${config.textColor} hover:opacity-80`}
                      style={{ borderColor: "currentColor" }}
                    >
                      {n.action}
                    </Button>
                  </div>
                )}
              </div>
            </GlassCard>
          );
        })}
      </div>

      {/* Preferences link */}
      <div className="mt-6 text-center">
        <button className="text-gray-500 text-sm hover:text-[#D4AF37] transition-colors underline underline-offset-4">
          Manage Notification Preferences
        </button>
      </div>
    </ModuleLayout>
  );
}
