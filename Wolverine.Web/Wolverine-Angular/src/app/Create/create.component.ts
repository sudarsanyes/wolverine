import { Component, HostListener, AfterViewChecked } from '@angular/core';
import { ProjectService } from '../Services/project.service';
import { Group, Card, Guid, SimplifiedProject } from '../Contracts/Contracts';
import { Router, ActivatedRoute } from '@angular/router';
import { ClipboardService } from 'ngx-clipboard'
import { ConfirmationGuard } from '../Guards/confirmation.guard';
import { delay } from 'q';

@Component({
    selector: 'create',
    templateUrl: './create.component.html'
})
export class CreateComponent implements AfterViewChecked {

    ActiveProject: SimplifiedProject;
    ID: string;
    NewCard: Card;
    NewGroup: Group;
    IsSaving: boolean;
    IsSavingSuccessful: boolean;
    IsDirty: boolean;
    IsPublishing: boolean;
    IsPublishingSuccessful: boolean;

    IsLocking: boolean;
    IsLockingSuccessful: boolean;
    IsUnlocking: boolean;
    IsUnlockingSuccessful: boolean;

    idOfElementAdded: string;

    constructor(private projectService: ProjectService, private router: Router, private route: ActivatedRoute, private clipboardService: ClipboardService, private confirmGuard: ConfirmationGuard) {
        this.NewCard = Card.getDefault();
        this.NewGroup = Group.getDefault();
    }

    ngOnInit() {
        this.ID = this.route.snapshot.paramMap.get('name');
        this.projectService.load(this.ID).subscribe((project: SimplifiedProject) => {
            this.ActiveProject = project;
        });
    }

    ngAfterViewChecked() {
        if (this.idOfElementAdded != null) {
            document.getElementById(this.idOfElementAdded).scrollIntoView({ block: 'end', behavior: 'smooth' });
            this.idOfElementAdded = null;
        }
    }

    onEditCard(card: Card) {
        this.IsDirty = true;
    }

    onDeleteCard(card: Card) {
        var index = 0;
        for (let c of this.ActiveProject.unsortedGroup.cards) {
            if (c.id == card.id) {
                this.ActiveProject.unsortedGroup.cards.splice(index, 1);
                break;
            }
            index++;
        }
        this.IsDirty = true;
    }

    onAddCard() {
        this.idOfElementAdded = this.NewCard.id;
        this.ActiveProject.unsortedGroup.cards.push(this.NewCard);
        this.NewCard = Card.getDefault();
        this.IsDirty = true;
    }

    onEditGroup(group: Group) {
        this.IsDirty = true;
    }

    onDeleteGroup(group: Group) {
        var index = 0;
        for (let g of this.ActiveProject.defaultGroups) {
            if (g.id == group.id) {
                this.ActiveProject.defaultGroups.splice(index, 1);
                break;
            }
            index++;
        }
        this.IsDirty = true;
    }

    onAddGroup() {
        this.idOfElementAdded = this.NewGroup.id;
        this.ActiveProject.defaultGroups.push(this.NewGroup);
        this.NewGroup = Group.getDefault();
        this.NewGroup.isUnsorted = false;
        this.IsDirty = true;
    }

    onAddCardToGroup(group: Group) {
        group.cards.push(this.NewCard);
        this.NewCard = Card.getDefault();
        this.IsDirty = true;
    }

    onDeleteCardFromGroup(group: Group, card: Card) {
        var index = 0;
        for (let c of group.cards) {
            if (c.id == card.id) {
                group.cards.splice(index, 1);
                break;
            }
            index++;
        }
        this.IsDirty = true;
    }

    onSave() {
        this.IsSaving = true;
        var response = this.projectService.save(this.ActiveProject);
        response.subscribe((data: boolean) => {
            this.IsSaving = false;
            this.IsSavingSuccessful = data;
            if (this.IsSavingSuccessful) {
                this.IsDirty = false;
            }
        });
    }

    onPublish() {
        this.IsPublishing = true;
        var userChoiceForPublishing = confirm("Remember: once published, you will not be able to add or remove cards. Are you sure you want to publish the project for the participants to sort?");
        if (userChoiceForPublishing) {
            this.ActiveProject.isPublished = true;
            console.log(this.ActiveProject);
            var response = this.projectService.save(this.ActiveProject);
            response.subscribe((data: boolean) => {
                this.IsPublishing = false;
                this.IsPublishingSuccessful = data;
                this.onEmailLink();
            });
        }
        this.IsPublishing = false;
    }

    onShareLink() {
        this.clipboardService.copyFromContent(this.ActiveProject.id);
        alert("The Id of the project has been copied to your clipboard. Press Ctrl + V to retrieve it.");
    }

    onEmailLink() {
        var mailText = "mailto:abc@abc.com?subject=" + this.ActiveProject.name + " - is ready for sorting&body=https://sudarsanyes.github.io/wolverine/sort/" + this.ActiveProject.id;
        console.log(mailText);
        window.location.href = mailText;
    }

    onLock() {
        this.ActiveProject.isLocked = true;
        this.IsLocking = true;
        var response = this.projectService.save(this.ActiveProject);
        response.subscribe((data: boolean) => {
            this.IsLockingSuccessful = data;
            if (this.IsLockingSuccessful) {
                this.IsLocking = false;
            }
        });
    }

    onUnlock() {
        this.ActiveProject.isLocked = false;
        this.IsUnlocking = true;
        var response = this.projectService.save(this.ActiveProject);
        response.subscribe((data: boolean) => {
            this.IsUnlockingSuccessful = data;
            if (this.IsUnlockingSuccessful) {
                this.IsUnlocking = false;
            }
        });
    }

    @HostListener('window:beforeunload')
    confirm(): boolean {
        if (this.IsDirty) {
            return confirm("You have unsaved changes in this page. Are you sure you want to navigate away from here?");
        }
    }
}