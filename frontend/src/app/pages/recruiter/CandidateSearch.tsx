import { DashboardLayout } from "../../components/DashboardLayout";
import { GlassCard } from "../../components/GlassCard";
import { Input } from "../../components/ui/input";
import { Button } from "../../components/ui/button";
import { Badge } from "../../components/ui/badge";
import { Search, Star, MapPin, Briefcase, Eye } from "lucide-react";

export function CandidateSearch() {
  const candidates = [
    { id: 1, name: "Alice Johnson", title: "Senior Frontend Developer", location: "San Francisco, CA", experience: "5+ years", matchScore: 98, skills: ["React", "TypeScript", "Node.js"] },
    { id: 2, name: "Bob Smith", title: "Full Stack Engineer", location: "Remote", experience: "7+ years", matchScore: 95, skills: ["Python", "Django", "PostgreSQL"] },
    { id: 3, name: "Carol Williams", title: "DevOps Engineer", location: "New York, NY", experience: "6+ years", matchScore: 92, skills: ["AWS", "Docker", "Kubernetes"] },
  ];

  return (
    <DashboardLayout role="recruiter">
      <div className="mb-8">
        <h1 className="text-3xl font-bold text-white mb-2">Find Candidates</h1>
        <p className="text-gray-400">Search and discover talented professionals</p>
      </div>

      <GlassCard className="p-6 mb-6">
        <div className="relative">
          <Search className="absolute left-3 top-1/2 -translate-y-1/2 h-5 w-5 text-gray-400" />
          <Input
            placeholder="Search by skills, job title, or keywords..."
            className="pl-10 bg-[#1E1E1E] border-[#D4AF37]/20 text-white"
          />
        </div>
      </GlassCard>

      <div className="space-y-4">
        {candidates.map((candidate) => (
          <GlassCard key={candidate.id} className="p-6 hover:border-[#D4AF37]/40 transition-colors">
            <div className="flex items-start justify-between mb-4">
              <div>
                <div className="flex items-center space-x-2 mb-2">
                  <h3 className="text-xl font-semibold text-white">{candidate.name}</h3>
                  <Badge className="bg-[#D4AF37]/20 text-[#D4AF37] border-[#D4AF37]/40">
                    <Star className="h-3 w-3 mr-1" />
                    {candidate.matchScore}% Match
                  </Badge>
                </div>
                <p className="text-gray-300 mb-3">{candidate.title}</p>
                <div className="flex items-center space-x-4 text-sm text-gray-400 mb-4">
                  <div className="flex items-center space-x-1">
                    <MapPin className="h-4 w-4" />
                    <span>{candidate.location}</span>
                  </div>
                  <div className="flex items-center space-x-1">
                    <Briefcase className="h-4 w-4" />
                    <span>{candidate.experience}</span>
                  </div>
                </div>
                <div className="flex flex-wrap gap-2">
                  {candidate.skills.map((skill) => (
                    <Badge key={skill} variant="outline">{skill}</Badge>
                  ))}
                </div>
              </div>
              <Button>
                <Eye className="mr-2 h-4 w-4" />
                View Profile
              </Button>
            </div>
          </GlassCard>
        ))}
      </div>
    </DashboardLayout>
  );
}
