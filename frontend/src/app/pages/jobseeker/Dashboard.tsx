import { DashboardLayout } from "../../components/DashboardLayout";
import { GlassCard } from "../../components/GlassCard";
import { StatCard } from "../../components/StatCard";
import { Button } from "../../components/ui/button";
import { Badge } from "../../components/ui/badge";
import { Progress } from "../../components/ui/progress";
import { useNavigate } from "react-router";
import {
  Briefcase,
  FileCheck,
  Calendar,
  TrendingUp,
  Star,
  MapPin,
  DollarSign,
  Clock,
  Building,
  ArrowRight,
  Sparkles,
  Award,
  BookOpen,
} from "lucide-react";

export function JobSeekerDashboard() {
  const navigate = useNavigate();
  const aiJobRecommendations = [
    {
      id: 1,
      title: "Senior Frontend Developer",
      company: "Tech Innovations Inc.",
      location: "San Francisco, CA",
      salary: "$120k - $180k",
      matchScore: 98,
      type: "Full-time",
      postedAt: "2 hours ago",
    },
    {
      id: 2,
      title: "React Developer",
      company: "Digital Solutions",
      location: "Remote",
      salary: "$100k - $150k",
      matchScore: 95,
      type: "Remote",
      postedAt: "5 hours ago",
    },
    {
      id: 3,
      title: "Full Stack Engineer",
      company: "StartupXYZ",
      location: "New York, NY",
      salary: "$130k - $170k",
      matchScore: 92,
      type: "Hybrid",
      postedAt: "1 day ago",
    },
  ];

  const upcomingInterviews = [
    {
      id: 1,
      company: "Tech Innovations Inc.",
      position: "Senior Frontend Developer",
      date: "Tomorrow",
      time: "10:00 AM",
      type: "Technical Interview",
    },
    {
      id: 2,
      company: "Digital Solutions",
      position: "React Developer",
      date: "June 20, 2026",
      time: "2:00 PM",
      type: "HR Round",
    },
  ];

  const skillGaps = [
    { skill: "TypeScript", current: 70, target: 90 },
    { skill: "System Design", current: 60, target: 85 },
    { skill: "Cloud Architecture", current: 50, target: 80 },
  ];

  const learningPaths = [
    {
      title: "Advanced TypeScript Patterns",
      progress: 45,
      lessons: 12,
      completed: 5,
    },
    {
      title: "System Design Mastery",
      progress: 30,
      lessons: 20,
      completed: 6,
    },
  ];

  return (
    <DashboardLayout role="jobseeker">
      {/* Welcome Section */}
      <div className="mb-8">
        <h1 className="text-3xl font-bold text-white mb-2">Welcome back, John!</h1>
        <p className="text-gray-400">Here's what's happening with your job search</p>
      </div>

      {/* Stats Grid */}
      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6 mb-8">
        <StatCard
          title="Applications Sent"
          value="24"
          icon={FileCheck}
          trend={{ value: 12, isPositive: true }}
        />
        <StatCard
          title="Profile Views"
          value="156"
          icon={TrendingUp}
          trend={{ value: 23, isPositive: true }}
        />
        <StatCard
          title="Interviews Scheduled"
          value="8"
          icon={Calendar}
          trend={{ value: 5, isPositive: true }}
        />
        <StatCard
          title="Job Matches"
          value="42"
          icon={Briefcase}
        />
      </div>

      {/* Profile Completion */}
      <GlassCard className="p-6 mb-8">
        <div className="flex items-start justify-between mb-4">
          <div>
            <h3 className="text-xl font-semibold text-white mb-1">Complete Your Profile</h3>
            <p className="text-gray-400">85% completed - Almost there!</p>
          </div>
          <div className="rounded-full bg-[#D4AF37]/10 p-3">
            <Star className="h-6 w-6 text-[#D4AF37]" />
          </div>
        </div>
        <Progress value={85} className="mb-4" />
        <div className="flex flex-wrap gap-2">
          <Badge variant="secondary">✓ Personal Info</Badge>
          <Badge variant="secondary">✓ Experience</Badge>
          <Badge variant="secondary">✓ Education</Badge>
          <Badge variant="outline">Add Certifications</Badge>
          <Badge variant="outline">Add Portfolio</Badge>
        </div>
      </GlassCard>

      <div className="grid lg:grid-cols-3 gap-6 mb-8">
        {/* AI Job Recommendations */}
        <div className="lg:col-span-2">
          <GlassCard className="p-6">
            <div className="flex items-center justify-between mb-6">
              <div className="flex items-center space-x-2">
                <Sparkles className="h-6 w-6 text-[#D4AF37]" />
                <h2 className="text-xl font-semibold text-white">AI-Recommended Jobs</h2>
              </div>
              <Button variant="ghost" size="sm" onClick={() => navigate("/jobseeker/jobs")}>View All</Button>
            </div>
            <div className="space-y-4">
              {aiJobRecommendations.map((job) => (
                <div
                  key={job.id}
                  className="p-4 rounded-lg bg-[#2D2D2D]/50 border border-[#D4AF37]/10 hover:border-[#D4AF37]/30 transition-colors"
                >
                  <div className="flex items-start justify-between mb-3">
                    <div className="flex-1">
                      <div className="flex items-center space-x-2 mb-1">
                        <h3 className="font-semibold text-white">{job.title}</h3>
                        <Badge className="bg-[#D4AF37]/20 text-[#D4AF37] border-[#D4AF37]/40">
                          {job.matchScore}% Match
                        </Badge>
                      </div>
                      <div className="flex items-center space-x-2 text-sm text-gray-400">
                        <Building className="h-4 w-4" />
                        <span>{job.company}</span>
                      </div>
                    </div>
                    <Clock className="h-4 w-4 text-gray-400" />
                  </div>
                  <div className="flex items-center flex-wrap gap-3 text-sm text-gray-400 mb-3">
                    <div className="flex items-center space-x-1">
                      <MapPin className="h-4 w-4" />
                      <span>{job.location}</span>
                    </div>
                    <div className="flex items-center space-x-1">
                      <DollarSign className="h-4 w-4" />
                      <span>{job.salary}</span>
                    </div>
                    <Badge variant="outline" className="text-xs">{job.type}</Badge>
                  </div>
                  <div className="flex items-center justify-between">
                    <span className="text-xs text-gray-500">{job.postedAt}</span>
                    <Button size="sm" onClick={() => navigate("/jobseeker/applications")}>Apply Now <ArrowRight className="ml-2 h-4 w-4" /></Button>
                  </div>
                </div>
              ))}
            </div>
          </GlassCard>
        </div>

        {/* Upcoming Interviews */}
        <div>
          <GlassCard className="p-6 mb-6">
            <div className="flex items-center space-x-2 mb-4">
              <Calendar className="h-5 w-5 text-[#D4AF37]" />
              <h2 className="text-lg font-semibold text-white">Upcoming Interviews</h2>
            </div>
            <div className="space-y-4">
              {upcomingInterviews.map((interview) => (
                <div
                  key={interview.id}
                  className="p-4 rounded-lg bg-[#2D2D2D]/50 border border-[#D4AF37]/10"
                >
                  <h3 className="font-semibold text-white mb-1">{interview.company}</h3>
                  <p className="text-sm text-gray-400 mb-2">{interview.position}</p>
                  <div className="flex items-center justify-between text-sm mb-2">
                    <span className="text-gray-400">{interview.date}</span>
                    <span className="text-[#D4AF37]">{interview.time}</span>
                  </div>
                  <Badge variant="outline" className="text-xs">{interview.type}</Badge>
                </div>
              ))}
            </div>
          </GlassCard>

          {/* Learning Paths */}
          <GlassCard className="p-6">
            <div className="flex items-center space-x-2 mb-4">
              <BookOpen className="h-5 w-5 text-[#D4AF37]" />
              <h2 className="text-lg font-semibold text-white">Learning Paths</h2>
            </div>
            <div className="space-y-4">
              {learningPaths.map((course, index) => (
                <div key={index} className="space-y-2">
                  <div className="flex items-center justify-between">
                    <h4 className="text-sm font-medium text-white">{course.title}</h4>
                    <span className="text-xs text-[#D4AF37]">{course.progress}%</span>
                  </div>
                  <Progress value={course.progress} className="h-2" />
                  <p className="text-xs text-gray-400">
                    {course.completed} of {course.lessons} lessons completed
                  </p>
                </div>
              ))}
            </div>
          </GlassCard>
        </div>
      </div>

      {/* Skill Gap Analysis */}
      <GlassCard className="p-6">
        <div className="flex items-center space-x-2 mb-6">
          <Award className="h-6 w-6 text-[#D4AF37]" />
          <h2 className="text-xl font-semibold text-white">AI Skill Gap Analysis</h2>
        </div>
        <div className="grid md:grid-cols-3 gap-6">
          {skillGaps.map((skill, index) => (
            <div key={index} className="space-y-3">
              <div className="flex items-center justify-between">
                <h4 className="font-medium text-white">{skill.skill}</h4>
                <Badge variant="outline">{skill.current}%</Badge>
              </div>
              <div className="space-y-1">
                <div className="flex justify-between text-xs text-gray-400">
                  <span>Current</span>
                  <span>Target: {skill.target}%</span>
                </div>
                <Progress value={skill.current} className="h-2" />
              </div>
              <Button size="sm" variant="outline" className="w-full">
                Start Learning
              </Button>
            </div>
          ))}
        </div>
      </GlassCard>
    </DashboardLayout>
  );
}
