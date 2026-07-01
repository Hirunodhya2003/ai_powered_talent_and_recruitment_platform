import { useState } from "react";
import { ModuleLayout } from "../../components/ModuleLayout";
import { GlassCard } from "../../components/GlassCard";
import { Button } from "../../components/ui/button";
import {
  Calendar,
  CalendarDays,
  GitBranch,
  Video,
  ChevronLeft,
  ChevronRight,
  Clock,
  Users,
} from "lucide-react";

const interviewTabs = [
  { label: "Schedule", path: "/interviews/schedule", icon: Calendar },
  { label: "Calendar", path: "/interviews/calendar", icon: CalendarDays },
  { label: "Timeline", path: "/interviews/timeline", icon: GitBranch },
  { label: "Video Room", path: "/interviews/video-room", icon: Video },
];

interface InterviewEvent {
  id: number;
  day: number;
  time: string;
  candidate: string;
  type: "Phone" | "Technical" | "HR" | "Panel" | "Final";
}

const juneInterviews: InterviewEvent[] = [
  { id: 1, day: 2, time: "9:00 AM", candidate: "Alex Morgan", type: "Phone" },
  { id: 2, day: 3, time: "11:00 AM", candidate: "Priya Sharma", type: "Technical" },
  { id: 3, day: 3, time: "2:00 PM", candidate: "James O'Brien", type: "HR" },
  { id: 4, day: 5, time: "10:00 AM", candidate: "Marcus Johnson", type: "Panel" },
  { id: 5, day: 9, time: "9:30 AM", candidate: "Sofia Lindqvist", type: "Phone" },
  { id: 6, day: 10, time: "1:00 PM", candidate: "Carlos Rivera", type: "Final" },
  { id: 7, day: 10, time: "3:30 PM", candidate: "Aisha Patel", type: "Technical" },
  { id: 8, day: 11, time: "10:00 AM", candidate: "Kwame Asante", type: "HR" },
  { id: 9, day: 12, time: "2:00 PM", candidate: "Emma Thompson", type: "Panel" },
  { id: 10, day: 16, time: "9:00 AM", candidate: "Ryan Park", type: "Phone" },
  { id: 11, day: 17, time: "10:00 AM", candidate: "Marcus Johnson", type: "Technical" },
  { id: 12, day: 17, time: "2:00 PM", candidate: "Priya Sharma", type: "HR" },
  { id: 13, day: 18, time: "11:00 AM", candidate: "Aisha Patel", type: "Panel" },
  { id: 14, day: 19, time: "3:00 PM", candidate: "Carlos Rivera", type: "Final" },
  { id: 15, day: 23, time: "10:00 AM", candidate: "Natalie Brooks", type: "Phone" },
  { id: 16, day: 24, time: "1:30 PM", candidate: "Derek Walsh", type: "Technical" },
  { id: 17, day: 25, time: "9:00 AM", candidate: "Yuki Tanaka", type: "HR" },
  { id: 18, day: 26, time: "2:00 PM", candidate: "Sofia Lindqvist", type: "Final" },
  { id: 19, day: 30, time: "11:00 AM", candidate: "James O'Brien", type: "Panel" },
];

const typeColors: Record<string, string> = {
  Phone: "bg-blue-500/20 text-blue-300 border-blue-500/30",
  Technical: "bg-purple-500/20 text-purple-300 border-purple-500/30",
  HR: "bg-green-500/20 text-green-300 border-green-500/30",
  Panel: "bg-orange-500/20 text-orange-300 border-orange-500/30",
  Final: "bg-[#D4AF37]/20 text-[#D4AF37] border-[#D4AF37]/30",
};

const typeDotColors: Record<string, string> = {
  Phone: "bg-blue-400",
  Technical: "bg-purple-400",
  HR: "bg-green-400",
  Panel: "bg-orange-400",
  Final: "bg-[#D4AF37]",
};

