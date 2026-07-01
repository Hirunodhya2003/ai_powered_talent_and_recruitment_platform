import { useState } from "react";
import { useNavigate } from "react-router";
import { DashboardLayout } from "../../components/DashboardLayout";
import { GlassCard } from "../../components/GlassCard";
import { Badge } from "../../components/ui/badge";
import { Button } from "../../components/ui/button";
import {
  Bookmark,
  MapPin,
  DollarSign,
  Building,
  Star,
  Clock,
  Trash2,
  Bell,
  BellPlus,
  Filter,
  ExternalLink,
} from "lucide-react";

const savedJobs = [
  {
    id: 1,
    title: "Senior Software Engineer",
    company: "Google",
    location: "Mountain View, CA",
    type: "Hybrid",
    salary: "$180k – $240k",
    matchScore: 97,
    dateSaved: "June 14, 2026",
    logo: "G",
    logoColor: "#4285F4",
  },
  {
    id: 2,
    title: "Staff Product Manager",
    company: "Stripe",
    location: "Remote",
    type: "Remote",
    salary: "$170k – $220k",
    matchScore: 94,
    dateSaved: "June 13, 2026",
    logo: "S",
    logoColor: "#635BFF",
  },
  {
    id: 3,
    title: "Principal Frontend Engineer",
    company: "Figma",
    location: "San Francisco, CA",
    type: "Hybrid",
    salary: "$190k – $250k",
    matchScore: 91,
    dateSaved: "June 12, 2026",
    logo: "F",
    logoColor: "#F24E1E",
  },
  {
    id: 4,
    title: "Engineering Manager",
    company: "Shopify",
    location: "Remote",
    type: "Remote",
    salary: "$160k – $210k",
    matchScore: 88,
    dateSaved: "June 11, 2026",
    logo: "S",
    logoColor: "#96BF48",
  },
  {
    id: 5,
    title: "Senior Data Scientist",
    company: "Airbnb",
    location: "New York, NY",
    type: "On-site",
    salary: "$155k – $200k",
    matchScore: 85,
    dateSaved: "June 10, 2026",
    logo: "A",
    logoColor: "#FF5A5F",
  },
  {
    id: 6,
    title: "DevOps Engineer",
    company: "Cloudflare",
    location: "Austin, TX",
    type: "On-site",
    salary: "$140k – $180k",
    matchScore: 82,
    dateSaved: "June 9, 2026",
    logo: "C",
    logoColor: "#F6821F",
  },
];

const jobAlerts = [
  {
    id: 1,
    name: "Senior Engineer – Remote",
    criteria: "Senior Engineer · Remote · $150k+",
    frequency: "Daily",
    matches: 12,
  },
  {
    id: 2,
    name: "Product Manager – Tech",
    criteria: "Product Manager · Tech industry · $130k+",
    frequency: "Weekly",
    matches: 7,
  },
];

const filters = ["All", "Remote", "Hybrid", "On-site"];

