import { DashboardLayout } from "../../components/DashboardLayout";
import { GlassCard } from "../../components/GlassCard";
import { Button } from "../../components/ui/button";
import { Badge } from "../../components/ui/badge";
import { CheckCircle, XCircle, Clock } from "lucide-react";
import { toast } from "sonner";

export function HiringDecisions() {
  const candidates = [
    { id: 1, name: "Alice Johnson", position: "Senior React Developer", score: 98, recommendation: "Highly Recommended" },
    { id: 2, name: "Bob Smith", position: "Full Stack Engineer", score: 95, recommendation: "Recommended" },
  ];

  const handleDecision = (name: string, decision: string) => {
    toast.success(`${name} has been ${decision}`);
  };

  return (
    <DashboardLayout role="hiring-manager">
      <div className="mb-8">
        <h1 className="text-3xl font-bold text-white mb-2">Hiring Decisions</h1>
        <p className="text-gray-400">Make final hiring decisions</p>
      </div>

      <div className="space-y-4">
        {candidates.map((candidate) => (
          <GlassCard key={candidate.id} className="p-6">
            <div className="flex items-start justify-between mb-4">
              <div>
                <div className="flex items-center space-x-2 mb-2">
                  <h3 className="text-xl font-semibold text-white">{candidate.name}</h3>
                  <Badge className="bg-[#D4AF37]/20 text-[#D4AF37] border-[#D4AF37]/40">
                    {candidate.score}% Match
                  </Badge>
                </div>
                <p className="text-gray-400 mb-2">{candidate.position}</p>
                <Badge variant="outline" className="text-green-500 border-green-500/40">
                  {candidate.recommendation}
                </Badge>
              </div>
            </div>
            <div className="flex items-center space-x-3">
              <Button
                className="bg-green-600 hover:bg-green-700"
                onClick={() => handleDecision(candidate.name, "approved")}
              >
                <CheckCircle className="mr-2 h-4 w-4" />
                Approve
              </Button>
              <Button
                variant="outline"
                onClick={() => handleDecision(candidate.name, "put on hold")}
              >
                <Clock className="mr-2 h-4 w-4" />
                Hold
              </Button>
              <Button
                variant="outline"
                className="border-red-500/40 text-red-500 hover:bg-red-500/10"
                onClick={() => handleDecision(candidate.name, "rejected")}
              >
                <XCircle className="mr-2 h-4 w-4" />
                Reject
              </Button>
            </div>
          </GlassCard>
        ))}
      </div>
    </DashboardLayout>
  );
}
