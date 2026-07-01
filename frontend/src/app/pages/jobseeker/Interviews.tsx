import { DashboardLayout } from "../../components/DashboardLayout";
import { GlassCard } from "../../components/GlassCard";
import { Button } from "../../components/ui/button";
import { Badge } from "../../components/ui/badge";
import { Calendar, Video, MapPin, Clock, Building, Users } from "lucide-react";
import { useNavigate } from "react-router";

export function JobSeekerInterviews() {
  const navigate = useNavigate();

  const interviews = [
    {
      id: 1,
      position: "Senior React Developer",
      company: "Tech Innovations Inc.",
      type: "Technical Interview",
      date: "Tomorrow",
      time: "10:00 AM - 11:30 AM",
      location: "Virtual",
      interviewer: "John Smith - Engineering Manager",
      meetingLink: "/interview/1",
      status: "upcoming",
    },
    {
      id: 2,
      position: "Full Stack Engineer",
      company: "StartupXYZ",
      type: "HR Round",
      date: "June 20, 2026",
      time: "2:00 PM - 2:45 PM",
      location: "Virtual",
      interviewer: "Sarah Johnson - HR Manager",
      meetingLink: "/interview/2",
      status: "upcoming",
    },
    {
      id: 3,
      position: "Frontend Developer",
      company: "Digital Agency",
      type: "Final Interview",
      date: "June 14, 2026",
      time: "3:00 PM - 4:00 PM",
      location: "Virtual",
      interviewer: "Mike Chen - CTO",
      status: "completed",
    },
  ];

  return (
    <DashboardLayout role="jobseeker">
      <div className="mb-8">
        <h1 className="text-3xl font-bold text-white mb-2">My Interviews</h1>
        <p className="text-gray-400">Manage your interview schedule</p>
      </div>

      <div className="space-y-6">
        {interviews.map((interview) => (
          <GlassCard key={interview.id} className="p-6">
            <div className="flex items-start justify-between mb-4">
              <div>
                <Badge
                  variant={interview.status === "upcoming" ? "default" : "secondary"}
                  className="mb-2"
                >
                  {interview.status === "upcoming" ? "Upcoming" : "Completed"}
                </Badge>
                <h3 className="text-xl font-semibold text-white mb-1">{interview.position}</h3>
                <div className="flex items-center space-x-2 text-gray-300">
                  <Building className="h-4 w-4" />
                  <span>{interview.company}</span>
                </div>
              </div>
              <Badge variant="outline" className="text-[#D4AF37] border-[#D4AF37]/40">
                {interview.type}
              </Badge>
            </div>

            <div className="grid md:grid-cols-2 gap-4 mb-6">
              <div className="flex items-center space-x-3">
                <div className="rounded-lg bg-[#D4AF37]/10 p-2">
                  <Calendar className="h-5 w-5 text-[#D4AF37]" />
                </div>
                <div>
                  <p className="text-sm text-gray-400">Date</p>
                  <p className="text-white">{interview.date}</p>
                </div>
              </div>
              <div className="flex items-center space-x-3">
                <div className="rounded-lg bg-[#D4AF37]/10 p-2">
                  <Clock className="h-5 w-5 text-[#D4AF37]" />
                </div>
                <div>
                  <p className="text-sm text-gray-400">Time</p>
                  <p className="text-white">{interview.time}</p>
                </div>
              </div>
              <div className="flex items-center space-x-3">
                <div className="rounded-lg bg-[#D4AF37]/10 p-2">
                  <MapPin className="h-5 w-5 text-[#D4AF37]" />
                </div>
                <div>
                  <p className="text-sm text-gray-400">Location</p>
                  <p className="text-white">{interview.location}</p>
                </div>
              </div>
              <div className="flex items-center space-x-3">
                <div className="rounded-lg bg-[#D4AF37]/10 p-2">
                  <Users className="h-5 w-5 text-[#D4AF37]" />
                </div>
                <div>
                  <p className="text-sm text-gray-400">Interviewer</p>
                  <p className="text-white">{interview.interviewer}</p>
                </div>
              </div>
            </div>

            {interview.status === "upcoming" && (
              <div className="flex items-center space-x-3">
                <Button onClick={() => navigate(interview.meetingLink ?? "/interviews/calendar")}>
                  <Video className="mr-2 h-4 w-4" />
                  Join Interview
                </Button>
                <Button variant="outline" onClick={() => navigate("/interviews/calendar")}>Add to Calendar</Button>
                <Button variant="outline" onClick={() => navigate("/interviews/schedule")}>Reschedule</Button>
              </div>
            )}
          </GlassCard>
        ))}
      </div>
    </DashboardLayout>
  );
}
