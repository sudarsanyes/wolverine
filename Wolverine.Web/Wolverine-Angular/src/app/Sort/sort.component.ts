import { Component, HostListener } from '@angular/core';
import { ProjectService } from '../Services/project.service';
import { Project, Group, Card, SortSession } from '../Contracts/Contracts';
import { ClipboardService } from 'ngx-clipboard';
import { Router, ActivatedRoute } from '@angular/router';
import { CdkDragDrop, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';

@Component({
  selector: 'sort',
  templateUrl: './sort.component.html'
})
export class SortComponent {

  @HostListener('window:beforeunload')
  confirm(): boolean {
    if (this.IsDirty) {
      return confirm("You have unsaved changes in this page. Are you sure you want to navigate away from here?");
    }
  }

  ActiveSortSession: SortSession;
  ActiveProject: Project;
  Id: string;
  IsSaving: boolean;
  IsSavingSuccessful: boolean;
  IsDirty: boolean;
  Groups = [];

  constructor(private projectService: ProjectService, private router: Router, private route: ActivatedRoute, private clipboardService: ClipboardService) {
    // Reserved. 
  }

  ngOnInit() {
    this.Id = this.route.snapshot.paramMap.get('id');
    this.projectService.loadSort(this.Id).subscribe((sortSession: SortSession) => {
      this.ActiveSortSession = sortSession;
      this.ActiveProject = this.ActiveSortSession.project;
      for (let group of this.ActiveProject.groups) {
        this.Groups.push(group.id);
      }
    });
  }

  drop(event: CdkDragDrop<string[]>) {
    if (event.previousContainer === event.container) {
      console.log("same container");
      moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
    } else {
      console.log("different container");
      transferArrayItem(event.previousContainer.data,
        event.container.data,
        event.previousIndex,
        event.currentIndex);
    }
  }

  onPublish() {
    console.log(this.ActiveSortSession);
    if (this.ActiveSortSession.participant == null) {
      alert("Participant name cannot be empty. Make sure that you have mentioned your name as the participant.");
    }
    else {
      var userChoiceForPublishing = confirm("Remember: once submitted, you will not be able modify your sort. Are you sure you want to submit?");
      if (userChoiceForPublishing) {
        this.IsSaving = true;
        this.ActiveSortSession.project.isPublished = true;
        var response = this.projectService.saveSort(this.ActiveSortSession);
        response.subscribe((data: boolean) => {
          this.IsSaving = false;
          this.IsSavingSuccessful = data;
          if (this.IsSavingSuccessful) {
            this.IsDirty = false;
          }
        });
      }
    }
  }
}