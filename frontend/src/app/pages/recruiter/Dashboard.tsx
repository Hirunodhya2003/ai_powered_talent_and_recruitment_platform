import { DashboardLayout } from "../../components/DashboardLayout";
import { GlassCard } from "../../components/GlassCard";
import { StatCard } from "../../components/StatCard";
import { Button } from "../../components/ui/button";
import { Badge } from "../../components/ui/badge";
import { Progress } from "../../components/ui/progress";
import { useNavigate } from "react-router";
import {
  Briefcase,
  Users,
  Calendar,
  TrendingUp,
  Clock,
  CheckCircle,
  Eye,
  Star,
} from "lucide-react";
import { BarChart, Bar, XAxis, YAxis, CartesianGrid, Tooltip, ResponsiveContainer, PieChart, Pie, Cell } from "recharts";

export function RecruiterDashboard() {
  const navigate = useNavigate();
  const hiringFunnelData = [
    { name: "Applied", value: 450 },
    { name: "Screened", value: 320 },
    { name: "Interviewed", value: 156 },
    { name: "Offered", value: 45 },
    { name: "Hired", value: 32 },
  ];

  const jobDistributionData = [
    { name: "Active", value: 24, color: "#D4AF37" },
    { name: "Filled", value: 18, color: "#22c55e" },
    { name: "On Hold", value: 8, color: "#eab308" },
  ];

  const activeJobs = [
    { id: 1, title: "Senior React Developer", applicants: 45, status: "Active", daysOpen: 5, filled: 60 },
    { id: 2, title: "Full Stack Engineer", applicants: 32, status: "Active", daysOpen: 12, filled: 45 },
    { id: 3, title: "DevOps Engineer", applicants: 28, status: "Active", daysOpen: 8, filled: 70 },
  ];

  const topCandidates = [
    { id: 1, name: "Alice Johnson", position: "Senior React Developer", matchScore: 98, status: "Interview Scheduled" },
    { id: 2, name: "Bob Smith", position: "Full Stack Engineer", matchScore: 95, status: "Resume Review" },
    { id: 3, name: "Carol Williams", position: "DevOps Engineer", matchScore: 92, status: "Phone Screen" },
  ];

  return (
    <DashboardLayout role="recruiter">
      <div className="mb-8">
        <h1 className="text-3xl font-bold text-white mb-2">Recruitment Overview</h1>
        <p className="text-gray-400">Track your hiring progress and candidate pipeline</p>
      </div>

      {/* Stats Grid */}
      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6 mb-8">
        <StatCard
          title="Active Jobs"
          value="24"
          icon={Briefcase}
          trend={{ value: 8, isPositive: true }}
        />
        <StatCard
          title="Total Candidates"
          value="450"
          icon={Users}
          trend={{ value: 15, isPositive: true }}
        />
        <StatCard
          title="Interviews Scheduled"
          value="32"
          icon={Calendar}
          trend={{ value: 12, isPositive: true }}
        />
        <StatCard
          title="Offers Sent"
          value="18"
          icon={TrendingUp}
          trend={{ value: 5, isPositive: true }}
        />
      </div>

      <div className="grid lg:grid-cols-2 gap-6 mb-8">
        {/* Hiring Funnel */}
        <GlassCard className="p-6">
          <h2 className="text-xl font-semibold text-white mb-6">Hiring Funnel</h2>
          <ResponsiveContainer width="100%" height={300}>
            <BarChart data={hiringFunnelData}>
              <CartesianGrid strokeDasharray="3 3" stroke="#2D2D2D" />
              <XAxis dataKey="name" stroke="#999" />
              <YAxis stroke="#999" />
              <Tooltip
                contentStyle={{
                  backgroundColor: "#1E1E1E",
                  border: "1px solid rgba(212, 175, 55, 0.2)",
                  borderRadius: "8px",
                  color: "#fff",
                }}
              />
              <Bar dataKey="value" fill="#D4AF37" radius={[8, 8, 0, 0]} />
            </BarChart>
          </ResponsiveContainer>
        </GlassCard>

        {/* Job Distribution */}
        <GlassCard className="p-6">
          <h2 className="text-xl font-semibold text-white mb-6">Job Distribution</h2>
          <ResponsiveContainer width="100%" height={300}>
            <PieChart>
              <Pie
                data={jobDistributionData}
                cx="50%"
                cy="50%"
                labelLine={false}
                label={({ name, value }) => `${name}: ${value}`}
                outerRadius={100}
                fill="#8884d8"
                dataKey="value"
              >
                {jobDistributionData.map((entry, index) => (
                  <Cell key={`cell-${index}`} fill={entry.color} />
                ))}
              </Pie>
              <Tooltip
                contentStyle={{
                  backgroundColor: "#1E1E1E",
                  border: "1px solid rgba(212, 175, 55, 0.2)",
                  borderRadius: "8px",
                  color: "#fff",
                }}
              />
            </PieChart>
          </ResponsiveContainer>
        </GlassCard>
      </div>

      <div className="grid lg:grid-cols-2 gap-6">
        {/* Active Jobs */}
        <GlassCard className="p-6">
          <div className="flex items-center justify-between mb-6">
            <h2 className="text-xl font-semibold text-white">Active Jobs</h2>
            <Button variant="ghost" size="sm" onClick={() => navigate("/recruiter/jobs")}>View All</Button>
          </div>
          <div className="space-y-4">
            {activeJobs.map((job) => (
              <div
                key={job.id}
                className="p-4 rounded-lg bg-[#2D2D2D]/50 border border-[#D4AF37]/10"
              >
                <div className="flex items-start justify-between mb-3">
                  <div>
                    <h3 className="font-semibold text-white mb-1">{job.title}</h3>
                    <div className="flex items-center space-x-3 text-sm text-gray-400">
                      <div className="flex items-center space-x-1">
                        <Users className="h-4 w-4" />
                        <span>{job.applicants} applicants</span>
                      </div>
                      <div className="flex items-center space-x-1">
                        <Clock className="h-4 w-4" />
                        <span>{job.daysOpen} days open</span>
                      </div>
                    </div>
                  </div>
                  <Badge className="bg-[#D4AF37]/20 text-[#D4AF37] border-[#D4AF37]/40">
                    {job.status}
                  </Badge>
                </div>
                <div className="space-y-1">
                  <div className="flex items-center justify-between text-sm">
                    <span className="text-gray-400">Pipeline Progress</span>
                    <span className="text-white">{job.filled}%</span>
                  </div>
                  <Progress value={job.filled} className="h-2" />
                </div>
              </div>
            ))}
          </div>
        </GlassCard>

        {/* Top Candidates */}
        <GlassCard className="p-6">
          <div className="flex items-center justify-between mb-6">
            <h2 className="text-xl font-semibold text-white">Top AI-Matched Candidates</h2>
            <Button variant="ghost" size="sm" onClick={() => navigate("/recruiter/candidates")}>View All</Button>
          </div>
          <div className="space-y-4">
            {topCandidates.map((candidate) => (
              <div
                key={candidate.id}
                className="p-4 rounded-lg bg-[#2D2D2D]/50 border border-[#D4AF37]/10 hover:border-[#D4AF37]/30 transition-colors cursor-pointer"
              >
                <div className="flex items-start justify-between mb-3">
                  <div>
                    <h3 className="font-semibold text-white mb-1">{candidate.name}</h3>
                    <p className="text-sm text-gray-400">{candidate.position}</p>
                  </div>
                  <div className="flex items-center space-x-1 bg-[#D4AF37]/20 text-[#D4AF37] px-2 py-1 rounded">
                    <Star className="h-3 w-3" />
                    <span className="text-sm font-semibold">{candidate.matchScore}%</span>
                  </div>
                </div>
                <div className="flex items-center justify-between">
                  <Badge variant="outline">{candidate.status}</Badge>
                  <Button size="sm" variant="ghost" onClick={() => navigate("/recruiter/candidates")}>
                    <Eye className="h-4 w-4" />
                  </Button>
                </div>
              </div>
            ))}
          </div>
        </GlassCard>
      </div>
    </DashboardLayout>
  );
}