export function InterviewCalendar() {
  const [view, setView] = useState<"month" | "week" | "day">("month");
  const [selectedDay, setSelectedDay] = useState<number | null>(17);

  // June 2026 starts on a Monday (day-of-week index 0 = Mon)
  const startDayOfWeek = 0; // Monday
  const daysInMonth = 30;
  const monthName = "June 2026";
  const dayHeaders = ["Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun"];

  const interviewsByDay: Record<number, InterviewEvent[]> = {};
  juneInterviews.forEach((iv) => {
    if (!interviewsByDay[iv.day]) interviewsByDay[iv.day] = [];
    interviewsByDay[iv.day].push(iv);
  });

  const selectedDayInterviews = selectedDay ? (interviewsByDay[selectedDay] || []) : [];

  // Build calendar cells
  const cells: (number | null)[] = [];
  for (let i = 0; i < startDayOfWeek; i++) cells.push(null);
  for (let d = 1; d <= daysInMonth; d++) cells.push(d);
  while (cells.length % 7 !== 0) cells.push(null);

  return (
    <ModuleLayout
      title="Interview Calendar"
      subtitle="Visual calendar view of all scheduled interviews"
      icon={CalendarDays}
      tabs={interviewTabs}
      backPath="/recruiter/dashboard"
      backLabel="Back to Portal"
    >
      {/* View toggle & navigation */}
      <div className="flex items-center justify-between mb-6">
        <div className="flex items-center gap-3">
          <button className="text-gray-400 hover:text-white transition-colors">
            <ChevronLeft className="h-5 w-5" />
          </button>
          <h2 className="text-white font-semibold text-lg">{monthName}</h2>
          <button className="text-gray-400 hover:text-white transition-colors">
            <ChevronRight className="h-5 w-5" />
          </button>
        </div>
        <div className="flex bg-[#1E1E1E] rounded-lg border border-[#D4AF37]/20 p-1 gap-1">
          {(["month", "week", "day"] as const).map((v) => (
            <button
              key={v}
              onClick={() => setView(v)}
              className={`px-4 py-1.5 rounded-md text-sm font-medium capitalize transition-all ${
                view === v
                  ? "bg-[#D4AF37] text-black"
                  : "text-gray-400 hover:text-white"
              }`}
            >
              {v}
            </button>
          ))}
        </div>
      </div>

      <div className="grid grid-cols-1 lg:grid-cols-3 gap-6">
        {/* Calendar Grid */}
        <div className="lg:col-span-2">
          <GlassCard className="p-4">
            {/* Day headers */}
            <div className="grid grid-cols-7 mb-2">
              {dayHeaders.map((h) => (
                <div key={h} className="text-center text-gray-500 text-xs font-medium py-2">
                  {h}
                </div>
              ))}
            </div>
            {/* Calendar grid */}
            <div className="grid grid-cols-7 gap-1">
              {cells.map((day, idx) => {
                if (day === null) {
                  return <div key={`empty-${idx}`} className="h-20" />;
                }
                const dayEvents = interviewsByDay[day] || [];
                const hasEvents = dayEvents.length > 0;
                const isSelected = selectedDay === day;
                const isToday = day === 17;

                return (
                  <button
                    key={day}
                    onClick={() => setSelectedDay(day)}
                    className={`h-20 p-1.5 rounded-lg text-left transition-all border ${
                      isSelected
                        ? "border-[#D4AF37] bg-[#D4AF37]/10"
                        : hasEvents
                        ? "border-[#D4AF37]/25 hover:border-[#D4AF37]/50 bg-[#2D2D2D]/40"
                        : "border-transparent hover:border-[#D4AF37]/20 hover:bg-[#2D2D2D]/30"
                    }`}
                  >
                    <div className="flex items-center justify-between mb-1">
                      <span
                        className={`text-xs font-semibold w-5 h-5 flex items-center justify-center rounded-full ${
                          isToday
                            ? "bg-[#D4AF37] text-black"
                            : isSelected
                            ? "text-[#D4AF37]"
                            : "text-gray-300"
                        }`}
                      >
                        {day}
                      </span>
                      {hasEvents && (
                        <span className="text-[10px] bg-[#D4AF37]/20 text-[#D4AF37] rounded-full px-1.5 font-medium">
                          {dayEvents.length}
                        </span>
                      )}
                    </div>
                    <div className="space-y-0.5">
                      {dayEvents.slice(0, 2).map((ev) => (
                        <div
                          key={ev.id}
                          className={`flex items-center gap-1 text-[9px] rounded px-1 py-0.5 truncate ${typeColors[ev.type]}`}
                        >
                          <div className={`w-1.5 h-1.5 rounded-full flex-shrink-0 ${typeDotColors[ev.type]}`} />
                          <span className="truncate">{ev.candidate.split(" ")[0]}</span>
                        </div>
                      ))}
                      {dayEvents.length > 2 && (
                        <div className="text-[9px] text-gray-500 pl-1">
                          +{dayEvents.length - 2} more
                        </div>
                      )}
                    </div>
                  </button>
                );
              })}
            </div>
          </GlassCard>

          {/* Legend */}
          <div className="flex flex-wrap gap-3 mt-3">
            {Object.entries(typeDotColors).map(([type, color]) => (
              <div key={type} className="flex items-center gap-1.5">
                <div className={`w-2 h-2 rounded-full ${color}`} />
                <span className="text-gray-400 text-xs">{type}</span>
              </div>
            ))}
          </div>
        </div>

        {/* Day detail panel */}
        <div>
          <GlassCard className="p-4">
            <div className="flex items-center gap-2 mb-4">
              <Calendar className="h-4 w-4 text-[#D4AF37]" />
              <h3 className="text-white font-semibold">
                {selectedDay ? `June ${selectedDay}, 2026` : "Select a day"}
              </h3>
            </div>
            {selectedDay && selectedDayInterviews.length === 0 && (
              <p className="text-gray-500 text-sm text-center py-8">No interviews scheduled</p>
            )}
            {!selectedDay && (
              <p className="text-gray-500 text-sm text-center py-8">Click a day to see interviews</p>
            )}
            <div className="space-y-3">
              {selectedDayInterviews.map((iv) => (
                <div key={iv.id} className={`rounded-lg border p-3 ${typeColors[iv.type]}`}>
                  <div className="flex items-center justify-between mb-1">
                    <span className="text-xs font-semibold">{iv.type}</span>
                    <span className="flex items-center gap-1 text-xs opacity-80">
                      <Clock className="h-3 w-3" />
                      {iv.time}
                    </span>
                  </div>
                  <div className="text-white text-sm font-medium">{iv.candidate}</div>
                </div>
              ))}
            </div>
            {selectedDay && selectedDayInterviews.length > 0 && (
              <div className="mt-4 pt-4 border-t border-[#D4AF37]/10">
                <div className="flex items-center gap-2 text-gray-400 text-xs">
                  <Users className="h-3.5 w-3.5" />
                  {selectedDayInterviews.length} interview{selectedDayInterviews.length > 1 ? "s" : ""} scheduled
                </div>
              </div>
            )}
          </GlassCard>
        </div>
      </div>
    </ModuleLayout>
  );
}
