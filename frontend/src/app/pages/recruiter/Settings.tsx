import { useState } from "react";
import { DashboardLayout } from "../../components/DashboardLayout";
import { GlassCard } from "../../components/GlassCard";
import { Button } from "../../components/ui/button";
import { Switch } from "../../components/ui/switch";
import { Input } from "../../components/ui/input";
import { Slider } from "../../components/ui/slider";
import {
  Building,
  Bell,
  Sliders,
  Link,
  Save,
  ChevronRight,
  Calendar,
  Mail,
  Zap,
} from "lucide-react";

type SettingsTab = "Company" | "Posting" | "Notifications" | "AI Ranking" | "Integrations";

const tabs: { label: SettingsTab; icon: React.ReactNode }[] = [
  { label: "Company", icon: <Building className="w-4 h-4" /> },
  { label: "Posting", icon: <Sliders className="w-4 h-4" /> },
  { label: "Notifications", icon: <Bell className="w-4 h-4" /> },
  { label: "AI Ranking", icon: <Zap className="w-4 h-4" /> },
  { label: "Integrations", icon: <Link className="w-4 h-4" /> },
];

export function RecruiterSettings() {
  const [activeTab, setActiveTab] = useState<SettingsTab>("Company");

  // Company
  const [companyName, setCompanyName] = useState("Acme Corp");
  const [companyWebsite, setCompanyWebsite] = useState("https://acmecorp.com");
  const [industry, setIndustry] = useState("Technology");
  const [companySize, setCompanySize] = useState("500–1,000");
  const [headquarters, setHeadquarters] = useState("San Francisco, CA");
  const [recruiterName, setRecruiterName] = useState("Jordan Williams");
  const [recruiterTitle, setRecruiterTitle] = useState("Senior Technical Recruiter");

  // Posting
  const [defaultVisibility, setDefaultVisibility] = useState<"Public" | "Private" | "Invite-Only">("Public");
  const [autoRenew, setAutoRenew] = useState(true);
  const [postingDuration, setPostingDuration] = useState("30");
  const [requireCoverLetter, setRequireCoverLetter] = useState(false);
  const [allowAnonymous, setAllowAnonymous] = useState(false);

  // Notifications
  const [notifToggles, setNotifToggles] = useState({
    newApplicationEmail: true,
    highMatchAlert: true,
    interviewReminders: true,
    weeklyReport: true,
    applicationDeadline: true,
    candidateWithdrawal: false,
  });
  const [digestFrequency, setDigestFrequency] = useState<"Daily" | "Weekly" | "Monthly">("Weekly");

  // AI Ranking
  const [autoRankingEnabled, setAutoRankingEnabled] = useState(true);
  const [rankingThreshold, setRankingThreshold] = useState([75]);
  const [skillsWeight, setSkillsWeight] = useState([40]);
  const [experienceWeight, setExperienceWeight] = useState([35]);
  const [educationWeight, setEducationWeight] = useState([15]);
  const [cultureWeight, setCultureWeight] = useState([10]);
  const [autoReject, setAutoReject] = useState(false);
  const [autoShortlist, setAutoShortlist] = useState(true);

  // Integrations
  const [atsConnected, setAtsConnected] = useState(true);
  const [calendarConnected, setCalendarConnected] = useState(true);
  const [emailConnected, setEmailConnected] = useState(true);
  const [slackConnected, setSlackConnected] = useState(false);

  const toggleNotif = (key: keyof typeof notifToggles) =>
    setNotifToggles((prev) => ({ ...prev, [key]: !prev[key] }));

  const SwitchRow = ({
    label,
    description,
    checked,
    onChange,
  }: {
    label: string;
    description?: string;
    checked: boolean;
    onChange: () => void;
  }) => (
    <div className="flex items-center justify-between py-3 border-b border-[#D4AF37]/10 last:border-0">
      <div>
        <p className="text-white text-sm font-medium">{label}</p>
        {description && <p className="text-gray-400 text-xs mt-0.5">{description}</p>}
      </div>
      <Switch checked={checked} onCheckedChange={onChange} />
    </div>
  );

  const IntegrationCard = ({
    icon,
    name,
    description,
    connected,
    onToggle,
  }: {
    icon: React.ReactNode;
    name: string;
    description: string;
    connected: boolean;
    onToggle: () => void;
  }) => (
    <div className="flex items-center justify-between py-4 border-b border-[#D4AF37]/10 last:border-0">
      <div className="flex items-center gap-3">
        <div className="w-10 h-10 rounded-xl bg-[#2D2D2D] flex items-center justify-center text-[#D4AF37]">
          {icon}
        </div>
        <div>
          <p className="text-white text-sm font-medium">{name}</p>
          <p className="text-gray-400 text-xs mt-0.5">{description}</p>
        </div>
      </div>
      <div className="flex items-center gap-3">
        <span className={`text-xs font-medium ${connected ? "text-green-400" : "text-gray-500"}`}>
          {connected ? "Connected" : "Not Connected"}
        </span>
        <Button
          variant="outline"
          onClick={onToggle}
          className={`text-sm ${
            connected
              ? "border-red-500/20 text-red-400 hover:bg-red-500/10"
              : "border-[#D4AF37]/20 text-[#D4AF37] hover:bg-[#D4AF37]/10"
          }`}
        >
          {connected ? "Disconnect" : "Connect"}
        </Button>
      </div>
    </div>
  );

  return (
    <DashboardLayout role="recruiter">
      <div className="p-6 space-y-6">
        <div>
          <h1 className="text-2xl font-bold text-white">Settings</h1>
          <p className="text-gray-400 mt-1">Manage your recruiter preferences and integrations</p>
        </div>

        <div className="flex gap-6">
          {/* Sidebar */}
          <div className="w-56 shrink-0 space-y-1">
            {tabs.map((tab) => (
              <button
                key={tab.label}
                onClick={() => setActiveTab(tab.label)}
                className={`w-full flex items-center gap-3 px-4 py-3 rounded-xl text-sm font-medium transition-all text-left ${
                  activeTab === tab.label
                    ? "bg-[#D4AF37]/10 text-[#D4AF37] border border-[#D4AF37]/20"
                    : "text-gray-400 hover:text-white hover:bg-[#2D2D2D]"
                }`}
              >
                {tab.icon}
                {tab.label}
                {activeTab === tab.label && (
                  <ChevronRight className="w-4 h-4 ml-auto" />
                )}
              </button>
            ))}
          </div>

          {/* Content */}
          <div className="flex-1 space-y-4">
            {/* Company Profile */}
            {activeTab === "Company" && (
              <GlassCard className="p-6">
                <h2 className="text-lg font-semibold text-white mb-4 flex items-center gap-2">
                  <Building className="w-5 h-5 text-[#D4AF37]" />
                  Company Profile
                </h2>
                <div className="grid grid-cols-2 gap-4">
                  <div className="col-span-2">
                    <label className="text-gray-400 text-xs mb-1 block">Company Name</label>
                    <Input value={companyName} onChange={(e) => setCompanyName(e.target.value)} className="bg-[#2D2D2D] border-[#D4AF37]/20 text-white" />
                  </div>
                  <div>
                    <label className="text-gray-400 text-xs mb-1 block">Website</label>
                    <Input value={companyWebsite} onChange={(e) => setCompanyWebsite(e.target.value)} className="bg-[#2D2D2D] border-[#D4AF37]/20 text-white" />
                  </div>
                  <div>
                    <label className="text-gray-400 text-xs mb-1 block">Industry</label>
                    <Input value={industry} onChange={(e) => setIndustry(e.target.value)} className="bg-[#2D2D2D] border-[#D4AF37]/20 text-white" />
                  </div>
                  <div>
                    <label className="text-gray-400 text-xs mb-1 block">Company Size</label>
                    <Input value={companySize} onChange={(e) => setCompanySize(e.target.value)} className="bg-[#2D2D2D] border-[#D4AF37]/20 text-white" />
                  </div>
                  <div>
                    <label className="text-gray-400 text-xs mb-1 block">Headquarters</label>
                    <Input value={headquarters} onChange={(e) => setHeadquarters(e.target.value)} className="bg-[#2D2D2D] border-[#D4AF37]/20 text-white" />
                  </div>
                  <div>
                    <label className="text-gray-400 text-xs mb-1 block">Your Name</label>
                    <Input value={recruiterName} onChange={(e) => setRecruiterName(e.target.value)} className="bg-[#2D2D2D] border-[#D4AF37]/20 text-white" />
                  </div>
                  <div>
                    <label className="text-gray-400 text-xs mb-1 block">Your Title</label>
                    <Input value={recruiterTitle} onChange={(e) => setRecruiterTitle(e.target.value)} className="bg-[#2D2D2D] border-[#D4AF37]/20 text-white" />
                  </div>
                </div>
                <div className="mt-4 pt-4 border-t border-[#D4AF37]/10 flex justify-end">
                  <Button className="bg-[#D4AF37] text-black font-semibold hover:bg-[#D4AF37]/90">
                    <Save className="w-4 h-4 mr-2" />
                    Save Profile
                  </Button>
                </div>
              </GlassCard>
            )}

            {/* Posting Preferences */}
            {activeTab === "Posting" && (
              <GlassCard className="p-6">
                <h2 className="text-lg font-semibold text-white mb-4 flex items-center gap-2">
                  <Sliders className="w-5 h-5 text-[#D4AF37]" />
                  Job Posting Preferences
                </h2>
                <div className="space-y-4">
                  <div>
                    <label className="text-gray-400 text-xs mb-2 block">Default Posting Visibility</label>
                    <div className="flex gap-3">
                      {(["Public", "Private", "Invite-Only"] as const).map((v) => (
                        <button
                          key={v}
                          onClick={() => setDefaultVisibility(v)}
                          className={`px-4 py-2 rounded-xl text-sm font-medium transition-all border ${
                            defaultVisibility === v
                              ? "bg-[#D4AF37]/10 text-[#D4AF37] border-[#D4AF37]/40"
                              : "border-[#D4AF37]/10 text-gray-400 hover:border-[#D4AF37]/20"
                          }`}
                        >
                          {v}
                        </button>
                      ))}
                    </div>
                  </div>
                  <div>
                    <label className="text-gray-400 text-xs mb-1 block">Default Posting Duration (days)</label>
                    <Input value={postingDuration} onChange={(e) => setPostingDuration(e.target.value)} className="bg-[#2D2D2D] border-[#D4AF37]/20 text-white w-32" />
                  </div>
                  <div className="pt-2 border-t border-[#D4AF37]/10">
                    <SwitchRow label="Auto-Renew Postings" description="Automatically renew postings before they expire" checked={autoRenew} onChange={() => setAutoRenew(!autoRenew)} />
                    <SwitchRow label="Require Cover Letter" description="Make cover letter mandatory for all applications" checked={requireCoverLetter} onChange={() => setRequireCoverLetter(!requireCoverLetter)} />
                    <SwitchRow label="Allow Anonymous Applications" description="Let candidates apply without revealing their identity" checked={allowAnonymous} onChange={() => setAllowAnonymous(!allowAnonymous)} />
                  </div>
                </div>
                <div className="mt-4 pt-4 border-t border-[#D4AF37]/10 flex justify-end">
                  <Button className="bg-[#D4AF37] text-black font-semibold hover:bg-[#D4AF37]/90">
                    <Save className="w-4 h-4 mr-2" />
                    Save Preferences
                  </Button>
                </div>
              </GlassCard>
            )}

            {/* Notification Settings */}
            {activeTab === "Notifications" && (
              <>
                <GlassCard className="p-6">
                  <h2 className="text-lg font-semibold text-white mb-1 flex items-center gap-2">
                    <Bell className="w-5 h-5 text-[#D4AF37]" />
                    Email Digest Frequency
                  </h2>
                  <p className="text-gray-400 text-sm mb-4">How often you receive summary emails</p>
                  <div className="flex gap-3">
                    {(["Daily", "Weekly", "Monthly"] as const).map((f) => (
                      <button
                        key={f}
                        onClick={() => setDigestFrequency(f)}
                        className={`px-4 py-2 rounded-xl text-sm font-medium transition-all border ${
                          digestFrequency === f
                            ? "bg-[#D4AF37]/10 text-[#D4AF37] border-[#D4AF37]/40"
                            : "border-[#D4AF37]/10 text-gray-400 hover:border-[#D4AF37]/20"
                        }`}
                      >
                        {f}
                      </button>
                    ))}
                  </div>
                </GlassCard>
                <GlassCard className="p-6">
                  <h2 className="text-lg font-semibold text-white mb-4 flex items-center gap-2">
                    <Mail className="w-5 h-5 text-[#D4AF37]" />
                    Notification Triggers
                  </h2>
                  <SwitchRow label="New Application Received" description="Email when a candidate applies to your posting" checked={notifToggles.newApplicationEmail} onChange={() => toggleNotif("newApplicationEmail")} />
                  <SwitchRow label="High-Match Candidate Alert" description="Instant alert for candidates scoring 90%+" checked={notifToggles.highMatchAlert} onChange={() => toggleNotif("highMatchAlert")} />
                  <SwitchRow label="Interview Reminders" description="Reminders 24h before scheduled interviews" checked={notifToggles.interviewReminders} onChange={() => toggleNotif("interviewReminders")} />
                  <SwitchRow label="Weekly Hiring Report" description="Summary of all hiring activity each week" checked={notifToggles.weeklyReport} onChange={() => toggleNotif("weeklyReport")} />
                  <SwitchRow label="Posting Expiration Alerts" description="Notify me 3 days before a posting expires" checked={notifToggles.applicationDeadline} onChange={() => toggleNotif("applicationDeadline")} />
                  <SwitchRow label="Candidate Withdrawal Alerts" description="Notify when a candidate withdraws their application" checked={notifToggles.candidateWithdrawal} onChange={() => toggleNotif("candidateWithdrawal")} />
                  <div className="mt-4 pt-4 border-t border-[#D4AF37]/10 flex justify-end">
                    <Button className="bg-[#D4AF37] text-black font-semibold hover:bg-[#D4AF37]/90">
                      <Save className="w-4 h-4 mr-2" />
                      Save Notifications
                    </Button>
                  </div>
                </GlassCard>
              </>
            )}

            {/* AI Ranking */}
            {activeTab === "AI Ranking" && (
              <>
                <GlassCard className="p-6">
                  <h2 className="text-lg font-semibold text-white mb-1 flex items-center gap-2">
                    <Zap className="w-5 h-5 text-[#D4AF37]" />
                    AI Ranking Configuration
                  </h2>
                  <p className="text-gray-400 text-sm mb-4">Configure how the AI evaluates and ranks candidates</p>
                  <SwitchRow label="Enable Auto-Ranking" description="Automatically rank all incoming applications using AI" checked={autoRankingEnabled} onChange={() => setAutoRankingEnabled(!autoRankingEnabled)} />
                  <SwitchRow label="Auto-Shortlist High Scorers" description="Automatically move candidates scoring above threshold to shortlist" checked={autoShortlist} onChange={() => setAutoShortlist(!autoShortlist)} />
                  <SwitchRow label="Auto-Reject Below Threshold" description="Automatically reject candidates below minimum score (use with care)" checked={autoReject} onChange={() => setAutoReject(!autoReject)} />
                </GlassCard>
                <GlassCard className="p-6">
                  <h2 className="text-lg font-semibold text-white mb-4">Ranking Threshold & Weights</h2>
                  <div className="space-y-6">
                    <div>
                      <div className="flex justify-between mb-2">
                        <label className="text-gray-300 text-sm">Minimum Ranking Threshold</label>
                        <span className="text-[#D4AF37] font-semibold text-sm">{rankingThreshold[0]}%</span>
                      </div>
                      <Slider value={rankingThreshold} onValueChange={setRankingThreshold} min={50} max={95} step={5} className="w-full" />
                      <p className="text-gray-500 text-xs mt-1">Candidates below this score are flagged for manual review</p>
                    </div>
                    <div className="pt-4 border-t border-[#D4AF37]/10">
                      <p className="text-gray-300 text-sm font-medium mb-4">Scoring Weights (must total 100%)</p>
                      {[
                        { label: "Technical Skills", value: skillsWeight, set: setSkillsWeight },
                        { label: "Work Experience", value: experienceWeight, set: setExperienceWeight },
                        { label: "Education", value: educationWeight, set: setEducationWeight },
                        { label: "Culture Fit", value: cultureWeight, set: setCultureWeight },
                      ].map(({ label, value, set }) => (
                        <div key={label} className="mb-4">
                          <div className="flex justify-between mb-2">
                            <label className="text-gray-400 text-sm">{label}</label>
                            <span className="text-[#D4AF37] font-semibold text-sm">{value[0]}%</span>
                          </div>
                          <Slider value={value} onValueChange={set} min={0} max={60} step={5} className="w-full" />
                        </div>
                      ))}
                    </div>
                  </div>
                  <div className="mt-4 pt-4 border-t border-[#D4AF37]/10 flex justify-end">
                    <Button className="bg-[#D4AF37] text-black font-semibold hover:bg-[#D4AF37]/90">
                      <Save className="w-4 h-4 mr-2" />
                      Save AI Settings
                    </Button>
                  </div>
                </GlassCard>
              </>
            )}

            {/* Integrations */}
            {activeTab === "Integrations" && (
              <GlassCard className="p-6">
                <h2 className="text-lg font-semibold text-white mb-1 flex items-center gap-2">
                  <Link className="w-5 h-5 text-[#D4AF37]" />
                  Integration Settings
                </h2>
                <p className="text-gray-400 text-sm mb-6">Connect your existing tools and systems</p>
                <IntegrationCard
                  icon={<Sliders className="w-5 h-5" />}
                  name="Greenhouse ATS"
                  description="Sync candidates and pipeline stages with your ATS"
                  connected={atsConnected}
                  onToggle={() => setAtsConnected(!atsConnected)}
                />
                <IntegrationCard
                  icon={<Calendar className="w-5 h-5" />}
                  name="Google Calendar"
                  description="Sync interview schedules with your Google Calendar"
                  connected={calendarConnected}
                  onToggle={() => setCalendarConnected(!calendarConnected)}
                />
                <IntegrationCard
                  icon={<Mail className="w-5 h-5" />}
                  name="Gmail / Outlook"
                  description="Send and receive candidate communications via email"
                  connected={emailConnected}
                  onToggle={() => setEmailConnected(!emailConnected)}
                />
                <IntegrationCard
                  icon={<Zap className="w-5 h-5" />}
                  name="Slack"
                  description="Receive hiring alerts and notifications in Slack channels"
                  connected={slackConnected}
                  onToggle={() => setSlackConnected(!slackConnected)}
                />
              </GlassCard>
            )}
          </div>
        </div>
      </div>
    </DashboardLayout>
  );
}
