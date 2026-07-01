import { DashboardLayout } from "../../components/DashboardLayout";
import { GlassCard } from "../../components/GlassCard";
import { Button } from "../../components/ui/button";
import { Badge } from "../../components/ui/badge";
import { FileText, Download, Sparkles, Award } from "lucide-react";

export function ResumeReader() {
  return (
    <DashboardLayout role="recruiter">
      <div className="mb-8">
        <h1 className="text-3xl font-bold text-white mb-2">Resume Reader</h1>
        <p className="text-gray-400">AI-powered resume analysis and skill extraction</p>
      </div>

      <div className="grid lg:grid-cols-3 gap-6">
        <div className="lg:col-span-2">
          <GlassCard className="p-6">
            <div className="flex items-center justify-between mb-6">
              <div className="flex items-center space-x-3">
                <FileText className="h-6 w-6 text-[#D4AF37]" />
                <h2 className="text-xl font-semibold text-white">Alice_Johnson_Resume.pdf</h2>
              </div>
              <Button variant="outline" size="sm">
                <Download className="mr-2 h-4 w-4" />
                Download
              </Button>
            </div>
            <div className="bg-white/5 p-8 rounded-lg min-h-[600px]">
              <p className="text-gray-400 text-center">PDF Viewer Placeholder</p>
            </div>
          </GlassCard>
        </div>

        <div className="space-y-6">
          <GlassCard className="p-6">
            <div className="flex items-center space-x-2 mb-4">
              <Sparkles className="h-5 w-5 text-[#D4AF37]" />
              <h3 className="font-semibold text-white">AI Analysis</h3>
            </div>
            <div className="space-y-4">
              <div>
                <p className="text-sm text-gray-400 mb-2">Match Score</p>
                <p className="text-3xl font-bold text-[#D4AF37]">98%</p>
              </div>
              <div>
                <p className="text-sm text-gray-400 mb-2">Skills</p>
                <div className="flex flex-wrap gap-2">
                  {["React", "TypeScript", "Node.js", "AWS", "Docker"].map((skill) => (
                    <Badge key={skill} variant="secondary">{skill}</Badge>
                  ))}
                </div>
              </div>
              <div>
                <p className="text-sm text-gray-400 mb-2">Experience</p>
                <p className="text-white">5+ years</p>
              </div>
              <div>
                <p className="text-sm text-gray-400 mb-2">Education</p>
                <p className="text-white">BS Computer Science</p>
              </div>
            </div>
          </GlassCard>
        </div>
      </div>
    </DashboardLayout>
  );
}
