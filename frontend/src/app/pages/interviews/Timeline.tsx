import { useState } from "react";
import { ModuleLayout } from "../../components/ModuleLayout";
import { GlassCard } from "../../components/GlassCard";
import { Button } from "../../components/ui/button";
import {
  Calendar,
  CalendarDays,
  GitBranch,
  Video,
  Users,
  Clock,
  AlertTriangle,
  CheckCircle,
  Circle,
} from "lucide-react";

const interviewTabs = [
  { label: "Schedule", path: "/interviews/schedule", icon: Calendar },
  { label: "Calendar", path: "/interviews/calendar", icon: CalendarDays },
  { label: "Timeline", path: "/interviews/timeline", icon: GitBranch },
  { label: "Video Room", path: "/interviews/video-room", icon: Video },
];

const stages = ["Phone Screen", "Technical", "HR Round", "Panel", "Final"];

const stageStats = [
  { label: "Phone Screen", count: 24, color: "text-blue-400", bg: "bg-blue-400/20" },
  { label: "Technical", count: 18, color: "text-purple-400", bg: "bg-purple-400/20" },
  { label: "HR Round", count: 12, color: "text-green-400", bg: "bg-green-400/20" },
  { label: "Panel", count: 7, color: "text-orange-400", bg: "bg-orange-400/20" },
  { label: "Final", count: 4, color: "text-[#D4AF37]", bg: "bg-[#D4AF37]/20" },
  { label: "Offered", count: 3, color: "text-emerald-400", bg: "bg-emerald-400/20" },
];

const candidates = [
  {
    id: 1,
    name: "Marcus Johnson",
    position: "Senior Frontend Engineer",
    currentStage: 2,
    daysInStage: 5,
    lastActivity: "June 15, 2026",
    nextAction: "Schedule Technical Round 2",
  },
  {
    id: 2,
    name: "Priya Sharma",
    position: "Product Manager",
    currentStage: 3,
    daysInStage: 3,
    lastActivity: "June 14, 2026",
    nextAction: "Await HR Feedback",
  },
  {
    id: 3,
    name: "Carlos Rivera",
    position: "DevOps Lead",
    currentStage: 4,
    daysInStage: 2,
    lastActivity: "June 16, 2026",
    nextAction: "Schedule Final Interview",
  },
  {
    id: 4,
    name: "Aisha Patel",
    position: "Data Scientist",
    currentStage: 1,
    daysInStage: 8,
    lastActivity: "June 9, 2026",
    nextAction: "Follow Up — Awaiting Availability",
  },
  {
    id: 5,
    name: "Sofia Lindqvist",
    position: "Full Stack Engineer",
    currentStage: 2,
    daysInStage: 11,
    lastActivity: "June 6, 2026",
    nextAction: "Reschedule Technical Interview",
  },
  {
    id: 6,
    name: "Kwame Asante",
    position: "Cloud Architect",
    currentStage: 0,
    daysInStage: 1,
    lastActivity: "June 16, 2026",
    nextAction: "Confirm Phone Screen",
  },
];

const stageColors = [
  "border-blue-400 bg-blue-400",
  "border-purple-400 bg-purple-400",
  "border-green-400 bg-green-400",
  "border-orange-400 bg-orange-400",
  "border-[#D4AF37] bg-[#D4AF37]",
];

const stageTextColors = [
  "text-blue-400",
  "text-purple-400",
  "text-green-400",
  "text-orange-400",
  "text-[#D4AF37]",
];

