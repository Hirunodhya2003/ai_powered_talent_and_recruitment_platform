import { useState } from "react";
import { useNavigate } from "react-router";
import { ModuleLayout } from "../../components/ModuleLayout";
import { GlassCard } from "../../components/GlassCard";
import { Badge } from "../../components/ui/badge";
import { Button } from "../../components/ui/button";
import {
  Calendar,
  CalendarDays,
  GitBranch,
  Video,
  Clock,
  Users,
  Plus,
  MapPin,
  Check,
  X,
} from "lucide-react";

const interviewTabs = [
  { label: "Schedule", path: "/interviews/schedule", icon: Calendar },
  { label: "Calendar", path: "/interviews/calendar", icon: CalendarDays },
  { label: "Timeline", path: "/interviews/timeline", icon: GitBranch },
  { label: "Video Room", path: "/interviews/video-room", icon: Video },
];

const upcomingInterviews = [
  {
    id: 1,
    candidate: "Marcus Johnson",
    position: "Senior Frontend Engineer",
    date: "June 17, 2026",
    time: "10:00 AM",
    type: "Technical",
    interviewers: ["Sarah Chen", "David Park"],
    status: "Confirmed",
  },
  {
    id: 2,
    candidate: "Priya Sharma",
    position: "Product Manager",
    date: "June 17, 2026",
    time: "2:00 PM",
    type: "HR",
    interviewers: ["Rachel Adams"],
    status: "Confirmed",
  },
  {
    id: 3,
    candidate: "James O'Brien",
    position: "Backend Engineer",
    date: "June 17, 2026",
    time: "4:30 PM",
    type: "Phone",
    interviewers: ["Tom Wilson"],
    status: "Pending",
  },
  {
    id: 4,
    candidate: "Aisha Patel",
    position: "Data Scientist",
    date: "June 18, 2026",
    time: "9:00 AM",
    type: "Panel",
    interviewers: ["Sarah Chen", "Mike Torres", "Lisa Wong"],
    status: "Confirmed",
  },
  {
    id: 5,
    candidate: "Carlos Rivera",
    position: "DevOps Lead",
    date: "June 18, 2026",
    time: "11:00 AM",
    type: "Final",
    interviewers: ["David Park", "CEO James Hart"],
    status: "Confirmed",
  },
  {
    id: 6,
    candidate: "Emma Thompson",
    position: "UX Designer",
    date: "June 19, 2026",
    time: "10:30 AM",
    type: "Technical",
    interviewers: ["Rachel Adams", "Nina Patel"],
    status: "Pending",
  },
  {
    id: 7,
    candidate: "Kwame Asante",
    position: "Cloud Architect",
    date: "June 20, 2026",
    time: "1:00 PM",
    type: "Phone",
    interviewers: ["Tom Wilson"],
    status: "Pending",
  },
  {
    id: 8,
    candidate: "Sofia Lindqvist",
    position: "Full Stack Engineer",
    date: "June 21, 2026",
    time: "3:00 PM",
    type: "HR",
    interviewers: ["Rachel Adams"],
    status: "Confirmed",
  },
];

const typeColors: Record<string, string> = {
  Phone: "bg-blue-500/20 text-blue-300 border-blue-500/30",
  Technical: "bg-purple-500/20 text-purple-300 border-purple-500/30",
  HR: "bg-green-500/20 text-green-300 border-green-500/30",
  Panel: "bg-orange-500/20 text-orange-300 border-orange-500/30",
  Final: "bg-[#D4AF37]/20 text-[#D4AF37] border-[#D4AF37]/30",
};

const statusColors: Record<string, string> = {
  Confirmed: "bg-green-500/20 text-green-300 border-green-500/30",
  Pending: "bg-yellow-500/20 text-yellow-300 border-yellow-500/30",
};

