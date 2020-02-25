import { Component, ɵConsole } from '@angular/core';
import { ProjectService } from '../Services/project.service';
import { Project, Group, Card, Guid } from '../Contracts/Contracts';
import { Router } from '@angular/router';

@Component({
  selector: 'home',
  templateUrl: './home.component.html'
})
export class HomeComponent {
  CreateProjectName: string = 'Untitled-1';
  SortProjectId: string;
  AnalyzeProjectId: string;
  NewProject: Project;
  IsCreatingNewProject: boolean;
  IsOpeningForSorting: boolean;
  IsOpeningForAnalysis: boolean;
  IsOpeningForSortingFailed: boolean;
  IsCreateProjectFailed: boolean;
  IsOpeningForAnalysisFailed: boolean;
  IsSortProjectIdMissing: boolean;
  IsAnalyzeProjectIdMissing: boolean;
  isCollapsed: boolean;

  EditProjectId: string;
  IsOpeningForEditing: boolean;
  IsOpenForEditingFailed: boolean;

  SearchProjectName: string;
  IsSearchingForProject: boolean;
  IsSearchedProjectFound: boolean;
  SearchProject: Project;
  IsSearchInitialized: boolean;

  constructor(private projectService: ProjectService, private router: Router) {
    this.IsCreatingNewProject = false;
    this.IsOpeningForSorting = false;
    this.IsOpeningForAnalysis = false;
    this.IsOpeningForSortingFailed = false;
    this.IsOpeningForAnalysisFailed = false;
    this.IsSortProjectIdMissing = false;
    this.IsAnalyzeProjectIdMissing = false;
    this.NewProject = new Project();
    this.NewProject.id = Guid.newGuid();
    this.isCollapsed = true;
    this.IsOpenForEditingFailed = false;
    this.IsOpeningForEditing = false;
    this.EditProjectId = null;
    this.IsSearchInitialized = false;
  }

  Sort() {
    this.ResetAllFailedFlags();
    if (this.SortProjectId == null) {
      this.IsSortProjectIdMissing = true;
    }
    else {
      this.IsSortProjectIdMissing = false;
      this.IsOpeningForSortingFailed = false;
      this.IsOpeningForSorting = true;
      this.projectService.createSort(this.SortProjectId).subscribe((sortSessionId: string) => {
        if (sortSessionId == null) {
          this.IsOpeningForSorting = false;
          this.IsOpeningForSortingFailed = true;
        }
        else {
          this.router.navigateByUrl('/sort/' + sortSessionId);
          this.IsOpeningForSorting = false;
        }
      });
    }
  }

  Create() {
    this.ResetAllFailedFlags();
    if (this.NewProject.name == null || this.NewProject.author == null) {
      console.log("missing fields");
      this.IsCreateProjectFailed = true;
    }
    else {
      console.log(this.NewProject);
      this.IsCreateProjectFailed = false;
      this.IsCreatingNewProject = true;
      this.projectService.create(this.NewProject).subscribe((id: string) => {
        this.router.navigateByUrl('/create/' + id);
        this.IsCreatingNewProject = false;
      });
    }
  }

  Open() {
    this.ResetAllFailedFlags();
    this.IsCreateProjectFailed = false;
    if (this.EditProjectId == null) {
      console.log("missing fields");
      this.IsOpenForEditingFailed = true;
    }
    else {
      this.IsOpenForEditingFailed = true;
      this.IsOpeningForEditing = true;

      console.log("checking for existance...");

      this.projectService.list().subscribe((projects: Project[]) => {
        console.log(this.EditProjectId);
        console.log(projects);
        projects.forEach((project) => {
          console.log(project.id);
          if (project.id == this.EditProjectId) {
            this.IsOpenForEditingFailed = false;
          }
        });
        if (!this.IsOpenForEditingFailed) {
          this.router.navigateByUrl('/create/' + this.EditProjectId);
        }
        this.IsOpenForEditingFailed = true;
      });

      console.log("finished checking for existance!");
      // this.router.navigateByUrl('/create/' + this.EditProjectId);
      this.IsOpeningForEditing = false;
    }
  }

  Analyze() {
    this.ResetAllFailedFlags();
    if (this.AnalyzeProjectId == null) {
      this.IsAnalyzeProjectIdMissing = true;
    }
    else {
      this.IsAnalyzeProjectIdMissing = false;
      this.projectService.load(this.AnalyzeProjectId).subscribe((project: Project) => {
        if (project == null || !project.isSessionZero || !project.isPublished) {
          this.IsOpeningForAnalysis = false;
          this.IsOpeningForAnalysisFailed = true;
        }
        else {
          this.router.navigateByUrl('/analyze/' + this.AnalyzeProjectId);
          this.IsOpeningForAnalysis = false;
        }
      });
    }
  }

  Search() {
    if (this.SearchProjectName != null) {
      this.ResetAllFailedFlags();
      this.IsSearchInitialized = true;
      this.IsSearchingForProject = true;
      this.projectService.list().subscribe((projects: Project[]) => {
        projects.forEach((project) => {
          if (project.name == this.SearchProjectName) {
            this.IsSearchedProjectFound = true;
            this.SearchProject = project;
          }
        });
        this.IsSearchingForProject = false;
      });
    }
  }

  ResetAllFailedFlags() {
    this.IsCreateProjectFailed = false;
    this.IsOpenForEditingFailed = false;
    this.IsOpeningForAnalysisFailed = false;
    this.IsOpeningForSortingFailed = false;
    this.IsSearchInitialized = false;
  }
}