export function InterviewTimeline() {
  const [activeFilter, setActiveFilter] = useState("All");
  const filters = ["All", ...stages];

  const filteredCandidates = activeFilter === "All"
    ? candidates
    : candidates.filter((c) => stages[c.currentStage] === activeFilter);

  const bottleneckStage = stageStats.find((s) => s.count > 10 && s.label !== "Phone Screen");

  return (
    <ModuleLayout
      title="Interview Timeline"
      subtitle="Track candidate progress through the interview pipeline"
      icon={GitBranch}
      tabs={interviewTabs}
      backPath="/recruiter/dashboard"
      backLabel="Back to Portal"
    >
      {/* Stage stats */}
      <div className="grid grid-cols-3 md:grid-cols-6 gap-3 mb-6">
        {stageStats.map((s) => (
          <GlassCard key={s.label} className="p-3 text-center">
            <div className={`text-2xl font-bold ${s.color}`}>{s.count}</div>
            <div className={`text-[10px] mt-1 px-2 py-0.5 rounded-full ${s.bg} ${s.color} font-medium`}>
              {s.label}
            </div>
          </GlassCard>
        ))}
      </div>

      {/* Bottleneck Alert */}
      {bottleneckStage && (
        <div className="flex items-start gap-3 bg-yellow-500/10 border border-yellow-500/30 rounded-lg p-4 mb-6">
          <AlertTriangle className="h-5 w-5 text-yellow-400 flex-shrink-0 mt-0.5" />
          <div>
            <div className="text-yellow-400 font-semibold text-sm">Bottleneck Alert</div>
            <div className="text-yellow-300/80 text-xs mt-0.5">
              {bottleneckStage.label} round is a bottleneck ({bottleneckStage.count} candidates, avg 8 days). Consider adding more interviewers or scheduling batch sessions.
            </div>
          </div>
        </div>
      )}

      {/* Filter tabs */}
      <div className="flex gap-2 flex-wrap mb-5">
        {filters.map((f) => (
          <button
            key={f}
            onClick={() => setActiveFilter(f)}
            className={`px-4 py-1.5 rounded-full text-xs font-medium border transition-all ${
              activeFilter === f
                ? "bg-[#D4AF37] text-black border-[#D4AF37]"
                : "text-gray-400 border-[#D4AF37]/20 hover:text-white hover:border-[#D4AF37]/40"
            }`}
          >
            {f}
          </button>
        ))}
      </div>

      {/* Candidate Timeline Cards */}
      <div className="space-y-4">
        {filteredCandidates.map((candidate) => (
          <GlassCard key={candidate.id} className="p-5 hover:border-[#D4AF37]/40 transition-colors">
            <div className="flex flex-col md:flex-row md:items-start gap-4">
              {/* Avatar & info */}
              <div className="flex items-start gap-3 md:w-56 flex-shrink-0">
                <div className="w-10 h-10 rounded-full bg-[#D4AF37]/20 border border-[#D4AF37]/30 flex items-center justify-center text-[#D4AF37] font-semibold text-sm flex-shrink-0">
                  {candidate.name.split(" ").map((n) => n[0]).join("")}
                </div>
                <div>
                  <div className="text-white font-medium text-sm">{candidate.name}</div>
                  <div className="text-gray-400 text-xs mt-0.5">{candidate.position}</div>
                  <div className="flex items-center gap-1 mt-1.5 text-gray-500 text-xs">
                    <Clock className="h-3 w-3" />
                    {candidate.daysInStage}d in stage
                  </div>
                </div>
              </div>

              {/* Pipeline stages */}
              <div className="flex-1">
                <div className="flex items-center">
                  {stages.map((stage, idx) => {
                    const isCompleted = idx < candidate.currentStage;
                    const isCurrent = idx === candidate.currentStage;
                    const isUpcoming = idx > candidate.currentStage;
                    return (
                      <div key={stage} className="flex items-center flex-1">
                        <div className="flex flex-col items-center">
                          <div
                            className={`w-8 h-8 rounded-full border-2 flex items-center justify-center transition-all ${
                              isCompleted
                                ? "border-[#D4AF37] bg-[#D4AF37]"
                                : isCurrent
                                ? `${stageColors[idx]} ring-2 ring-offset-2 ring-offset-[#1E1E1E]`
                                : "border-gray-600 bg-transparent"
                            }`}
                          >
                            {isCompleted ? (
                              <CheckCircle className="h-4 w-4 text-black" />
                            ) : isCurrent ? (
                              <Circle className={`h-3 w-3 ${isCurrent ? "text-black" : "text-gray-500"} fill-current`} />
                            ) : (
                              <Circle className="h-3 w-3 text-gray-600" />
                            )}
                          </div>
                          <span
                            className={`text-[9px] mt-1 font-medium text-center whitespace-nowrap ${
                              isCompleted
                                ? "text-[#D4AF37]"
                                : isCurrent
                                ? stageTextColors[idx]
                                : "text-gray-600"
                            }`}
                          >
                            {stage.split(" ")[0]}
                          </span>
                        </div>
                        {idx < stages.length - 1 && (
                          <div
                            className={`flex-1 h-0.5 mx-1 rounded mb-3.5 ${
                              idx < candidate.currentStage ? "bg-[#D4AF37]" : "bg-gray-700"
                            }`}
                          />
                        )}
                      </div>
                    );
                  })}
                </div>
              </div>

              {/* Right side: activity & action */}
              <div className="md:w-48 flex-shrink-0 flex flex-col gap-2">
                <div className="text-gray-500 text-xs">
                  Last activity: <span className="text-gray-300">{candidate.lastActivity}</span>
                </div>
                <Button
                  size="sm"
                  className="bg-[#D4AF37]/10 text-[#D4AF37] hover:bg-[#D4AF37]/20 border border-[#D4AF37]/20 h-8 text-xs"
                >
                  {candidate.nextAction}
                </Button>
              </div>
            </div>
          </GlassCard>
        ))}
      </div>
    </ModuleLayout>
  );
}
