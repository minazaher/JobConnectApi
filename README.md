## JobConnect: A Comprehensive Job Portal Platform

**JobConnect** is an online platform designed to streamline the job search process for both job seekers and employers. It provides a convenient and efficient way for individuals to find job opportunities and for employers to find qualified candidates.

**Actors (User Types):**

* **Admin:** Manages the platform and user accounts.
* **Job Seeker:** Searches for jobs, applies for openings, and communicates with employers.
* **Employer/Recruiter:** Posts job openings, reviews proposals, and communicates with job seekers.

**Requirements:**

* **Admin Features:**
    * CRUD (Create, Read, Update, Delete) operations on employer accounts.
    * Approve or reject job postings submitted by employers. Approved jobs become visible to job seekers.
* **Job Seeker Features:**
    * View all posted jobs.
    * Search for jobs based on location, industry, title, salary range, and date.
    * Apply for jobs by submitting proposals (including CV attachments).
    * Save important jobs for later application.
    * Communicate with employers after a proposal is accepted.
* **Employer Features:**
    * Post new job openings.
    * Review submitted proposals from job seekers (accept or reject).
    * Jobs are automatically removed from the platform once a proposal is accepted.
    * Communicate with job seekers after a proposal is accepted.
* **Job Details:**
    * Employer name
    * Job type (part-time, full-time, remote)
    * Job budget
    * Post creation date
    * Job description
    * Number of proposals submitted

**User Roles:**

1. **Admin:**
    * Login/Logout
    * Manage employers (CRUD)
    * Approve/Reject job posts
2. **Job Seeker:**
    * Login/Logout/Register
    * View & Search for jobs
    * Apply for jobs (submit proposals)
    * Save important jobs
    * Communicate with employers (after acceptance)
3. **Employer:**
    * Login/Logout
    * Post new jobs
    * Review proposals (accept or reject)
    * Communicate with job seekers (after acceptance)
