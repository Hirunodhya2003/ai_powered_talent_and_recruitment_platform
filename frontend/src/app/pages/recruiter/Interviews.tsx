import { useState } from "react";
import { useNavigate } from "react-router";
import { DashboardLayout } from "../../components/DashboardLayout";
import { GlassCard } from "../../components/GlassCard";
import { Badge } from "../../components/ui/badge";
import { Button } from "../../components/ui/button";
import {
  Calendar,
  Video,
  Phone,
  Users,
  User,
  Clock,
  CheckCircle,
  XCircle,
  RefreshCw,
  TrendingUp,
  Briefcase,
} from "lucide-react";

type InterviewType = "Phone" | "Technical" | "HR" | "Panel";
type InterviewStatus = "Confirmed" | "Pending" | "Reschedule Requested";

interface Interview {
  id: number;
  candidateName: string;
  candidateTitle: string;
  position: string;
  date: string;
  time: string;
  type: InterviewType;
  interviewer: string;
  status: InterviewStatus;
  videoLink: boolean;
}

const interviews: Interview[] = [
  {
    id: 1,
    candidateName: "Priya Sharma",
    candidateTitle: "Senior Backend Engineer",
    position: "Principal Software Engineer",
    date: "June 18, 2026",
    time: "10:00 AM PST",
    type: "Technical",
    interviewer: "David Kim",
    status: "Confirmed",
    videoLink: true,
  },
  {
    id: 2,
    candidateName: "Marcus Johnson",
    candidateTitle: "Product Manager",
    position: "Director of Product",
    date: "June 18, 2026",
    time: "2:00 PM PST",
    type: "Panel",
    interviewer: "Sarah Chen, Tom Rivera",
    status: "Reschedule Requested",
    videoLink: true,
  },
  {
    id: 3,
    candidateName: "Elena Vasquez",
    candidateTitle: "UX Designer",
    position: "Senior Product Designer",
    date: "June 19, 2026",
    time: "11:00 AM PST",
    type: "HR",
    interviewer: "Jordan Williams",
    status: "Confirmed",
    videoLink: false,
  },
  {
    id: 4,
    candidateName: "James O'Brien",
    candidateTitle: "DevOps Engineer",
    position: "Staff Infrastructure Engineer",
    date: "June 19, 2026",
    time: "3:30 PM PST",
    type: "Technical",
    interviewer: "Mike Zhang",
    status: "Pending",
    videoLink: true,
  },
  {
    id: 5,
    candidateName: "Aisha Patel",
    candidateTitle: "Data Scientist",
    position: "Senior Data Scientist",
    date: "June 20, 2026",
    time: "9:00 AM PST",
    type: "Phone",
    interviewer: "Jordan Williams",
    status: "Confirmed",
    videoLink: false,
  },
  {
    id: 6,
    candidateName: "Ryan Nakamura",
    candidateTitle: "Full Stack Developer",
    position: "Senior Frontend Engineer",
    date: "June 22, 2026",
    time: "1:00 PM PST",
    type: "Technical",
    interviewer: "David Kim",
    status: "Confirmed",
    videoLink: true,
  },
  {
    id: 7,
    candidateName: "Sophie Laurent",
    candidateTitle: "Engineering Manager",
    position: "Director of Engineering",
    date: "June 23, 2026",
    time: "10:30 AM PST",
    type: "Panel",
    interviewer: "CEO, CTO, VP Engineering",
    status: "Pending",
    videoLink: true,
  },
  {
    id: 8,
    candidateName: "Carlos Mendez",
    candidateTitle: "Machine Learning Engineer",
    position: "Staff ML Engineer",
    date: "June 24, 2026",
    time: "2:30 PM PST",
    type: "Technical",
    interviewer: "Lisa Wang",
    status: "Confirmed",
    videoLink: true,
  },
];

const typeConfig: Record<InterviewType, { icon: React.ReactNode; color: string }> = {
  Phone: { icon: <Phone className="w-3.5 h-3.5" />, color: "bg-green-500/10 text-green-400 border border-green-500/20" },
  Technical: { icon: <Briefcase className="w-3.5 h-3.5" />, color: "bg-blue-500/10 text-blue-400 border border-blue-500/20" },
  HR: { icon: <User className="w-3.5 h-3.5" />, color: "bg-purple-500/10 text-purple-400 border border-purple-500/20" },
  Panel: { icon: <Users className="w-3.5 h-3.5" />, color: "bg-orange-500/10 text-orange-400 border border-orange-500/20" },
};

const statusConfig: Record<InterviewStatus, { color: string; icon: React.ReactNode }> = {
  Confirmed: { color: "bg-green-500/10 text-green-400 border border-green-500/20", icon: <CheckCircle className="w-3.5 h-3.5" /> },
  Pending: { color: "bg-yellow-500/10 text-yellow-400 border border-yellow-500/20", icon: <Clock className="w-3.5 h-3.5" /> },
  "Reschedule Requested": { color: "bg-orange-500/10 text-orange-400 border border-orange-500/20", icon: <RefreshCw className="w-3.5 h-3.5" /> },
};

