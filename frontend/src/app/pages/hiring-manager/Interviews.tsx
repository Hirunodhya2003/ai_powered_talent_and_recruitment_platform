import { useState } from "react";
import { useNavigate } from "react-router";
import { DashboardLayout } from "../../components/DashboardLayout";
import { GlassCard } from "../../components/GlassCard";
import { StatCard } from "../../components/StatCard";
import { Badge } from "../../components/ui/badge";
import { Button } from "../../components/ui/button";
import {
  Calendar,
  Clock,
  Video,
  FileText,
  MessageSquare,
  Users,
  CheckCircle,
  AlertCircle,
  ChevronLeft,
  ChevronRight,
} from "lucide-react";

export function ManagerInterviews() {
  const navigate = useNavigate();
  const [selectedDay, setSelectedDay] = useState(2);

  const weekDays = [
    { label: "Mon", date: "16", count: 2 },
    { label: "Tue", date: "17", count: 3 },
    { label: "Wed", date: "18", count: 1 },
    { label: "Thu", date: "19", count: 4 },
    { label: "Fri", date: "20", count: 2 },
    { label: "Sat", date: "21", count: 0 },
    { label: "Sun", date: "22", count: 0 },
  ];

  const interviews = [
    {
      id: 1,
      candidate: "Alexandra Chen",
      position: "VP of Engineering",
      company: "Internal",
      date: "Tue, Jun 17, 2026",
      time: "10:00 AM – 11:00 AM",
      panel: ["Sarah Mills (HR)", "James Cooper (CTO)", "You"],
      myRole: "Decision Maker",
      status: "Upcoming",
      avatarInitials: "AC",
    },
    {
      id: 2,
      candidate: "Marcus Thompson",
      position: "Senior Product Manager",
      company: "Internal",
      date: "Tue, Jun 17, 2026",
      time: "2:00 PM – 3:00 PM",
      panel: ["Rachel Kim (HR)", "You"],
      myRole: "Interviewer",
      status: "Upcoming",
      avatarInitials: "MT",
    },
    {
      id: 3,
      candidate: "Priya Patel",
      position: "Data Science Lead",
      company: "Internal",
      date: "Wed, Jun 18, 2026",
      time: "11:30 AM – 12:30 PM",
      panel: ["Tom Walsh (HR)", "Linda Foster (Data)", "You"],
      myRole: "Observer",
      status: "Scheduled",
      avatarInitials: "PP",
    },
    {
      id: 4,
      candidate: "James Okafor",
      position: "Senior Full Stack Engineer",
      company: "Internal",
      date: "Thu, Jun 19, 2026",
      time: "9:00 AM – 10:00 AM",
      panel: ["Anna Liu (HR)", "Dev Team", "You"],
      myRole: "Decision Maker",
      status: "Upcoming",
      avatarInitials: "JO",
    },
    {
      id: 5,
      candidate: "Sofia Rodriguez",
      position: "UX Research Lead",
      company: "Internal",
      date: "Thu, Jun 19, 2026",
      time: "3:30 PM – 4:30 PM",
      panel: ["Karen Scott (HR)", "Design Team", "You"],
      myRole: "Interviewer",
      status: "Awaiting Feedback",
      avatarInitials: "SR",
    },
    {
      id: 6,
      candidate: "David Kim",
      position: "Cloud Infrastructure Architect",
      company: "Internal",
      date: "Mon, Jun 16, 2026",
      time: "10:00 AM – 11:30 AM",
      panel: ["HR Team", "You", "CTO"],
      myRole: "Decision Maker",
      status: "Completed",
      avatarInitials: "DK",
    },
    {
      id: 7,
      candidate: "Natalie Brooks",
      position: "Marketing Director",
      company: "Internal",
      date: "Mon, Jun 16, 2026",
      time: "2:00 PM – 3:00 PM",
      panel: ["HR", "CMO", "You"],
      myRole: "Observer",
      status: "Awaiting Feedback",
      avatarInitials: "NB",
    },
  ];

  const getRoleBadgeStyle = (role: string) => {
    switch (role) {
      case "Decision Maker":
        return "bg-[#D4AF37]/20 text-[#D4AF37] border-[#D4AF37]/40";
      case "Interviewer":
        return "bg-blue-500/20 text-blue-400 border-blue-500/30";
      case "Observer":
        return "bg-gray-500/20 text-gray-400 border-gray-500/30";
      default:
        return "bg-gray-500/20 text-gray-400 border-gray-500/30";
    }
  };

  const getStatusBadgeStyle = (status: string) => {
    switch (status) {
      case "Upcoming":
        return "bg-green-500/20 text-green-400 border-green-500/30";
      case "Scheduled":
        return "bg-blue-500/20 text-blue-400 border-blue-500/30";
      case "Awaiting Feedback":
        return "bg-amber-500/20 text-amber-400 border-amber-500/30";
      case "Completed":
        return "bg-gray-500/20 text-gray-400 border-gray-500/30";
      default:
        return "bg-gray-500/20 text-gray-400 border-gray-500/30";
    }
  };

  return (
    <DashboardLayout role="hiring-manager">
      <div className="mb-8">
        <h1 className="text-3xl font-bold text-white mb-2">Interview Schedule</h1>
        <p className="text-gray-400">Manage and track your interview pipeline</p>
      </div>

      {/* Stats */}
      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6 mb-8">
        <StatCard
          title="Pending Reviews"
          value="6"
          icon={AlertCircle}
          trend={{ value: 8, isPositive: false }}
        />
        <StatCard
          title="Scheduled This Week"
          value="4"
          icon={Calendar}
          trend={{ value: 15, isPositive: true }}
        />
        <StatCard
          title="Awaiting Feedback"
          value="9"
          icon={MessageSquare}
          trend={{ value: 12, isPositive: false }}
        />
        <StatCard
          title="Completed This Month"
          value="28"
          icon={CheckCircle}
          trend={{ value: 22, isPositive: true }}
        />
      </div>

      {/* Week Calendar Strip */}
      <GlassCard className="p-6 mb-8">
        <div className="flex items-center justify-between mb-4">
          <h2 className="text-xl font-semibold text-white">Week of June 16–22, 2026</h2>
          <div className="flex gap-2">
            <Button variant="outline" size="sm" className="border-[#D4AF37]/30 text-gray-300 hover:bg-[#D4AF37]/10">
              <ChevronLeft className="h-4 w-4" />
            </Button>
            <Button variant="outline" size="sm" className="border-[#D4AF37]/30 text-gray-300 hover:bg-[#D4AF37]/10">
              <ChevronRight className="h-4 w-4" />
            </Button>
          </div>
        </div>
        <div className="grid grid-cols-7 gap-2">
          {weekDays.map((day, index) => (
            <button
              key={index}
              onClick={() => setSelectedDay(index)}
              className={`flex flex-col items-center p-3 rounded-lg transition-all ${
                selectedDay === index
                  ? "bg-[#D4AF37]/20 border border-[#D4AF37]/60"
                  : "bg-[#2D2D2D]/50 border border-transparent hover:border-[#D4AF37]/20"
              }`}
            >
              <span className="text-xs text-gray-400 mb-1">{day.label}</span>
              <span className={`text-lg font-semibold mb-2 ${selectedDay === index ? "text-[#D4AF37]" : "text-white"}`}>
                {day.date}
              </span>
              {day.count > 0 ? (
                <span className="text-xs bg-[#D4AF37]/20 text-[#D4AF37] px-2 py-0.5 rounded-full font-medium">
                  {day.count}
                </span>
              ) : (
                <span className="text-xs text-gray-600">—</span>
              )}
            </button>
          ))}
        </div>
      </GlassCard>

      {/* Interview List */}
      <GlassCard className="p-6">
        <div className="flex items-center justify-between mb-6">
          <h2 className="text-xl font-semibold text-white">All Interviews</h2>
          <div className="flex gap-2">
            <Button variant="outline" size="sm" className="border-[#D4AF37]/30 text-[#D4AF37] hover:bg-[#D4AF37]/10">
              Filter
            </Button>
            <Button size="sm" className="bg-[#D4AF37] text-black hover:bg-[#D4AF37]/90 font-semibold">
              <Calendar className="mr-2 h-4 w-4" />
              Schedule Interview
            </Button>
          </div>
        </div>
        <div className="space-y-4">
          {interviews.map((interview) => (
            <div
              key={interview.id}
              className="p-5 rounded-lg bg-[#2D2D2D]/50 border border-[#D4AF37]/10 hover:border-[#D4AF37]/30 transition-all"
            >
              <div className="flex items-start justify-between gap-4">
                <div className="flex items-start gap-4 flex-1">
                  {/* Avatar */}
                  <div className="w-11 h-11 rounded-full bg-[#D4AF37]/20 border border-[#D4AF37]/40 flex items-center justify-center flex-shrink-0">
                    <span className="text-sm font-bold text-[#D4AF37]">{interview.avatarInitials}</span>
                  </div>

                  {/* Details */}
                  <div className="flex-1 min-w-0">
                    <div className="flex items-center flex-wrap gap-2 mb-1">
                      <h3 className="font-semibold text-white">{interview.candidate}</h3>
                      <Badge className={getRoleBadgeStyle(interview.myRole)}>{interview.myRole}</Badge>
                      <Badge className={getStatusBadgeStyle(interview.status)}>{interview.status}</Badge>
                    </div>
                    <p className="text-[#D4AF37] text-sm font-medium mb-2">{interview.position}</p>
                    <div className="flex flex-wrap items-center gap-4 text-sm text-gray-400">
                      <span className="flex items-center gap-1.5">
                        <Calendar className="h-3.5 w-3.5" />
                        {interview.date}
                      </span>
                      <span className="flex items-center gap-1.5">
                        <Clock className="h-3.5 w-3.5" />
                        {interview.time}
                      </span>
                      <span className="flex items-center gap-1.5">
                        <Users className="h-3.5 w-3.5" />
                        {interview.panel.join(", ")}
                      </span>
                    </div>
                  </div>
                </div>

                {/* Actions */}
                <div className="flex items-center gap-2 flex-shrink-0">
                  <Button
                    variant="outline"
                    size="sm"
                    className="border-[#D4AF37]/30 text-gray-300 hover:bg-[#D4AF37]/10 hover:text-[#D4AF37]"
                  >
                    <FileText className="mr-1.5 h-3.5 w-3.5" />
                    View Feedback
                  </Button>
                  <Button
                    variant="outline"
                    size="sm"
                    className="border-[#D4AF37]/30 text-gray-300 hover:bg-[#D4AF37]/10 hover:text-[#D4AF37]"
                  >
                    <MessageSquare className="mr-1.5 h-3.5 w-3.5" />
                    Add Notes
                  </Button>
                  <Button
                    size="sm"
                    className="bg-[#D4AF37] text-black hover:bg-[#D4AF37]/90 font-semibold"
                    onClick={() => navigate("/interviews/video-room")}
                  >
                    <Video className="mr-1.5 h-3.5 w-3.5" />
                    Join Video
                  </Button>
                </div>
              </div>
            </div>
          ))}
        </div>

        {/* Pagination */}
        <div className="flex items-center justify-between mt-6 pt-4 border-t border-[#D4AF37]/10">
          <p className="text-sm text-gray-400">Showing 7 of 28 interviews</p>
          <div className="flex gap-2">
            <Button variant="outline" size="sm" className="border-[#D4AF37]/30 text-gray-300">
              Previous
            </Button>
            <Button variant="outline" size="sm" className="border-[#D4AF37]/30 text-[#D4AF37] bg-[#D4AF37]/10">
              1
            </Button>
            <Button variant="outline" size="sm" className="border-[#D4AF37]/30 text-gray-300">
              2
            </Button>
            <Button variant="outline" size="sm" className="border-[#D4AF37]/30 text-gray-300">
              3
            </Button>
            <Button variant="outline" size="sm" className="border-[#D4AF37]/30 text-gray-300">
              Next
            </Button>
          </div>
        </div>
      </GlassCard>
    </DashboardLayout>
  );
}
