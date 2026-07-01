import { DashboardLayout } from "../../components/DashboardLayout";
import { StatCard } from "../../components/StatCard";
import { GlassCard } from "../../components/GlassCard";
import { Button } from "../../components/ui/button";
import { Badge } from "../../components/ui/badge";
import { useNavigate } from "react-router";
import { Users, ClipboardCheck, Calendar, Star, Eye } from "lucide-react";

export function HiringManagerDashboard() {
  const navigate = useNavigate();
  const pendingReviews = [
    { id: 1, name: "Alice Johnson", position: "Senior React Developer", matchScore: 98, submittedBy: "Sarah - Recruiter" },
    { id: 2, name: "Bob Smith", position: "Full Stack Engineer", matchScore: 95, submittedBy: "Mike - Recruiter" },
  ];

  return (
    <DashboardLayout role="hiring-manager">
      <div className="mb-8">
        <h1 className="text-3xl font-bold text-white mb-2">Hiring Manager Dashboard</h1>
        <p className="text-gray-400">Review candidates and make hiring decisions</p>
      </div>

      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6 mb-8">
        <StatCard title="Pending Reviews" value="12" icon={Users} />
        <StatCard title="Interviews Today" value="5" icon={Calendar} />
        <StatCard title="Offers Pending" value="3" icon={ClipboardCheck} />
        <StatCard title="Avg Rating" value="4.5" icon={Star} />
      </div>

      <GlassCard className="p-6">
        <h2 className="text-xl font-semibold text-white mb-6">Pending Candidate Reviews</h2>
        <div className="space-y-4">
          {pendingReviews.map((candidate) => (
            <div key={candidate.id} className="p-4 rounded-lg bg-[#2D2D2D]/50 border border-[#D4AF37]/10">
              <div className="flex items-start justify-between">
                <div>
                  <div className="flex items-center space-x-2 mb-2">
                    <h3 className="font-semibold text-white">{candidate.name}</h3>
                    <Badge className="bg-[#D4AF37]/20 text-[#D4AF37] border-[#D4AF37]/40">
                      {candidate.matchScore}% Match
                    </Badge>
                  </div>
                  <p className="text-gray-400 text-sm mb-2">{candidate.position}</p>
                  <p className="text-xs text-gray-500">Submitted by {candidate.submittedBy}</p>
                </div>
                <Button onClick={() => navigate(`/hiring-manager/review/${candidate.id}`)}>
                  <Eye className="mr-2 h-4 w-4" />
                  Review
                </Button>
              </div>
            </div>
          ))}
        </div>
      </GlassCard>
    </DashboardLayout>
  );
}