export function SavedJobs() {
  const navigate = useNavigate();
  const [activeFilter, setActiveFilter] = useState("All");
  const [removed, setRemoved] = useState<number[]>([]);

  const visible = savedJobs.filter(
    (job) =>
      !removed.includes(job.id) &&
      (activeFilter === "All" || job.type === activeFilter)
  );

  return (
    <DashboardLayout role="jobseeker">
      <div className="p-6 space-y-6">
        {/* Header */}
        <div className="flex items-center justify-between">
          <div>
            <h1 className="text-2xl font-bold text-white">Saved Jobs</h1>
            <p className="text-gray-400 mt-1">
              {visible.length} saved positions
            </p>
          </div>
          <Button
            onClick={() => navigate("/seeker/job-search")}
            className="bg-[#D4AF37] text-black font-semibold hover:bg-[#D4AF37]/90"
          >
            <ExternalLink className="w-4 h-4 mr-2" />
            Browse More Jobs
          </Button>
        </div>

        {/* Filters */}
        <div className="flex items-center gap-3">
          <Filter className="w-4 h-4 text-gray-400" />
          {filters.map((f) => (
            <button
              key={f}
              onClick={() => setActiveFilter(f)}
              className={`px-4 py-1.5 rounded-full text-sm font-medium transition-all ${
                activeFilter === f
                  ? "bg-[#D4AF37] text-black"
                  : "bg-[#2D2D2D] text-gray-300 hover:bg-[#D4AF37]/20 hover:text-[#D4AF37]"
              }`}
            >
              {f}
            </button>
          ))}
        </div>

        {/* Job List */}
        <div className="space-y-4">
          {visible.length === 0 ? (
            <GlassCard className="p-6">
              <p className="text-center text-gray-400 py-8">
                No saved jobs match this filter.
              </p>
            </GlassCard>
          ) : (
            visible.map((job) => (
              <GlassCard key={job.id} className="p-6">
                <div className="flex items-start justify-between gap-4">
                  <div className="flex items-start gap-4 flex-1">
                    {/* Logo */}
                    <div
                      className="w-12 h-12 rounded-xl flex items-center justify-center text-white font-bold text-lg shrink-0"
                      style={{ backgroundColor: job.logoColor + "33", border: `1px solid ${job.logoColor}55` }}
                    >
                      <span style={{ color: job.logoColor }}>{job.logo}</span>
                    </div>

                    {/* Info */}
                    <div className="flex-1 min-w-0">
                      <div className="flex items-center gap-2 flex-wrap">
                        <h3 className="text-white font-semibold text-lg">
                          {job.title}
                        </h3>
                        <Badge className="bg-[#D4AF37]/10 text-[#D4AF37] border border-[#D4AF37]/20 text-xs">
                          {job.matchScore}% match
                        </Badge>
                      </div>
                      <div className="flex items-center gap-1 mt-1">
                        <Building className="w-3.5 h-3.5 text-gray-400" />
                        <span className="text-gray-300 text-sm">{job.company}</span>
                      </div>
                      <div className="flex items-center gap-4 mt-2 flex-wrap">
                        <span className="flex items-center gap-1 text-gray-400 text-sm">
                          <MapPin className="w-3.5 h-3.5" />
                          {job.location}
                        </span>
                        <span className="flex items-center gap-1 text-gray-400 text-sm">
                          <DollarSign className="w-3.5 h-3.5" />
                          {job.salary}
                        </span>
                        <span className="flex items-center gap-1 text-gray-400 text-sm">
                          <Clock className="w-3.5 h-3.5" />
                          Saved {job.dateSaved}
                        </span>
                        <Badge
                          className={`text-xs ${
                            job.type === "Remote"
                              ? "bg-green-500/10 text-green-400 border border-green-500/20"
                              : job.type === "Hybrid"
                              ? "bg-blue-500/10 text-blue-400 border border-blue-500/20"
                              : "bg-purple-500/10 text-purple-400 border border-purple-500/20"
                          }`}
                        >
                          {job.type}
                        </Badge>
                      </div>
                    </div>
                  </div>

                  {/* Match Score Visual */}
                  <div className="flex flex-col items-center shrink-0">
                    <div className="relative w-14 h-14">
                      <svg className="w-14 h-14 -rotate-90" viewBox="0 0 56 56">
                        <circle cx="28" cy="28" r="24" fill="none" stroke="#2D2D2D" strokeWidth="4" />
                        <circle
                          cx="28" cy="28" r="24" fill="none"
                          stroke="#D4AF37" strokeWidth="4"
                          strokeDasharray={`${(job.matchScore / 100) * 150.8} 150.8`}
                          strokeLinecap="round"
                        />
                      </svg>
                      <span className="absolute inset-0 flex items-center justify-center text-xs font-bold text-[#D4AF37]">
                        {job.matchScore}%
                      </span>
                    </div>
                    <span className="text-xs text-gray-500 mt-1">AI Match</span>
                  </div>
                </div>

                {/* Actions */}
                <div className="flex items-center gap-3 mt-4 pt-4 border-t border-[#D4AF37]/10">
                  <Button
                    onClick={() => navigate("/seeker/job-search")}
                    className="bg-[#D4AF37] text-black font-semibold hover:bg-[#D4AF37]/90 flex-1"
                  >
                    Apply Now
                  </Button>
                  <Button
                    variant="outline"
                    className="border-[#D4AF37]/20 text-gray-300 hover:border-[#D4AF37]/50 hover:text-white"
                  >
                    <Star className="w-4 h-4 mr-2 text-[#D4AF37]" />
                    View Details
                  </Button>
                  <Button
                    variant="outline"
                    onClick={() => setRemoved((r) => [...r, job.id])}
                    className="border-red-500/20 text-red-400 hover:border-red-500/50 hover:bg-red-500/10"
                  >
                    <Trash2 className="w-4 h-4" />
                  </Button>
                </div>
              </GlassCard>
            ))
          )}
        </div>

        {/* Job Alerts Section */}
        <div>
          <div className="flex items-center justify-between mb-4">
            <div className="flex items-center gap-2">
              <Bell className="w-5 h-5 text-[#D4AF37]" />
              <h2 className="text-lg font-semibold text-white">Job Alerts</h2>
              <Badge className="bg-[#D4AF37]/10 text-[#D4AF37] border border-[#D4AF37]/20 text-xs">
                {jobAlerts.length} Active
              </Badge>
            </div>
            <Button
              variant="outline"
              onClick={() => navigate("/seeker/job-search")}
              className="border-[#D4AF37]/20 text-[#D4AF37] hover:bg-[#D4AF37]/10 text-sm"
            >
              <BellPlus className="w-4 h-4 mr-2" />
              Set Up Alert
            </Button>
          </div>
          <div className="space-y-3">
            {jobAlerts.map((alert) => (
              <GlassCard key={alert.id} className="p-6">
                <div className="flex items-center justify-between">
                  <div>
                    <h3 className="text-white font-medium">{alert.name}</h3>
                    <p className="text-gray-400 text-sm mt-0.5">{alert.criteria}</p>
                    <div className="flex items-center gap-3 mt-2">
                      <Badge className="bg-[#D4AF37]/10 text-[#D4AF37] border border-[#D4AF37]/20 text-xs">
                        {alert.frequency}
                      </Badge>
                      <span className="text-gray-400 text-sm">
                        {alert.matches} new matches this week
                      </span>
                    </div>
                  </div>
                  <div className="flex items-center gap-2">
                    <Button
                      variant="outline"
                      className="border-[#D4AF37]/20 text-[#D4AF37] hover:bg-[#D4AF37]/10 text-sm"
                    >
                      Edit
                    </Button>
                    <Button
                      variant="outline"
                      className="border-red-500/20 text-red-400 hover:border-red-500/50 text-sm"
                    >
                      Delete
                    </Button>
                  </div>
                </div>
              </GlassCard>
            ))}
          </div>
        </div>
      </div>
    </DashboardLayout>
  );
}
