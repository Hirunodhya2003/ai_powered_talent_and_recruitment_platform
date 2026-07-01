import { DashboardLayout } from "../../components/DashboardLayout";
import { GlassCard } from "../../components/GlassCard";
import { Button } from "../../components/ui/button";
import { Badge } from "../../components/ui/badge";
import { Calendar as CalendarIcon, Clock, Users, Plus } from "lucide-react";
import { Calendar } from "../../components/ui/calendar";

export function InterviewScheduling() {
  const scheduledInterviews = [
    { id: 1, candidate: "Alice Johnson", position: "Senior React Developer", date: "Tomorrow", time: "10:00 AM", type: "Technical" },
    { id: 2, candidate: "Bob Smith", position: "Full Stack Engineer", date: "June 20", time: "2:00 PM", type: "HR Round" },
  ];

  return (
    <DashboardLayout role="recruiter">
      <div className="mb-8 flex items-center justify-between">
        <div>
          <h1 className="text-3xl font-bold text-white mb-2">Interview Scheduling</h1>
          <p className="text-gray-400">Manage interview schedules and availability</p>
        </div>
        <Button>
          <Plus className="mr-2 h-4 w-4" />
          Schedule Interview
        </Button>
      </div>

      <div className="grid lg:grid-cols-3 gap-6">
        <div className="lg:col-span-2 space-y-4">
          {scheduledInterviews.map((interview) => (
            <GlassCard key={interview.id} className="p-6">
              <div className="flex items-start justify-between">
                <div>
                  <h3 className="text-xl font-semibold text-white mb-1">{interview.candidate}</h3>
                  <p className="text-gray-400 mb-4">{interview.position}</p>
                  <div className="flex items-center space-x-6 text-sm">
                    <div className="flex items-center space-x-2 text-gray-300">
                      <CalendarIcon className="h-4 w-4" />
                      <span>{interview.date}</span>
                    </div>
                    <div className="flex items-center space-x-2 text-gray-300">
                      <Clock className="h-4 w-4" />
                      <span>{interview.time}</span>
                    </div>
                    <Badge variant="outline">{interview.type}</Badge>
                  </div>
                </div>
                <div className="flex items-center space-x-2">
                  <Button variant="outline" size="sm">Reschedule</Button>
                  <Button variant="outline" size="sm">Cancel</Button>
                </div>
              </div>
            </GlassCard>
          ))}
        </div>

        <div>
          <GlassCard className="p-6">
            <h3 className="font-semibold text-white mb-4">Calendar</h3>
            <Calendar mode="single" className="rounded-md border-0" />
          </GlassCard>
        </div>
      </div>
    </DashboardLayout>
  );
}