const stats = [
  { label: "Total Scheduled", value: "24", icon: <Calendar className="w-5 h-5" />, color: "text-[#D4AF37]" },
  { label: "This Week", value: "8", icon: <Clock className="w-5 h-5" />, color: "text-blue-400" },
  { label: "Completed", value: "156", icon: <CheckCircle className="w-5 h-5" />, color: "text-green-400" },
  { label: "Cancelled", value: "12", icon: <XCircle className="w-5 h-5" />, color: "text-red-400" },
];

export function RecruiterInterviews() {
  const navigate = useNavigate();
  const [cancelled, setCancelled] = useState<number[]>([]);

  const visible = interviews.filter((i) => !cancelled.includes(i.id));

  return (
    <DashboardLayout role="recruiter">
      <div className="p-6 space-y-6">
        {/* Header */}
        <div className="flex items-center justify-between">
          <div>
            <h1 className="text-2xl font-bold text-white">Interview Management</h1>
            <p className="text-gray-400 mt-1">Upcoming interviews for the next 7 days</p>
          </div>
          <Button className="bg-[#D4AF37] text-black font-semibold hover:bg-[#D4AF37]/90">
            <Calendar className="w-4 h-4 mr-2" />
            Schedule Interview
          </Button>
        </div>

        {/* Stats */}
        <div className="grid grid-cols-4 gap-4">
          {stats.map((stat) => (
            <GlassCard key={stat.label} className="p-6">
              <div className="flex items-center justify-between mb-3">
                <span className={stat.color}>{stat.icon}</span>
                <TrendingUp className="w-4 h-4 text-gray-600" />
              </div>
              <p className={`text-3xl font-bold ${stat.color}`}>{stat.value}</p>
              <p className="text-gray-400 text-sm mt-1">{stat.label}</p>
            </GlassCard>
          ))}
        </div>

        {/* Interview List */}
        <div className="space-y-4">
          <h2 className="text-lg font-semibold text-white flex items-center gap-2">
            <Calendar className="w-5 h-5 text-[#D4AF37]" />
            Upcoming Interviews
          </h2>
          {visible.map((interview) => (
            <GlassCard key={interview.id} className="p-6">
              <div className="flex items-start justify-between gap-4">
                <div className="flex items-start gap-4 flex-1">
                  {/* Avatar */}
                  <div className="w-12 h-12 rounded-xl bg-[#D4AF37]/10 border border-[#D4AF37]/20 flex items-center justify-center text-[#D4AF37] font-bold text-lg shrink-0">
                    {interview.candidateName.charAt(0)}
                  </div>

                  {/* Info */}
                  <div className="flex-1">
                    <div className="flex items-center gap-2 flex-wrap">
                      <h3 className="text-white font-semibold">{interview.candidateName}</h3>
                      <span className="text-gray-500 text-sm">·</span>
                      <span className="text-gray-400 text-sm">{interview.candidateTitle}</span>
                    </div>
                    <p className="text-[#D4AF37] text-sm mt-0.5 font-medium">{interview.position}</p>
                    <div className="flex items-center gap-4 mt-2 flex-wrap">
                      <span className="flex items-center gap-1.5 text-gray-300 text-sm">
                        <Calendar className="w-3.5 h-3.5 text-gray-500" />
                        {interview.date}
                      </span>
                      <span className="flex items-center gap-1.5 text-gray-300 text-sm">
                        <Clock className="w-3.5 h-3.5 text-gray-500" />
                        {interview.time}
                      </span>
                      <span className="flex items-center gap-1.5 text-gray-400 text-sm">
                        <User className="w-3.5 h-3.5 text-gray-500" />
                        {interview.interviewer}
                      </span>
                    </div>
                    <div className="flex items-center gap-2 mt-2">
                      <Badge className={`text-xs flex items-center gap-1 ${typeConfig[interview.type].color}`}>
                        {typeConfig[interview.type].icon}
                        {interview.type}
                      </Badge>
                      <Badge className={`text-xs flex items-center gap-1 ${statusConfig[interview.status].color}`}>
                        {statusConfig[interview.status].icon}
                        {interview.status}
                      </Badge>
                    </div>
                  </div>
                </div>

                {/* Actions */}
                <div className="flex flex-col gap-2 shrink-0">
                  {interview.videoLink && (
                    <Button
                      onClick={() => navigate("/interviews/video-room")}
                      className="bg-[#D4AF37] text-black font-semibold hover:bg-[#D4AF37]/90 text-sm"
                    >
                      <Video className="w-4 h-4 mr-2" />
                      Join Video Call
                    </Button>
                  )}
                  <Button
                    variant="outline"
                    className="border-[#D4AF37]/20 text-gray-300 hover:border-[#D4AF37]/40 text-sm"
                  >
                    <RefreshCw className="w-4 h-4 mr-2" />
                    Reschedule
                  </Button>
                  <Button
                    variant="outline"
                    onClick={() => setCancelled((c) => [...c, interview.id])}
                    className="border-red-500/20 text-red-400 hover:bg-red-500/10 hover:border-red-500/40 text-sm"
                  >
                    <XCircle className="w-4 h-4 mr-2" />
                    Cancel
                  </Button>
                </div>
              </div>
            </GlassCard>
          ))}

          {visible.length === 0 && (
            <GlassCard className="p-6">
              <p className="text-center text-gray-400 py-8">
                No upcoming interviews scheduled.
              </p>
            </GlassCard>
          )}
        </div>
      </div>
    </DashboardLayout>
  );
}