export function InterviewScheduleModule() {
  const navigate = useNavigate();
  const [showForm, setShowForm] = useState(false);
  const [formData, setFormData] = useState({
    candidateName: "",
    position: "",
    type: "Phone",
    date: "",
    time: "",
    duration: "60",
    interviewers: "",
    notes: "",
    sendInvite: true,
  });

  const stats = [
    { label: "Scheduled Today", value: "3", color: "text-[#D4AF37]" },
    { label: "This Week", value: "12", color: "text-blue-400" },
    { label: "Pending Confirmation", value: "4", color: "text-yellow-400" },
    { label: "Completed This Month", value: "67", color: "text-green-400" },
  ];

  return (
    <ModuleLayout
      title="Interview Scheduling"
      subtitle="Schedule and manage all interview appointments"
      icon={Calendar}
      tabs={interviewTabs}
      backPath="/recruiter/dashboard"
      backLabel="Back to Portal"
    >
      {/* Stats */}
      <div className="grid grid-cols-2 md:grid-cols-4 gap-4 mb-6">
        {stats.map((s) => (
          <GlassCard key={s.label} className="p-4 text-center">
            <div className={`text-3xl font-bold ${s.color}`}>{s.value}</div>
            <div className="text-gray-400 text-xs mt-1">{s.label}</div>
          </GlassCard>
        ))}
      </div>

      {/* Schedule button */}
      <div className="flex justify-between items-center mb-4">
        <h2 className="text-white font-semibold text-lg">Upcoming Interviews</h2>
        <Button
          onClick={() => setShowForm(!showForm)}
          className="bg-[#D4AF37] text-black hover:bg-[#D4AF37]/80 gap-2"
        >
          <Plus className="h-4 w-4" />
          Schedule New Interview
        </Button>
      </div>

      {/* Inline Form Panel */}
      {showForm && (
        <GlassCard className="p-6 mb-6 border-[#D4AF37]/30">
          <div className="flex justify-between items-center mb-4">
            <h3 className="text-white font-semibold text-base">New Interview</h3>
            <button onClick={() => setShowForm(false)} className="text-gray-400 hover:text-white">
              <X className="h-5 w-5" />
            </button>
          </div>
          <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
            <div>
              <label className="text-gray-400 text-xs mb-1 block">Candidate Name</label>
              <input
                type="text"
                placeholder="Enter candidate name"
                value={formData.candidateName}
                onChange={(e) => setFormData({ ...formData, candidateName: e.target.value })}
                className="w-full bg-[#2D2D2D] border border-[#D4AF37]/20 rounded-lg px-3 py-2 text-white text-sm placeholder-gray-500 focus:outline-none focus:border-[#D4AF37]/50"
              />
            </div>
            <div>
              <label className="text-gray-400 text-xs mb-1 block">Position</label>
              <input
                type="text"
                placeholder="Job title / position"
                value={formData.position}
                onChange={(e) => setFormData({ ...formData, position: e.target.value })}
                className="w-full bg-[#2D2D2D] border border-[#D4AF37]/20 rounded-lg px-3 py-2 text-white text-sm placeholder-gray-500 focus:outline-none focus:border-[#D4AF37]/50"
              />
            </div>
            <div>
              <label className="text-gray-400 text-xs mb-1 block">Interview Type</label>
              <select
                value={formData.type}
                onChange={(e) => setFormData({ ...formData, type: e.target.value })}
                className="w-full bg-[#2D2D2D] border border-[#D4AF37]/20 rounded-lg px-3 py-2 text-white text-sm focus:outline-none focus:border-[#D4AF37]/50"
              >
                {["Phone", "Technical", "HR", "Panel", "Final"].map((t) => (
                  <option key={t} value={t}>{t}</option>
                ))}
              </select>
            </div>
            <div>
              <label className="text-gray-400 text-xs mb-1 block">Duration</label>
              <select
                value={formData.duration}
                onChange={(e) => setFormData({ ...formData, duration: e.target.value })}
                className="w-full bg-[#2D2D2D] border border-[#D4AF37]/20 rounded-lg px-3 py-2 text-white text-sm focus:outline-none focus:border-[#D4AF37]/50"
              >
                {["30", "45", "60", "90"].map((d) => (
                  <option key={d} value={d}>{d} min</option>
                ))}
              </select>
            </div>
            <div>
              <label className="text-gray-400 text-xs mb-1 block">Date</label>
              <input
                type="date"
                value={formData.date}
                onChange={(e) => setFormData({ ...formData, date: e.target.value })}
                className="w-full bg-[#2D2D2D] border border-[#D4AF37]/20 rounded-lg px-3 py-2 text-white text-sm focus:outline-none focus:border-[#D4AF37]/50"
              />
            </div>
            <div>
              <label className="text-gray-400 text-xs mb-1 block">Time</label>
              <input
                type="time"
                value={formData.time}
                onChange={(e) => setFormData({ ...formData, time: e.target.value })}
                className="w-full bg-[#2D2D2D] border border-[#D4AF37]/20 rounded-lg px-3 py-2 text-white text-sm focus:outline-none focus:border-[#D4AF37]/50"
              />
            </div>
            <div className="md:col-span-2">
              <label className="text-gray-400 text-xs mb-1 block">Interviewers</label>
              <input
                type="text"
                placeholder="e.g. Sarah Chen, David Park"
                value={formData.interviewers}
                onChange={(e) => setFormData({ ...formData, interviewers: e.target.value })}
                className="w-full bg-[#2D2D2D] border border-[#D4AF37]/20 rounded-lg px-3 py-2 text-white text-sm placeholder-gray-500 focus:outline-none focus:border-[#D4AF37]/50"
              />
            </div>
            <div className="md:col-span-2">
              <label className="text-gray-400 text-xs mb-1 block">Notes</label>
              <textarea
                placeholder="Additional notes or instructions..."
                value={formData.notes}
                onChange={(e) => setFormData({ ...formData, notes: e.target.value })}
                rows={3}
                className="w-full bg-[#2D2D2D] border border-[#D4AF37]/20 rounded-lg px-3 py-2 text-white text-sm placeholder-gray-500 focus:outline-none focus:border-[#D4AF37]/50 resize-none"
              />
            </div>
            <div className="md:col-span-2 flex items-center gap-3">
              <button
                onClick={() => setFormData({ ...formData, sendInvite: !formData.sendInvite })}
                className={`w-5 h-5 rounded border flex items-center justify-center transition-colors ${
                  formData.sendInvite
                    ? "bg-[#D4AF37] border-[#D4AF37]"
                    : "bg-transparent border-[#D4AF37]/30"
                }`}
              >
                {formData.sendInvite && <Check className="h-3 w-3 text-black" />}
              </button>
              <span className="text-gray-300 text-sm">Send Calendar Invite to participants</span>
            </div>
          </div>
          <div className="flex gap-3 mt-5">
            <Button className="bg-[#D4AF37] text-black hover:bg-[#D4AF37]/80">
              Schedule Interview
            </Button>
            <Button
              variant="ghost"
              className="text-gray-400 hover:text-white"
              onClick={() => setShowForm(false)}
            >
              Cancel
            </Button>
          </div>
        </GlassCard>
      )}

      {/* Interview List */}
      <div className="space-y-3">
        {upcomingInterviews.map((interview) => (
          <GlassCard key={interview.id} className="p-4 hover:border-[#D4AF37]/40 transition-colors">
            <div className="flex flex-col md:flex-row md:items-center justify-between gap-4">
              <div className="flex items-start gap-4">
                <div className="w-10 h-10 rounded-full bg-[#D4AF37]/20 border border-[#D4AF37]/30 flex items-center justify-center text-[#D4AF37] font-semibold text-sm flex-shrink-0">
                  {interview.candidate.split(" ").map((n) => n[0]).join("")}
                </div>
                <div>
                  <div className="text-white font-medium">{interview.candidate}</div>
                  <div className="text-gray-400 text-sm">{interview.position}</div>
                  <div className="flex items-center gap-3 mt-1.5 flex-wrap">
                    <span className="flex items-center gap-1 text-gray-400 text-xs">
                      <Clock className="h-3 w-3" />
                      {interview.date} · {interview.time}
                    </span>
                    <span className="flex items-center gap-1 text-gray-400 text-xs">
                      <Users className="h-3 w-3" />
                      {interview.interviewers.join(", ")}
                    </span>
                  </div>
                </div>
              </div>
              <div className="flex items-center gap-3 flex-wrap">
                <span className={`text-xs px-2.5 py-1 rounded-full border font-medium ${typeColors[interview.type]}`}>
                  {interview.type}
                </span>
                <span className={`text-xs px-2.5 py-1 rounded-full border font-medium ${statusColors[interview.status]}`}>
                  {interview.status}
                </span>
                <div className="flex gap-2">
                  <Button
                    size="sm"
                    onClick={() => navigate("/interviews/video-room")}
                    className="bg-[#D4AF37]/10 text-[#D4AF37] hover:bg-[#D4AF37]/20 border border-[#D4AF37]/20 h-8 text-xs gap-1"
                  >
                    <Video className="h-3 w-3" />
                    Join
                  </Button>
                  <Button
                    size="sm"
                    variant="ghost"
                    className="text-gray-400 hover:text-white h-8 text-xs"
                  >
                    Edit
                  </Button>
                  <Button
                    size="sm"
                    variant="ghost"
                    className="text-red-400 hover:text-red-300 h-8 text-xs"
                  >
                    Cancel
                  </Button>
                </div>
              </div>
            </div>
          </GlassCard>
        ))}
      </div>
    </ModuleLayout>
  );
}
